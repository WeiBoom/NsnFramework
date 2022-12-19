using System;
using UnityEngine;
using System.Reflection;

namespace Nsn
{
    public class RuntimeAssembly
    {

        public static System.Type GetType(string script)
        {
            Type type = null;//GameAssembly.GetType(script);
            if (type == null)
                type = System.Reflection.Assembly.GetExecutingAssembly().GetType(script);
            return type;
        }

        public static Component AddScript(GameObject obj, string script)
        {
            return obj.AddComponent(GetType(script));
        }

        public static Component AddScript(MonoBehaviour mono, string script)
        {
            return AddScript(mono.gameObject, script);
        }

        public static Component AddScript<T>(MonoBehaviour mono) where T : Component
        {
            return AddScript<T>(mono.gameObject);
        }
        
        public static Component AddScript<T>(GameObject obj) where T : Component
        {
            return obj.AddComponent<T>();
        }

        public static object CreateInstance(string script,params object[] args)
        {
            try
            {
                var target = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(script,true,System.Reflection.BindingFlags.Default,null,args,null,null);
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
                var target = assembly.CreateInstance(script,true,System.Reflection.BindingFlags.Default,null,args,null,null);
                return target;
            }
            catch (Exception e)
            {
                Debug.LogError($"从程序集{assembly}中创建类{script}失败 ，Exception ：{e}");
                throw;
            }
        }

    }
}
