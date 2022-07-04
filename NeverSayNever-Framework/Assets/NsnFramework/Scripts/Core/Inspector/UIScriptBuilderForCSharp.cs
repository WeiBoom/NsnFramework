using System;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.EditorUtilitiy
{
    public static class UIScriptBuilderForCSharp
    {
        enum EScriptLanguageType
        {
            CSharp,
            Lua,
        }
        enum ECsScriptModuleType
        {
            Item,
            Panel,
            Attribute,
            Messenger,
        }

        // 根据类型获取生成的脚本的文件名
        private static string GetModuleFileName(string moduleName,ECsScriptModuleType moduleType)
        {
            var suffix = ".cs";
            switch (moduleType)
            {
                case ECsScriptModuleType.Item :
                    suffix = "";
                    break;
                case ECsScriptModuleType.Attribute :
                    suffix = "Init";
                    break;
                case ECsScriptModuleType.Panel :
                    suffix = "Panel";
                    break;
                case ECsScriptModuleType.Messenger :
                    suffix = "Messenger";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(moduleType), moduleType, null);
            }
            return moduleName + suffix;
        }
        
        // 构建命名空间
        private static void BuildNameSpace(ref CodeCompileUnit unit, ref CodeTypeDeclaration myClass)
        {
            //设置命名空间（这个是指要生成的类的空间）
            CodeNamespace myNamespace = new CodeNamespace();
            //导入必要的命名空间引用
            myNamespace.Imports.Add(new CodeNamespaceImport("System"));
            myNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            myNamespace.Imports.Add(new CodeNamespaceImport("UnityEngine"));
            myNamespace.Imports.Add(new CodeNamespaceImport("UnityEngine.UI"));
            myNamespace.Imports.Add(new CodeNamespaceImport("TMPro"));
            myNamespace.Imports.Add(new CodeNamespaceImport("NeverSayNever"));

            //if (Framework.GlobalConfig != null && !Framework.GlobalConfig.UIScriptNamespace.IsNullOrEmpty())
            //{
            //    //把这个类放在这个命名空间下
            //    myNamespace.Name = Framework.GlobalConfig.UIScriptNamespace;
            //}

            myNamespace.Types.Add(myClass);
            //把该命名空间加入到编译器单元的命名空间集合中
            unit.Namespaces.Add(myNamespace);
        }

        // 构建类
        private static CodeTypeDeclaration BuildClass<T>(string className, bool isPartial = false)
        {
            //Code:代码体
            var myClass = new CodeTypeDeclaration(className)
            {
                //指定为类
                IsClass = true,
                //指定为partial
                IsPartial = isPartial,
                //设置类的访问类型
                TypeAttributes = TypeAttributes.Public
            };

            myClass.BaseTypes.Add(typeof(T));
            return myClass;
        }

        // 创建一个构造函数
        private static void BuildConstructor(ref CodeTypeDeclaration myClass, string name)
        {
            // 创建一个构造器
            var constructor = new CodeConstructor {Attributes = MemberAttributes.Public};
            // 添加一个string类型的参数
            constructor.Parameters.Add(new CodeParameterDeclarationExpression("System.String", "name"));
            // 基类方法的调用
            constructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression("name"));
            // 添加构造函数
            myClass.Members.Add(constructor);
        }

        // 构建方法
        private static CodeMemberMethod BuildMethod(string methodName, ref CodeTypeDeclaration myClass, bool isOverride = false)
        {
            //添加方法
            var method = new CodeMemberMethod
            {
                Name = methodName,
                Attributes = isOverride ?
                    (MemberAttributes.Family | MemberAttributes.Override) :
                    (MemberAttributes.Private | MemberAttributes.Final),
            };
            // 添加基类方法调用
            if (isOverride)
                method.Statements.Add(new CodeVariableReferenceExpression($"base.{methodName}()"));
            myClass.Members.Add(method);
            return method;
        }

        // 声明ui组件并在 方法中初始化
        private static void BuildAttributeFromUIElements(List<UIComponentItem> list,ref CodeTypeDeclaration declaration,ref CodeMemberMethod method)
        {
            foreach (var item in list)
            {
                var element = item.element;
                // 先 声明一个属性
                BuildMemberField(element, ref declaration);
                // 在方法中获取这个字段
                var left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), item.key.ToLower());
                var right = new CodeVariableReferenceExpression($"Info.Collection[\"{item.key}\"] as {element.GetType()}");
                var cas = new CodeAssignStatement(left, right);
                method.Statements.Add(cas);
            }
        }

        // 构建字段
        private static void BuildMemberField<T>(T field, ref CodeTypeDeclaration myClass) where T : Component
        {
            var member = new CodeMemberField(field.GetType().Name, field.gameObject.name.ToLower())
            {
                Attributes = MemberAttributes.Private
            };
            myClass.Members.Add(member);
        }

        // 创建脚本
        private static void BuildScript(string moduleName, ref CodeCompileUnit unit, ECsScriptModuleType scriptModuleType)
        {
            //生成C#脚本("VisualBasic"：VB脚本)
            var provider = CodeDomProvider.CreateProvider("CSharp");
            //代码风格:大括号的样式{}
            var options = new CodeGeneratorOptions { BracingStyle = "C", BlankLinesBetweenMembers = true };
            // 文件名
            var outPutFolder = moduleName + "Module";
            //是否在字段、属性、方法之间添加空白行
            //输出文件路径
            var outputFolderPath = FrameworkConfig.GlobalConfig.UIScriptRootForCs + outPutFolder;
            var fileName = GetModuleFileName(moduleName, scriptModuleType);
            var outputFilePath = outputFolderPath + "/" + fileName + ".cs";

            // 逻辑脚本如果已经有了，就不再覆盖了，免得写了半天GG了
            if (scriptModuleType != ECsScriptModuleType.Attribute && System.IO.File.Exists(outputFilePath))
            {
                Debug.Log($"文件已经存在 {outputFilePath}");
                return;
            }

            if (!System.IO.Directory.Exists(outputFolderPath))
            {
                Debug.Log($"创建目录 {outputFolderPath}");
                System.IO.Directory.CreateDirectory(outputFolderPath); //不存在就创建目录 
            }
            //保存
            using (var sw = new System.IO.StreamWriter(outputFilePath))
            {
                //为指定的代码文档对象模型(CodeDOM) 编译单元生成代码并将其发送到指定的文本编写器，使用指定的选项。(官方解释)
                //将自定义代码编译器(代码内容)、和代码格式写入到sw中
                Debug.Log(outputFilePath);
                provider.GenerateCodeFromCompileUnit(unit, sw, options);
            }
        }

        /// <summary>
        /// 生成UI对象对应的Cs脚本
        /// </summary>
        /// <param name="uiTarget"></param>
        /// <param name="isPanel"></param>
        public static void BuildCSharpScriptForPanel(UIBaseBehaviour uiTarget, bool isPanel = true)
        {
#if UNITY_EDITOR
            var objName = uiTarget.gameObject.name;
            var moduleName = isPanel ? objName.Substring(0, objName.Length - 5) : objName;

            // --------------- Step 1 ------------------ 

            var uiType = isPanel ? ECsScriptModuleType.Panel : ECsScriptModuleType.Item;
            var uiFileName = GetModuleFileName(moduleName, uiType);

            // 准备一个代码编译器单元
            var partUnit = new CodeCompileUnit();
            // 先创建一个属性类，作为核心类的partical存在，只用于获取组件
            var attributeClass = BuildClass<UIBasePanel>(uiFileName, true);
            // 添加固定的命名空间
            BuildNameSpace(ref partUnit, ref attributeClass);

            // 创建一个初始化字段的方法
            var initFieldFunc = BuildMethod("OnInitAttribute", ref attributeClass, true);

            // 声明固定UI组件属性
            BuildAttributeFromUIElements(uiTarget.fixedElements, ref attributeClass, ref initFieldFunc);
            BuildAttributeFromUIElements(uiTarget.dynamicElements, ref attributeClass, ref initFieldFunc);
            // 构建脚本
            BuildScript(moduleName, ref partUnit, ECsScriptModuleType.Attribute);

            // --------------- Step 2 ------------------ 
            // 准备一个代码编译器单元
            var coreUnit = new CodeCompileUnit();
            // 先创建一个核心类
            var coreClass = BuildClass<UIBasePanel>(uiFileName, true);
            // 添加固定的命名空间
            BuildNameSpace(ref coreUnit, ref coreClass);

            // 创建一个初始化字段的方法
            BuildMethod("OnAwake", ref coreClass, true);
            BuildMethod("OnStart", ref coreClass, true);
            // 构建脚本
            BuildScript(moduleName, ref coreUnit, uiType);

            if (isPanel)
            {
                // --------------- Step 3 ------------------ 
                // 创建一个UIMessenger类，用于注册UI,只有UI面板需要注册
                // 准备一个代码编译器单元
                var messengerUnit = new CodeCompileUnit();
                // 命名为xxMessenger
                var messengerFileName = GetModuleFileName(moduleName, ECsScriptModuleType.Messenger);
                // 创建一个消息类
                var messengerClass = BuildClass<UIPanelMessenger>(messengerFileName);
                // 添加命名空间
                BuildNameSpace(ref messengerUnit, ref messengerClass);
                // 创建构造函数
                var messengerName = GetModuleFileName(moduleName, ECsScriptModuleType.Messenger);
                BuildConstructor(ref messengerClass, messengerName);
                // 构建脚本
                BuildScript(moduleName, ref messengerUnit, ECsScriptModuleType.Messenger);
            }

            UnityEditor.EditorUtility.SetDirty(uiTarget.gameObject);
            // 刷新Unity
            UnityEditor.AssetDatabase.Refresh();
            UnityEditor.AssetDatabase.SaveAssets();

#endif
        }

    }
}
