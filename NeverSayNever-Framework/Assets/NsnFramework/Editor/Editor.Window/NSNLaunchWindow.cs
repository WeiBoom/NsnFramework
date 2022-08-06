using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;

namespace NeverSayNever.EditorUtilitiy
{
    using NeverSayNever;
    public class NSNLaunchWindow : OdinEditorWindow
    {
        private static NSNLaunchWindow _window;
        private static GUIStyle _style;

        // 生成的UI脚本的命名空间
        private string _uiScirptNamespace;
        // 在project视图显示文件大小
        private static bool _showFileSize;
        // 当前显示状态
        private bool _currentShowFileSize;

        // 目前没有使用window的必要，减少操作，只提供初始化配置的接口
        [MenuItem("NeverSayNever/Launch")]
        private static void InitWindow()
        {
            if (_window == null)
                _window = GetWindow<NSNLaunchWindow>(false, "NSN 全局配置", true);
            _window.Show();

            if (_style == null)
            {
                _style = new GUIStyle
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 40,
                    fixedHeight = 50,
                    stretchWidth = false,
                    normal =
                    {
                        textColor = Color.white
                    }
                };
            }
        }

        [MenuItem("NeverSayNever/Initialize")]
        public static void InitializeFrameworkConfig()
        {
            GenerateAssetPathConfig();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        protected override void OnEnable()
        {
            _currentShowFileSize = _showFileSize;
        }

        protected override void OnGUI()
        {
            GUILayout.Label("NSN Framework",_style);
            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            GUILayout.Label($"框架配置文件存放路径 ： {Framework.ScriptableObjectAssetRootPath}");
            EditorGUILayout.EndHorizontal();

            // 在project视图显示文件大小
            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            GUILayout.Label($"在Project视图显示文件大小(有消耗)");
            _showFileSize = EditorGUILayout.Toggle(_showFileSize);
            EditorGUILayout.EndHorizontal();

            // 代码命名空间
            EditorGUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label($"自动生成的脚本的命名空间，不需要则为空");
            _uiScirptNamespace = EditorGUILayout.TextField("UI脚本命名空间", _uiScirptNamespace);
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
            if (GUILayout.Button("一键生成默认全局配置"))
            {
                GenerateAssetPathConfig();
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            UpdateSetting();
        }

        private void UpdateSetting()
        {
            if(_currentShowFileSize != _showFileSize)
            {
                _currentShowFileSize = _showFileSize;
                if (_showFileSize)
                    NSNFilesSizeInspector.OpenPlaySize();
                else
                    NSNFilesSizeInspector.ClosePlaySize();
            }
        }

        /// <summary>
        /// 生成资源路径配置
        /// </summary>
        private static void GenerateAssetPathConfig()
        {
            // 生成UI自动收集的规则配置
            NEditorTools.GenerateScriptableObjectAsset<NsnUIElementsCollectRule>();

            var assetCfg = NEditorTools.GetScriptableObjectAsset<NsnGlobalAssetConfig>();
            if (assetCfg == null)
            {
                // 生成打包资源配置
                NEditorTools.GenerateScriptableObjectAsset<NsnGlobalAssetConfig>();
                assetCfg = NEditorTools.GetScriptableObjectAsset<NsnGlobalAssetConfig>();

                assetCfg.VariesAssetFolderDic.Add("UI", InitializeAssetFolderInfo("Prefab/UIPanels", ".prefab"));
                assetCfg.VariesAssetFolderDic.Add("Model", InitializeAssetFolderInfo("Prefab/Models", ".prefab"));
                assetCfg.VariesAssetFolderDic.Add("Effect", InitializeAssetFolderInfo("Prefab/Effects", ".prefab"));
                assetCfg.VariesAssetFolderDic.Add("Texture", InitializeAssetFolderInfo("Texture", ".png"));
                assetCfg.VariesAssetFolderDic.Add("Audio", InitializeAssetFolderInfo("Audio", ".mp3"));
                assetCfg.VariesAssetFolderDic.Add("Font", InitializeAssetFolderInfo("Common/Fonts", ".asset"));
                assetCfg.VariesAssetFolderDic.Add("Shader", InitializeAssetFolderInfo("Common/Shaders", ".shader"));
                assetCfg.VariesAssetFolderDic.Add("TextAsset", InitializeAssetFolderInfo("Config", ".txt"));
            }
        }

        private static NsnGlobalAssetConfig.AssetFolderInfo InitializeAssetFolderInfo(string folderName,string extension)
        {
            var folderInfo = new NsnGlobalAssetConfig.AssetFolderInfo();
            folderInfo.extension = extension;
            folderInfo.folder = AssetDatabase.LoadAssetAtPath("NsnFramework/Examples/DataRes/" + folderName, typeof(UnityEngine.Object));
            if(folderInfo.folder != null)
            {
                folderInfo.path = AssetDatabase.GetAssetPath(folderInfo.folder);
            }
            return folderInfo;
        }
        
    }
}
