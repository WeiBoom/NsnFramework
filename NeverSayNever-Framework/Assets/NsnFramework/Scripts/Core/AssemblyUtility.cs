using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;

namespace Nsn
{
    public class AssemblyUtility
    {
        private static Assembly s_ExecutingAssemably;
        private static Assembly CurrentAssembly
        {
            get
            {
                if (s_ExecutingAssemably == null)
                    s_ExecutingAssemably = Assembly.GetExecutingAssembly();
                return s_ExecutingAssemably;
            }
        }

        private static readonly Dictionary<string, List<Type>> s_AssemblyTypeCacheDic = new Dictionary<string, List<Type>>();

        static AssemblyUtility()
        {
            s_AssemblyTypeCacheDic?.Clear();
        }

        /// <summary>
        /// 获取指定的程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static Assembly GetAssembly(string assemblyName)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach(var assembly in assemblies)
            {
                if (assembly.GetName().Name == assemblyName)
                    return assembly;
            }
            return null;
        }

        /// <summary>
        /// 获取指定程序集中所有的类型
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private static List<Type> GetTypes(string assemblyName)
        {
            s_AssemblyTypeCacheDic.TryGetValue(assemblyName, out var types);
            if (types != null)
            {
                return types;
            }
            else
            {
                var assembly = GetAssembly(assemblyName);
                if (assembly != null)
                {
                    types = assembly.GetTypes().ToList();
                    s_AssemblyTypeCacheDic.Add(assemblyName, types);
                    return types;
                }
            }
            return new List<Type>();
        }

        /// <summary>
        /// 获取所有带有属性标签或有继承关系标签的类
        /// </summary>
        /// <param name="assemblyName">程序集</param>
        /// <param name="attributeType">属性标签</param>
        /// <param name="parentType">父类属性标签</param>
        /// <returns></returns>
        public static List<Type> GetAttributeTypes(string assemblyName, Type attributeType, Type parentType = null)
        {
            List<Type> targetTypes = new List<Type>();
            List<Type> allTypes = GetTypes(assemblyName);
            for (int i = 0; i < allTypes.Count; i++)
            {
                Type type = allTypes[i];
                if (Attribute.IsDefined(type, attributeType))
                {
                    bool checkResult = true;
                    if(parentType != null)
                    {
                        checkResult = false;
                        if (parentType.IsAssignableFrom(type))
                        {
                            if (type.Name != parentType.Name)
                                checkResult = true;
                        }
                    }
                    if(checkResult)
                        targetTypes.Add(type);
                }
            }
            return targetTypes;
        }

        public static List<Type> GetAssignableAttributeTypes(string assemblyName, System.Type parentType, System.Type attributeType, bool checkError = true)
        {
            List<Type> result = new List<Type>();
            List<Type> cacheTypes = GetTypes(assemblyName);
            for (int i = 0; i < cacheTypes.Count; i++)
            {
                Type type = cacheTypes[i];

                // 判断属性标签
                if (Attribute.IsDefined(type, attributeType))
                {
                    // 判断继承关系
                    if (parentType.IsAssignableFrom(type))
                    {
                        if (type.Name == parentType.Name)
                            continue;
                        result.Add(type);
                    }
                    else
                    {
                        if (checkError)
                            throw new Exception($"class {type} must inherit from {parentType}.");
                    }
                }
            }
            return result;
        }

        public static object CreateInstance(string script,params object[] args)
        {
            try
            {
                var target = CurrentAssembly.CreateInstance(script,true,System.Reflection.BindingFlags.Default,null,args,null,null);
                return target;
            }
            catch (Exception e)
            {
                Debug.LogError($"创建类{script}失败 ，Exception ：{e}");
                throw;
            }
        }

        public static object CreateInstance(Assembly assembly, string script,params object[] args)
        {
            try
            {
                var target = assembly.CreateInstance(script, true, BindingFlags.Default, null, args, null, null);
                return target;
            }
            catch (Exception e)
            {
                Debug.LogError($"从程序集{assembly}中创建类{script}失败 ，Exception ：{e}");
                throw;
            }
        }

        public static Type GetType(string script)
        {
            Type type = CurrentAssembly.GetType(script);
            return type;
        }

    }
}
