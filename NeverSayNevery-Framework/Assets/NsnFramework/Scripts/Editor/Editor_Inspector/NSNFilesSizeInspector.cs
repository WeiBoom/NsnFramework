using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NeverSayNever.Editors
{

    public class NSNFilesSizeInspector
    {
        private const string REMOVE_STR = "Assets";
        private const string FILESIZE = "FileSize";

        private static readonly int mRemoveCount = REMOVE_STR.Length;
        private static readonly Color mColor = new Color(106 / 255f, 106 / 255f, 106 / 255f, 1);
        private static Dictionary<string, string> DirSizeDictionary = new Dictionary<string, string>();
        private static List<string> DirList = new List<string>();
        private static bool isShowSize = true;

        private static string[] suffix = new[] { "", "K", "M", "G", "T", "P", "E", "Z", "Y" };

        public static bool GetShowSizeState()
        {
            isShowSize = EditorPrefs.GetBool(FILESIZE);
            return isShowSize;
        }

        public static void OpenPlaySize()
        {
            if (isShowSize == true) return;
            EditorPrefs.SetBool(FILESIZE, true);
            isShowSize = true;
            GetPropjectDirs();
        }

        public static void ClosePlaySize()
        {
            if (isShowSize == false) return;
            EditorPrefs.SetBool(FILESIZE, false);
            isShowSize = false;
            Init();
        }

        [InitializeOnLoadMethod]
        private static void InitializeOnLoadMethod()
        {
            Init();
            EditorApplication.projectChanged += GetPropjectDirs;
            EditorApplication.projectWindowItemOnGUI += OnGUI;
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            GetPropjectDirs();
        }

        private static void GetPropjectDirs()
        {
            Init();
            if (isShowSize == false) return;
            GetAllDirecotries(Application.dataPath);
            foreach (string path in DirList)
            {
                DirSizeDictionary.Add(NEditorTools.GetRegularPath(path), GetFormatSizeString((int)GetDirectoriesSize(path)));
            }
        }
        private static void Init()
        {
            GetShowSizeState();
            DirSizeDictionary.Clear();
            DirList.Clear();
        }

        private static void OnGUI(string guid, Rect selectionRect)
        {
            if (isShowSize == false) return;
            var dataPath = Application.dataPath;
            var startIndex = dataPath.LastIndexOf(REMOVE_STR);
            var dir = dataPath.Remove(startIndex, mRemoveCount);
            var path = dir + AssetDatabase.GUIDToAssetPath(guid);
            string text;
            if (DirSizeDictionary.ContainsKey(path))
            {
                text = DirSizeDictionary[path];
            }
            else if (File.Exists(path))
            {
                var fileInfo = new FileInfo(path);
                var fileSize = fileInfo.Length;
                text = GetFormatSizeString((int)fileSize);
            }
            else
            {
                return;
            }



            var label = EditorStyles.label;
            var content = new GUIContent(text);
            var width = label.CalcSize(content).x + 10;

            var pos = selectionRect;
            pos.x = pos.xMax - width;
            pos.width = width;
            pos.yMin++;

            var color = GUI.color;
            GUI.color = mColor;
            GUI.DrawTexture(pos, EditorGUIUtility.whiteTexture);
            GUI.color = color;
            GUI.Label(pos, text);
        }

        private static string GetFormatSizeString(int size, int p = 1024, string specifier = "#,##0.##")
        {
            int index = 0;

            while (size >= p)
            {
                size /= p;
                index++;
            }

            return string.Format(
                "{0} {1}B",
                size.ToString(specifier),
                index < suffix.Length ? suffix[index] : "-"
            );
        }

        private static void GetAllDirecotries(string dirPath)
        {
            if (Directory.Exists(dirPath) == false)
            {
                return;
            }
            DirList.Add(dirPath);
            DirectoryInfo[] dirArray = new DirectoryInfo(dirPath).GetDirectories();
            foreach (DirectoryInfo item in dirArray)
            {
                GetAllDirecotries(item.FullName);
            }
        }

        private static long GetDirectoriesSize(string dirPath)
        {
            if (Directory.Exists(dirPath) == false)
            {
                return 0;
            }

            long size = 0;
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            foreach (FileInfo info in dir.GetFiles())
            {
                size += info.Length;
            }

            DirectoryInfo[] dirBotton = dir.GetDirectories();
            foreach (DirectoryInfo info in dirBotton)
            {
                size += GetDirectoriesSize(info.FullName);
            }
            return size;
        }
    }
}