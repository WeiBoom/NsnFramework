using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace NeverSayNever.EditorUtilitiy
{
    using NeverSayNever;
    public enum ECompressOption
    {
        Uncompressed,
        ChunkBasedCompressionLZ4,
    }

    public static class BundleBuildTool
    {
        //[MenuItem("NeverSayNever/Tools/Build AssetBundle(Current Paltform)", false, 100)]
        public static void MenuItem_BuildAssetBundle()
        {
            var targetPlatform = EditorUserBuildSettings.activeBuildTarget.ToString();
            switch(EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    targetPlatform = "Windows";
                    break;
                case BuildTarget.Android:
                    targetPlatform = "Android";
                    break;
                case BuildTarget.StandaloneOSX:
                case BuildTarget.iOS:
                    targetPlatform = "iOS";
                    break;
            }

            if (EditorUtility.DisplayDialog("资源构建", $"构建所有资源到 {targetPlatform} 平台", "确定", "取消"))
            {
                BuildInternal(EditorUserBuildSettings.activeBuildTarget);
            }
        }

        private static void BuildInternal(BuildTarget buildTarget)
        {
            var packager = new BundlePackager(buildTarget, 0);
            packager.CompressOption = ECompressOption.ChunkBasedCompressionLZ4;
            
            ExecuteWithStopWatch(packager.OnPreBuild);
            ExecuteWithStopWatch(packager.OnPostBuild);
        }


        private static void ExecuteWithStopWatch(UnityEngine.Events.UnityAction action)
        {
            if (action == null)
                return;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            action?.Invoke();
            watch.Stop();
            Debug.Log($"执行 {action.Method.Name } 完成，耗时 : {watch.ElapsedMilliseconds / 1000} 秒");
        }
    }


    public class BundlePackager
    {
        public class BundleBuild
        {
            public string bundleName;
            public List<string> assetNames = new List<string>();

            public AssetBundleBuild ToBuild()
            {
                var build = new AssetBundleBuild()
                {
                    assetBundleName = bundleName,
                    assetNames = assetNames.ToArray()
                };
                return build;
            }
        }

        /// <summary>
        /// 框架默认的全局配置
        /// </summary>
        private static NsnGlobalAssetConfig globalConfig;
        /// <summary>
        /// shader文件合并成一个bundle
        /// </summary>
        public const string ShaderBundle = "shaders";
        /// <summary>
        /// AB文件默认后缀名（前缀加点）
        /// </summary>
        public const string BundleDefaultVariantWithPoint = ".u3d";
        /// <summary>
		/// 构建输出的Unity清单文件名称
		/// </summary>
		public const string UnityManifestFileName = "UnityManifest";
        /// <summary>
        /// 构建输出的补丁清单文件名称
        /// </summary>
        public const string PatchManifestFileName = "PatchManifest.bytes";

        /// <summary>
        /// 构建bundle的平台
        /// </summary>
        private BuildTarget BuildTarget { get; }
        /// <summary>
        /// 资源版本号
        /// </summary>
        private int BundleVersion { get; }
        /// <summary>
        /// 构建资源后输出的路径
        /// </summary>
        private string BuildOutputPath { get; }
        /// <summary>
        /// 当前平台构建资源输出的路径
        /// </summary>
        private string PlatformOutputPath { get; }
        /// <summary>
        /// 资源压缩方式
        /// </summary>
        public ECompressOption CompressOption = ECompressOption.Uncompressed;
        /// <summary>
        /// 是否强制重新打包资源
        /// </summary>
        public bool IsForceRebuild = true;


        // 临时缓存lua拷贝的txt文件路径，打包完成后删除
        private static Queue<string> temporyLuaTxtFileList = new Queue<string>();


        public BundlePackager(BuildTarget buildTarget, int bundleVersion)
        {
            BuildTarget = buildTarget;
            BundleVersion = bundleVersion;
            BuildOutputPath = Application.streamingAssetsPath; //NEditorTools.GetProjectPath();
            PlatformOutputPath = $"{BuildOutputPath}/{NsnPlatform.Platform}";
        }

        /// <summary>
        /// 构建之前的预处理，检查配置与资源
        /// </summary>
        public void OnPreBuild()
        {
            Debug.Log("----- 检查配置，准备构建资源 ------");

            // 检查构建平台
            if (BuildTarget == BuildTarget.NoTarget)
                throw new System.Exception("请选择构建平台");
            // 检查资源版本号
            if (BundleVersion < 0)
                throw new System.Exception("请设置资源版本号");
            // 检查输出目录
            if (string.IsNullOrEmpty(PlatformOutputPath))
                throw new System.Exception("资源输出目录不能为空");

            // 重构资源，清空原有资源目录
            if (IsForceRebuild)
            {
                NEditorTools.ClearFolder(PlatformOutputPath);
                Debug.Log($"删除资源目录 ： {PlatformOutputPath}");
            }

            // 创建输出目录
            if (Directory.Exists(PlatformOutputPath)) return;
            Directory.CreateDirectory(PlatformOutputPath);
            Debug.Log($"输出目录：{PlatformOutputPath}");
        }

        /// <summary>
        /// 构建资源
        /// </summary>
        public void OnPostBuild()
        {
            var targetBuildInfoList = new Dictionary<string, BundleBuild>();

            var collectList = FrameworkConfig.GlobalConfig.BuildAssetCollections;

            foreach (var element in collectList)
            {
                var directoryPath = NEditorTools.GetRegularPath(AssetDatabase.GetAssetPath(element.folder));
                // 因为unity不能识别.lua文件，所以lua需要单独处理
                // 这里按整个文件夹打包的形式处理，所有的lua文件都打包为一个bundle
                if (element.buildType == EBundleBuildType.Lua)
                {
                    var bundleBuild = BuildLuaBundle(directoryPath);
                    targetBuildInfoList.Add(bundleBuild.bundleName, bundleBuild);
                    continue;
                }

                // 子文件夹单独一个包
                if (element.labelType == EBundleLabelType.ByChildFolderName)
                {
                    var childDirectories = Directory.GetDirectories(directoryPath);
                    foreach (var childPath in childDirectories)
                    {
                        var bundleBuild = BuildDirectoryToPack(childPath, element.buildType);
                        targetBuildInfoList.Add(bundleBuild.bundleName, bundleBuild);
                    }
                }
                // 整个文件夹单独一个包
                else if (element.labelType == EBundleLabelType.ByFolderName)
                {
                    var bundleBuild = BuildDirectoryToPack(directoryPath, element.buildType);
                    targetBuildInfoList.Add(bundleBuild.bundleName, bundleBuild);
                }
                // 每个文件单独打包
                else
                {
                    List<string> files = GetAllFilesWithFilter(directoryPath, element.buildType);
                    foreach (var file in files)
                    {
                        BundleBuild bundleBuild;
                        if (element.buildType == EBundleBuildType.Atalas)
                        {
                            bundleBuild = BuildSpriteAtlasToPack(file, element.buildType);
                        }
                        else
                        {
                            bundleBuild = CreateBundleBuildInfo(element.buildType, file, new string[] { file });
                        }
                        targetBuildInfoList.Add(bundleBuild.bundleName, bundleBuild);
                    }
                }
            }

            var buildList = new List<AssetBundleBuild>();
            foreach (var bundle in targetBuildInfoList.Values)
            {
                buildList.Add(bundle.ToBuild());
            }
            var buildArray = buildList.ToArray();
            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(PlatformOutputPath, buildArray, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget);
            if (manifest == null)
                Debug.LogError("BuildAssetBundles Failure");
            else
                Debug.Log("BuildAssetBundles Success");

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();

            // 打包完成，删除临时创建的lua文件
            while (temporyLuaTxtFileList.Count > 0)
            {
                var str = temporyLuaTxtFileList.Dequeue();
                NEditorTools.DeleteFile(str);
                NEditorTools.DeleteFile(str + ".meta");
            }
        }

        public void ClearAllAssetBundles()
        {
            var names = AssetDatabase.GetAllAssetBundleNames();
            var length = names.Length;
            for(int i =0;i < length; i++)
            {
                var label = names[i];
                if(EditorUtility.DisplayCancelableProgressBar($"Clear AssetBundles : {i}/{length}",label,(float)i / length))
                    break;
                AssetDatabase.RemoveAssetBundleName(label, true);
            }
            EditorUtility.ClearProgressBar();
        }


        private List<string> GetAllFilesWithFilter(string directoryPath, EBundleBuildType buildType)
        {
            List<string> allFiles = new List<string>();
            var filters = GetTargetSuffixByBuildType(buildType);
            var filterArray = filters.Split('|');
            foreach (var filter in filterArray)
            {
                if (!filters.IsNullOrEmpty())
                {
                    var tagetfiles = Directory.GetFiles(NEditorTools.GetRegularPath(directoryPath), filter);
                    if (tagetfiles != null && tagetfiles.Length > 0)
                    {
                        allFiles.AddRange(tagetfiles.ToList());
                    }
                }
            }
            return allFiles;
        }


        // 创建Bundle构建信息
        private BundleBuild CreateBundleBuildInfo(EBundleBuildType buildType, string tarPath, string[] assetNames)
        {
            // 添加构建信息
            var bundleBuild = new BundleBuild();
            var bundlePreName = GetBundlePreNameByBuildType(buildType);
            var fileName = Path.GetFileNameWithoutExtension(tarPath);
            bundleBuild.bundleName = bundlePreName + fileName + BundleDefaultVariantWithPoint;
            foreach (var f in assetNames)
            {
                var file = NEditorTools.GetRegularPath(f);
                bundleBuild.assetNames.Add(file);
            }
            return bundleBuild;
        }

        // 打包整个文件夹
        private BundleBuild BuildDirectoryToPack(string path, EBundleBuildType buildType)
        {
            var finalpath = NEditorTools.GetRegularPath(path);
            var files = GetAllFilesWithFilter(path, buildType);
            if (files == null || files.Count == 0)
                return null;
            var bundleBuild = CreateBundleBuildInfo(buildType, finalpath, files.ToArray());
            return bundleBuild;
        }

        // 打包图集
        private BundleBuild BuildSpriteAtlasToPack(string filePath,EBundleBuildType buildType)
        {
            var targetAssetsNames = new List<string>();
            var atlas = AssetDatabase.LoadAssetAtPath<UnityEngine.U2D.SpriteAtlas>(filePath);
            var spriteFolders = UnityEditor.U2D.SpriteAtlasExtensions.GetPackables(atlas);
            foreach (var folder in spriteFolders)
            {
                var assetPath = AssetDatabase.GetAssetPath(folder);
                var spriteFiles = Directory.GetFiles(assetPath, "*.png", SearchOption.AllDirectories);
                foreach (var sprite in spriteFiles)
                {
                    targetAssetsNames.Add(sprite);
                }
            }

            var bundleBuild = CreateBundleBuildInfo(buildType, filePath, targetAssetsNames.ToArray());
            return bundleBuild;
        }


        private BundleBuild BuildLuaBundle(string path)
        {
            var luaFiles = Directory.GetFiles(path, "*.lua", SearchOption.AllDirectories);
            var temporyFiles = new List<string>();
            foreach (var file in luaFiles)
            {
                var rootPath = Application.dataPath.Replace("Assets", "") + "/";
                var txtFile = file.Replace(".lua", ".txt");
                var finalPath = rootPath + file;
                var txtFilePath = rootPath + txtFile;
                NEditorTools.DeleteFile(txtFilePath);
                NEditorTools.CopyFile(finalPath, txtFilePath);
                temporyLuaTxtFileList.Enqueue(txtFilePath);
                temporyFiles.Add(txtFile);
            }
            AssetDatabase.Refresh();
            var bundleBuild = CreateBundleBuildInfo(EBundleBuildType.Lua, path, temporyFiles.ToArray());
            return bundleBuild;
        }

        private string GetTargetSuffixByBuildType(EBundleBuildType type)
        {
            var config = FrameworkConfig.GlobalConfig.BuildAssetCollections;

            for (int i = 0; i < config.Count; i++)
            {
                var colleciton = config[i];
                if(colleciton.buildType == type)
                {
                    return colleciton.fileFilter;
                }
            }
            return "*";
        }

        private string GetBundlePreNameByBuildType(EBundleBuildType type)
        {
            switch (type)
            {
                case EBundleBuildType.Prefab:
                    return "prefab_";
                case EBundleBuildType.Texture:
                    return "tex_";
                case EBundleBuildType.Atalas:
                    return "atlas_";
                case EBundleBuildType.Font:
                    return "font_";
                case EBundleBuildType.Scene:
                    return "scene_";
                case EBundleBuildType.Audio:
                    return "audio_";
                default:
                    return "";
            }
        }

        private bool IsValidAsset(string asset)
        {
            if (!asset.StartsWith("Assets/")) return false;
            var ext = Path.GetExtension(asset).ToLower();
            return ext != ".dll" && ext != ".cs" && ext != ".meta" && ext != ".js" && ext != ".boo";
        }

        private bool IsSceneAsset(string asset)
        {
            return asset.EndsWith(".unity");
        }

        private bool IsShaderAsset(string asset)
        {
            return asset.EndsWith(".shader");
        }

    }
}