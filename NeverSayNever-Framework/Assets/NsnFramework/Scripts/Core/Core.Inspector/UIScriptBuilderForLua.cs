using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.EditorUtilitiy
{
    public class UIScriptBuilderForLua
    {
        private const string templete_moduleName = "ModuleName";
        private const string templete_AttributeName = "AttributeName";
        private const string templete_PanelName = "PanelName";
        private const string templete_MessengerName = "MessengerName";

        private static string targetModuleName = "";
        private static string targetPanelName = "";
        private static string targetAttributeName = "";
        private static string targetMessengerName = "";

        /// <summary>
        /// 生成UI对象对应的Lua脚本
        /// </summary>
        /// <param name="uiTarget"></param>
        public static void BuildLuaScriptForPanel(UIBaseBehaviour uiTarget,bool isPanel)
        {
#if UNITY_EDITOR
            var objName = uiTarget.gameObject.name;
            var moduleName = isPanel ? objName.Substring(0, objName.Length - 5) : objName;
            var moduleFolder = moduleName + "Module";
            var outputFolederPath = Framework.GlobalConfig.LuaSciprtDirectory + "/UI/" + moduleFolder;

            if (!Directory.Exists(outputFolederPath))
            {
                Debug.Log($"创建目录 {outputFolederPath}");
                System.IO.Directory.CreateDirectory(outputFolederPath); //不存在就创建目录 
            }

            BuildLuaPanel(uiTarget, moduleName,outputFolederPath);

            UnityEditor.AssetDatabase.Refresh();
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }

#if UNITY_EDITOR

        private static void BuildLuaPanel(UIBaseBehaviour uiTarget, string moduleName, string outputPath)
        {
            targetModuleName = moduleName;
            targetAttributeName = moduleName + "Attribute";
            targetPanelName = moduleName + "Panel";
            targetMessengerName = moduleName + "Messenger";
            var templetePath = Application.dataPath.Replace("Assets", "") + UnityEditor.AssetDatabase.GetAssetPath(Framework.GlobalConfig.LuaTempleteFolder);

            // Step.1 拷贝并写入UI初始化代码
            var attributeFile = outputPath + "/" + targetAttributeName + ".lua";
            //三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
            File.Copy(templetePath + "/UIPanelAttributeForLuaTemplete.txt", attributeFile, true);
            var allText = ReadAllText(attributeFile);
            allText = allText.Replace("--Content", GetAllElementsString(uiTarget));
            WriteAllText(attributeFile, allText);

            // Step.2 拷贝并写入UI逻辑代码
            var panelFile = outputPath + "/" + targetPanelName + ".lua";
            if (!File.Exists(panelFile))
            {
                File.Copy(templetePath + "/UIPanelForLuaTemplete.txt", panelFile, false);
                allText = ReadAllText(panelFile);
                WriteAllText(panelFile, allText);
            }

            // Step.3 拷贝并写入UIMessenger代码
            var messengerFile = outputPath + "/" + targetMessengerName + ".lua";
            if (!File.Exists(messengerFile))
            {
                File.Copy(templetePath + "/UIPanelMessengerForLuaTemplete.txt", messengerFile, false);
                allText = ReadAllText(messengerFile);
                WriteAllText(messengerFile, allText);
            }
        }
#endif


        private static string GetAllElementsString(UIBaseBehaviour uiTarget)
        {
            var uiElements = new List<string>();
            GetUIElements(uiTarget.fixedElements, ref uiElements);
            GetUIElements(uiTarget.dynamicElements, ref uiElements);

            string s = "";
            foreach (var str in uiElements)
            {
                s += str + "\n";
            }
            return s;
        }

        private static string ReadAllText(string targetFile)
        {
            StreamReader reader = new StreamReader(targetFile, Encoding.Default);
            var allText = reader.ReadToEnd();
            reader.Close();
            allText = allText.Replace(templete_moduleName, targetModuleName)
                                .Replace(templete_PanelName, targetPanelName)
                                .Replace(templete_AttributeName, targetAttributeName)
                                .Replace(templete_MessengerName, targetMessengerName);
            return allText;
        }

        private static void WriteAllText(string targetFile, string allText)
        {
            StreamWriter writer = new StreamWriter(targetFile, false, Encoding.Default);
            writer.Write(allText);
            writer.Flush();
        }


        private static void GetUIElements(List<UIComponentItem> elements, ref List<string> list)
        {
            foreach (var element in elements)
            {
                if (element == null)
                    continue;
                if (element.key.IsNullOrEmpty() || element.element == null)
                {
                    throw new System.Exception("Element is null. Please check your ui elements in inspector");
                }
                var itemName = element.key.ToLower();
                var preName = itemName.Split('_')[0];
                string str = $"\tthis.{itemName} = this.UIInfos:GetUICollection(\"{itemName}\")";//:GetComponent(\"{GetUITargetType(preName)}\");";
                list.Add(str);
            }
        }

    }
}