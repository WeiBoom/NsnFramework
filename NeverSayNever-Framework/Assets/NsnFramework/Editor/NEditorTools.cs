using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nsn.EditorToolKit
{
    public static class NEditorTools
    {
        /// <summary>
        /// 获取规范路径
        /// </summary>
        /// <param name="path">原路径</param>
        /// <returns></returns>    
        public static string GetRegularPath(string path)
        {
            return path.Replace('\\', '/').Replace("\\", "/");
        }

        /// <summary>
        /// 获取项目工程路径
        /// </summary>
        /// <returns></returns>
        public static string GetProjectPath()
        {
            var projectPath = Path.GetDirectoryName(Application.dataPath);
            return GetRegularPath(projectPath);
        }

        #region Dictonary / File

        /// <summary>
        /// 打开指定路径的文件夹
        /// </summary>
        /// <param name="folerPath"></param>
        public static void OpenFolder(string folerPath)
        {
            #if UNITY_EDITOR_WIN
            
            if (!System.IO.Directory.Exists(folerPath))
            {
                Debug.LogError("目录不存在 ： " + folerPath);
                return;
            }

            folerPath = GetRegularPath(folerPath);
            System.Diagnostics.Process.Start("explorer.exe", folerPath);
            
            #endif
        }

        /// <summary>
        /// 生成指定的目录并创建文件，
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void CreateFileDirectory(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (string.IsNullOrEmpty(directory))
            {
                Debug.LogError("所需创建的文件路径为空");
                return;
            }

            if (!Directory.Exists(directory))
            {
                Debug.Log($"创建文件！ 路径：{directory}");
                Directory.CreateDirectory(directory ?? "");
            }
            else
            {
                Debug.LogWarning($"文件夹已经存在！ 路径：{directory}");
            }
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="folderPath"></param>
        public static void CreateDirectory(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                Debug.LogError("所需创建的文件夹路径为空");
                return;
            }
            
            if (!Directory.Exists(folderPath))
            {
                Debug.Log($"创建文件！ 路径：{folderPath}");
                Directory.CreateDirectory(folderPath ?? "");
            }
            else
            {
                Debug.LogWarning($"文件夹已经存在！ 路径：{folderPath}");
            }
        }

        /// <summary>
        /// 拷贝文件夹（包括子所有子目录文件）
        /// </summary>
        /// <param name="source">源文件夹路径</param>
        /// <param name="target">目标路径</param>
        public static void CopyFolder(string source, string target)
        {
            source = GetRegularPath(source);

            CreateFileDirectory(target);
            // 获得源目录下的所有文件
            var fileList = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories);
            // 遍历文件夹下的所有文件，创建目录并拷贝到对应文件夹下
            foreach (var file in fileList)
            {
                var temp = GetRegularPath(file);
                var savePath = temp.Replace(source, target);
                CopyFile(file, savePath, true);
            }
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="source">源文件路径</param>
        /// <param name="target">目标路径</param>
        /// <param name="overwrite">是否重写文件</param>
        private static void CopyFile(string source, string target, bool overwrite)
        {
            if (!File.Exists(source)) throw new FileNotFoundException(source);
            // 创建目录
            CreateFileDirectory(target);
            // 复制文件
            File.Copy(source, target, overwrite);
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="path1">源文件全路径</param>
        /// <param name="path2">目标文件全路径</param>
        public static void CopyFile(string path1, string path2)
        {
            int bufferSize = 10240;

            Stream source = new FileStream(path1, FileMode.Open, FileAccess.Read);
            Stream target = new FileStream(path2, FileMode.Create, FileAccess.Write);

            byte[] buffer = new byte[bufferSize];
            int bytesRead;
            do
            {
                bytesRead = source.Read(buffer, 0, bufferSize);
                target.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);

            source.Dispose();
            target.Dispose();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleteFile(string filePath)
        {
            var fileName = GetRegularPath(filePath);
            if (File.Exists(fileName))
                File.Delete(fileName);
        }

        /// <summary>
        /// 清空指定目录下的所有文件
        /// </summary>
        /// <param name="directoryPath"></param>
        public static void ClearFiles(string directoryPath)
        {
            // 获取目录下所有的文件
            var allFiles = Directory.GetFiles(directoryPath);
            // 删除文件
            for (var i = 0; i < allFiles.Length; i++)
            {
                var filePath = GetRegularPath(allFiles[i]);
                File.Delete(filePath);
            }
        }
        
        /// <summary>
        /// 清空文件夹
        /// </summary>
        /// <param name="directoryPath">文件夹目录</param>
        public static void ClearFolder(string directoryPath)
        {
            if (!Directory.Exists(directoryPath)) return;

            ClearFiles(directoryPath);

            // 获取目录下所有的文件夹
            var allDirectories = Directory.GetDirectories(directoryPath);
            // 删除文件夹
            for (var i = 0; i < allDirectories.Length; i++)
            {
                var childDirectory = GetRegularPath(allDirectories[i]);
                ClearFiles(childDirectory);
                Directory.Delete(childDirectory);
            }
        }
        
        #endregion
        
        #region Time
        
        /// <summary>
        /// 获取当前的时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetCurTimeStamp()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        } 
        
        #endregion

        #region AssetBundle

        /// <summary>
        /// 根据打包目标平台获取相应的平台名
        /// </summary>
        /// <param name="target">指定的打包目标平台</param>
        /// <returns></returns>
        public static string GetPlatformForAssetBundles(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.StandaloneOSX:
                    return "OSX";
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return "Windows";
                case BuildTarget.iOS:
                    return "iOS";
                case BuildTarget.Android:
                    return "Android";
                case BuildTarget.WebGL:
                    return "WebGL";
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取当前的平台名
        /// </summary>
        /// <returns></returns>
        public static string GetPlatformName()
        {
            return GetPlatformForAssetBundles(EditorUserBuildSettings.activeBuildTarget);
        }

        #endregion
    }
}