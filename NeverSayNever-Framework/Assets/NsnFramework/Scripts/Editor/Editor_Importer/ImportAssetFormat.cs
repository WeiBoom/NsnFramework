using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.U2D;

using UnityEngine;
using UnityEngine.U2D;

using Object = UnityEngine.Object;



namespace NeverSayNever.Editors
{

    public enum EImportAssetType
    {
        Texture,
        SpriteAtlas,
    }

    public class ImportAssetFormat : AssetPostprocessor
    {
        public const string SuffixSpriteAtlas = ".spriteatlas";
        
        private void OnPreprocessTexture()
        {
#if USING_ASTC
            var texImporter = assetImporter as TextureImporter;
            SetImporterFormat(texImporter, ImportAssetType.spriteAtlas,TextureImporterFormat.ASTC_RGBA_8x8);
#elif USING_ETC
            var texImporter = assetImporter as TextureImporter;
            SetImporterFormat(texImporter, ImportAssetType.spriteAtlas,TextureImporterFormat.ETC2_RGBA8Crunched);
#endif
        }

        private void OnPreprocessAsset()
        {
#if USING_ASTC
            SetSpriteAtlasFormat(this.assetPath, TextureImporterFormat.ASTC_RGBA_8x8);
#elif USING_ETC
            SetSpriteAtlasFormat(this.assetPath, TextureImporterFormat.ETC2_RGBA8Crunched);
#endif
        }

        private void SetSpriteAtlasFormat(string targetAssetPath, TextureImporterFormat targetFormat)
        {
            string extension = Path.GetExtension(assetPath);
            if (extension == SuffixSpriteAtlas)
            {
                TextureCheckerEditor.TargetTexIpFormat = targetFormat;
                TextureCheckerEditor.FormatSpriteAtlas(targetAssetPath);
            }
        }

        private void SetImporterFormat(TextureImporter texImporter, EImportAssetType assetType, TextureImporterFormat targetFormat)
        {
            if (texImporter == null)
                return;
            // 设置安卓
            var androidSetting = texImporter.GetPlatformTextureSettings("Android");
            androidSetting.format = targetFormat;
            androidSetting.overridden = true;
            texImporter.SetPlatformTextureSettings(androidSetting);

            // 设置iOS
            var iosSetting = texImporter.GetPlatformTextureSettings("iPhone");
            iosSetting.format = targetFormat;
            iosSetting.overridden = true;
            texImporter.SetPlatformTextureSettings(iosSetting);
        }
    }


    public static class TextureCheckerEditor
    {
        public static TextureImporterFormat TargetTexIpFormat { get; set; }

        [MenuItem("NeverSayNever/Tools/Texture/转换所有图集和图片的格式(ASTC_RGBA_8x8)")]
        public static void ChangeTextureFormatToASTC_RGBA_8x8()
        {
            if (!EditorUtility.DisplayDialog("Texture Format", "确定转换所有的图片(图集)格式 为 ASTC_RGBA_8x8 ？", "搞快", "算了")) return;
            TargetTexIpFormat = TextureImporterFormat.ASTC_RGBA_8x8;
            ChangeTexturesFormat();
        }

        [MenuItem("NeverSayNever/Tools/Texture/转换所有图集和图片的格式(RGBA_Crunched_ETC2)")]
        public static void ChangeTextureFormatToRGBA_Crunched_ETC2()
        {
            if (!EditorUtility.DisplayDialog("Texture Format", "确定转换所有的图片(图集)格式 为 RGBA_Crunched_ETC2 ？", "搞快", "算了")
            ) return;
            TargetTexIpFormat = TextureImporterFormat.ETC2_RGBA8Crunched;
            ChangeTexturesFormat();
        }
        private static void ChangeTexturesFormat()
        {
            // 转换所有的.png的格式
            var texturesPathList = new List<string>();
            GetDirs(Application.dataPath, ".png", ref texturesPathList);
            EditorUtility.DisplayProgressBar("转换格式", "0/" + texturesPathList.Count, 0);//创建进度条
            ChangeSelectedTextureFormat(texturesPathList, FormatTextureProgress);

            // 转换所有的spriteAtlas的格式
            var atlasList = new List<string>();
            EditorUtility.DisplayProgressBar("转换格式", "0/" + atlasList.Count, 0);//创建进度条
            GetDirs(Application.dataPath, ImportAssetFormat.SuffixSpriteAtlas, ref atlasList);
            ChangeSelectedTextureFormat(atlasList, FormatSpriteAtlas);


            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("Complete", "修改已完成", "确定");
        }

        private static IEnumerable<Object> GetSelectedObjects()
        {
            var selections = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            return selections;
        }

        /// <summary>
        /// 获取当前路径下所有指定后缀的文件
        /// </summary>
        /// <param name="dirPath">指定路径</param>
        /// <param name="suffix">指定后缀</param>
        /// <param name="dirs">引用的缓存列表</param>
        private static void GetDirs(string dirPath, string suffix, ref List<string> dirs)
        {
            dirs.AddRange(from path in Directory.GetFiles(dirPath) where System.IO.Path.GetExtension(path) == suffix select path.Substring(path.IndexOf("Assets", StringComparison.Ordinal)));

            if (Directory.GetDirectories(dirPath).Length <= 0) return;
            foreach (var path in Directory.GetDirectories(dirPath))
            {
                GetDirs(path, suffix, ref dirs);
            }
        }

        private static void ChangeSelectedTextureFormat(IReadOnlyCollection<string> texturesPath, Action<string, int, int> action)
        {
            var length = texturesPath.Count;
            var count = 0;
            foreach (var path in texturesPath)
            {
                action?.Invoke(path, count, length);
                count++;
            }
        }

        public static string FormatTexture(string path,TextureImporterFormat androidFormat)
        {
            var texImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            if (texImporter != null)
            {
                var defaultSetting = new TextureImporterPlatformSettings
                {
                    name = "Default",
                    maxTextureSize = 2048,
                    overridden = false,
                    format = TextureImporterFormat.Automatic,
                };
                var androidSetting = new TextureImporterPlatformSettings
                {
                    name = "Android",
                    overridden = true,
                    format = androidFormat,
                    androidETC2FallbackOverride = AndroidETC2FallbackOverride.UseBuildSettings,
                };
                texImporter.SetPlatformTextureSettings(defaultSetting);
                texImporter.SetPlatformTextureSettings(androidSetting);

                AssetDatabase.WriteImportSettingsIfDirty(path);
                return texImporter.name;
            }

            return string.Empty;
        }

        private static void FormatTextureProgress(string path, int count, int total)
        {
            var texName = FormatTexture(path, TargetTexIpFormat);
            if(!texName.IsNullOrEmpty())
            {
                var info = $"正在转换\" {texName} \" 当前进度 ：{count}/{total}";
                EditorUtility.DisplayProgressBar("转换格式", info, (float)count / total);//创建进度条
            }
            else
            {
                Debug.Log(path);
            }
        }

        private static void FormatSpriteAtlas(string path, int count, int total)
        {
            var name = FormatSpriteAtlas(path);
            var info = $"正在转换\" {name} \" 当前进度 ：{count}/{total}";
            EditorUtility.DisplayProgressBar("转换格式", info, (float)count / total);//创建进度条
        }

        public static string FormatSpriteAtlas(string path)
        {
            SpriteAtlas template = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(path);

            var name = template.name;
            var targetFormat = TargetTexIpFormat;
            if (targetFormat == TextureImporterFormat.ASTC_RGBA_8x8 && name.StartsWith("Atlas") == false)
            {
                targetFormat = TextureImporterFormat.ASTC_RGBA_6x6;
            }

            var defaultSetting = new TextureImporterPlatformSettings
            {
                name = "Default",
                format = TextureImporterFormat.Automatic,
            };
            var androidSetting = new TextureImporterPlatformSettings
            {
                name = "Android",
                format = targetFormat,
                overridden = true,
            };
            template.SetPlatformSettings(androidSetting);
            return name;
        }

    }
}