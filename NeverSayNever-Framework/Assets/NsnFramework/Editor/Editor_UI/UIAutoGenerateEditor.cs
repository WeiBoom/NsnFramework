using System;
using System.Collections.Generic;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace NeverSayNever.EditorUtilitiy
{
    [Obsolete("本来是用于自动生成界面元素的代码，但是这种方式不提倡，所以废弃了，留作学习参考")]
    public class UIAutoGenerateEditor
    {
       // [MenuItem("GameObject/NeverSayNever/生成UI属性类[CSharp]", priority = 0)]
        static void GeneratePanelAttribute()
        {
            var obj = Selection.activeGameObject;
            if (obj != null)
            {
                if (obj.GetComponent<UIPanelInfo>() == null)
                {
                    
                    Debug.LogError("非UI预制体不自动生成代码");
                    return;
                }

                GenerateAttributeClass(obj);
                // 创建后并不会立即刷新，这里手动刷新Unity
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogError(("请先选择一个Panel预制体"));
                return;
            }
        }

        // 生成一个面板属性类
        private static void GenerateAttributeClass(GameObject obj)
        {
            var trans = obj.transform;
            var className = obj.name;

            //  动态生成代码

            //准备一个代码编译器单元
            var unit = new CodeCompileUnit();
            // 属性类
            var attributeClass = BuildClass(className, true);
            // 添加固定的命名空间
            BuildNameSpace(ref unit, ref attributeClass);

            var nodeDic = GetNodeDic<Transform>(trans);
            var buttonDic = GetNodeDic<Button>(trans);
            var imgDic = GetNodeDic<Image>(trans);
            var textureDic = GetNodeDic<RawImage>(trans);
            var textDic = GetNodeDic<Text>(trans);

            // 先初始化字段
            DeclareFields(nodeDic, ref attributeClass);
            DeclareFields(buttonDic, ref attributeClass);
            DeclareFields(imgDic, ref attributeClass);
            DeclareFields(textureDic, ref attributeClass);
            DeclareFields(textDic, ref attributeClass);

            // 创建一个初始化字段的方法
            var initFieldFunc = BuildMethod("OnInitAttribute", ref attributeClass, true);
            // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
            initFieldFunc.Attributes = MemberAttributes.Family | MemberAttributes.Override;
            // 给方法添加内容
            InjectInitializeMethod(nodeDic, ref initFieldFunc);
            InjectInitializeMethod(buttonDic, ref initFieldFunc);
            InjectInitializeMethod(imgDic, ref initFieldFunc);
            InjectInitializeMethod(textureDic, ref initFieldFunc);
            InjectInitializeMethod(textDic, ref initFieldFunc);

            // 构建UI属性类
            BuildScript(className, ref unit, true);
            // 若当前不存在Panel的脚本，则主动创建一个，并生成按钮事件
            var scriptPath = FrameworkConfig.GlobalConfig;
            var panelOutputPath = scriptPath + className + "/" + className+".cs";
            // 构建UI类
            if (!System.IO.File.Exists(panelOutputPath))
                GeneratePanelClass(className, buttonDic);

        }

        // 生成一个面板Mono类
        private static void GeneratePanelClass(string className, Dictionary<string, Button> buttonDic)
        {
            //准备一个代码编译器单元
            var unit2 = new CodeCompileUnit();
            // 属性类
            var panelClass = BuildClass(className, true);
            // 添加固定的命名空间
            BuildNameSpace(ref unit2, ref panelClass);

            //var initPanelMethod = BuildMethod("OnInitialize", ref panelClass,true);
            //var startMethod = BuildMethod("OnStart", ref panelClass,true);

            // 创建一个注册事件的方法
            var registerBtnFunc = BuildMethod("RegisterButtonEvents", ref panelClass);
            // 声明每个按钮的点击事件的方法
            foreach (var v in buttonDic.Values)
            {
                string name = v.name;
                name = name.Substring(4);
                var method = BuildMethod("onclick_" + name, ref panelClass);
                InjectRegisterMethod(v, method, registerBtnFunc);
            }

            BuildScript(className, ref unit2);
        }

        // 获取组件指定的名字开头
        private static string GetNodeStartsType<T>(T type) where T : Component
        {
            var titleName = "";
            switch (type)
            {
                case Button _:
                    titleName = "btn_";
                    break;
                case Text _:
                    titleName = "txt_";
                    break;
                case Image _:
                    titleName = "img_";
                    break;
                case RawImage _:
                    titleName = "tex_";
                    break;
                case ScrollRect _:
                    titleName = "scroll_";
                    break;
                case Transform _:
                    titleName = "node_";
                    break;
            }

            return titleName;
        }

        static string GetType<T>()
        {
            return typeof(T).Name;
        }

        // 获取当前节点的路径
        private static string GetNodePath(Transform node, Transform root, string path = "")
        {
            var parent = node.parent;
            if (parent == null)
                return path;

            if (string.IsNullOrEmpty(path))
                path = node.gameObject.name;
            else
                path = node.gameObject.name + "/" + path;

            if (parent != root)
                path = GetNodePath(parent, root, path);

            return path;
        }

        // 获取指定的组件
        private static Dictionary<string, T> GetNodeDic<T>(Transform root) where T : Component
        {
            var dic = new Dictionary<string, T>();
            ForeachNode(root, root, ref dic);
            return dic;
        }

        // 声明属性
        private static void DeclareFields<T>(Dictionary<string, T> dic, ref CodeTypeDeclaration myClass) where T : Component
        {
            foreach (var k in dic.Keys)
            {
                dic.TryGetValue(k, out var v);
                BuildMemberField(v, ref myClass);
            }
        }

        // 插入初始化的方法
        private static void InjectInitializeMethod<T>(Dictionary<string, T> dic, ref CodeMemberMethod method)
            where T : Component
        {
            foreach (var k in dic.Keys)
            {
                dic.TryGetValue(k, out var v);
                if (v == null) continue;
                var left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), v.name);
                var right = new CodeVariableReferenceExpression(
                    "transform.Find(\"" + k + "\").GetComponent<" + GetType<T>() + ">()");
                var cas = new CodeAssignStatement(left, right);
                method.Statements.Add(cas);
            }
        }

        // 注册按钮事件
        private static void InjectRegisterMethod(Button btn, CodeMemberMethod btnMethod, CodeMemberMethod method)
        {
            var statements =
                new CodeSnippetStatement("\t\t" + btn.gameObject.name + ".onClick.AddListener(" + btnMethod.Name +
                                         ");");
            method.Statements.Add(statements);
        }

        // 递归遍历panel下所有指定类型的节点，并把路径和对象作为k，v添加到字典里;
        private  static void ForeachNode<T>(Transform node, Component root, ref Dictionary<string, T> componentsDic)
            where T : Component
        {
            if (node == null)
                return;
            var count = node.childCount;
            for (var i = 0; i < count; i++)
            {
                var child = node.GetChild(i);
                if (child == null) continue;
                var component = child.GetComponent<T>();
                if (component != null)
                {
                    var startName = GetNodeStartsType(component);
                    if (child.gameObject.name.StartsWith(startName))
                    {
                        var path = GetNodePath(child, root.transform);
                        componentsDic.Add(path, component);
                    }

                }

                ForeachNode(child, root, ref componentsDic);
            }
        }

        // 构建类
        private static CodeTypeDeclaration BuildClass(string className, bool isPartial = false)
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

            myClass.BaseTypes.Add(typeof(UIBasePanel));
            return myClass;
        }

        // 构建命名空间
        private  static void BuildNameSpace(ref CodeCompileUnit unit, ref CodeTypeDeclaration myClass)
        {
            //设置命名空间（这个是指要生成的类的空间）
            CodeNamespace myNamespace = new CodeNamespace();
            //导入必要的命名空间引用
            myNamespace.Imports.Add(new CodeNamespaceImport("System"));
            myNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            myNamespace.Imports.Add(new CodeNamespaceImport("UnityEngine"));
            myNamespace.Imports.Add(new CodeNamespaceImport("UnityEngine.UI"));
            //把这个类放在这个命名空间下
            myNamespace.Types.Add(myClass);
            //把该命名空间加入到编译器单元的命名空间集合中
            unit.Namespaces.Add(myNamespace);
        }

        // 构建字段
        private static void BuildMemberField<T>(T field, ref CodeTypeDeclaration myClass) where T : Component
        {
            var member = new CodeMemberField(GetType<T>(), field.gameObject.name)
            {
                Attributes = MemberAttributes.Private
            };
            myClass.Members.Add(member);
        }

        // 构建属性
        private static CodeMemberProperty BuildMemberProperty(ref CodeTypeDeclaration myClass)
        {
            var property = new CodeMemberProperty
            {
                //设置访问类型
                // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                //对象名称
                Name = "Str",          
                //有get
                HasGet = true,            
                //有set
                HasSet = true,
                //设置property的类型            
                Type = new CodeTypeReference(typeof(string))
            };

            //添加注释
            property.Comments.Add(new CodeCommentStatement("this is Str"));
            //get
            property.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "str")));
            //set
            property.SetStatements.Add(new CodeAssignStatement(
                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "str"),
                new CodePropertySetValueReferenceExpression()));

            // 添加到Customer class类中
            myClass.Members.Add(property);

            return property;
        }

        // 构建方法
        private static CodeMemberMethod BuildMethod(string methodName, ref CodeTypeDeclaration myClass, bool isOverride = false)
        {
            //添加方法
            var method = new CodeMemberMethod
            {
                Name = methodName,
                // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
                Attributes = MemberAttributes.Private | MemberAttributes.Final
            };
            //方法名
            //访问类型
            if (isOverride)
                method.Attributes = MemberAttributes.Override;

            //添加一个参数
            //method.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "number"));
            //设置返回值类型：int/不设置则为void
            //method.ReturnType = new CodeTypeReference(typeof(int));
            //设置返回值
            //method.Statements.Add(new CodeSnippetStatement(" return number+1; "));

            //将方法添加到myClass类中
            myClass.Members.Add(method);
            return method;
        }

        // 创建脚本
        private static void BuildScript(string className, ref CodeCompileUnit unit, bool isAttribute = false)
        {
            //生成C#脚本("VisualBasic"：VB脚本)
            var provider = CodeDomProvider.CreateProvider("CSharp");
            //代码风格:大括号的样式{}
            var options = new CodeGeneratorOptions { BracingStyle = "C", BlankLinesBetweenMembers = true };

            //是否在字段、属性、方法之间添加空白行
            //输出文件路径
            var scriptRoot = FrameworkConfig.GlobalConfig.UIScriptRootForCs;
            var outputPath = scriptRoot + className;
            var suffix = isAttribute ? ".Designer.cs" : ".cs";
            var outputAttributeFile = outputPath + "/" + className + suffix;

            // 逻辑脚本如果已经有了，就不再覆盖了，免得写了半天GG了
            if (!isAttribute && System.IO.File.Exists(outputAttributeFile))
            {
                Debug.Log($"文件已经存在 {outputAttributeFile}");
                return;
            }

            if (!System.IO.Directory.Exists(outputPath))
            {
                Debug.Log($"创建目录 {outputPath}");
                System.IO.Directory.CreateDirectory(outputPath); //不存在就创建目录 
            }
            //保存
            using (var sw = new System.IO.StreamWriter(outputAttributeFile))
            {
                //为指定的代码文档对象模型(CodeDOM) 编译单元生成代码并将其发送到指定的文本编写器，使用指定的选项。(官方解释)
                //将自定义代码编译器(代码内容)、和代码格式写入到sw中
                Debug.Log(outputAttributeFile);
                provider.GenerateCodeFromCompileUnit(unit, sw, options);
            }
        }
    }
}