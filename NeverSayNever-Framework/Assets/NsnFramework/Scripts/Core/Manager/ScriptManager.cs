using System;
using UnityEngine;
using System.Reflection;

namespace NeverSayNever
{
    public class ScriptManager : Singleton<ScriptManager>
    {
        public System.Reflection.Assembly GameAssembly
        {
            get;
            private set;
        }
        
        public override void OnInitialize(params object[] args)
        {
            base.OnInitialize(args);
            if (GameAssembly == null)
                GameAssembly = Instance.GetType().Assembly;
        }

        public static System.Type GetType(string script)
        {
            var type = Instance.GameAssembly.GetType(script);
            if (type == null)
                type = System.Reflection.Assembly.GetExecutingAssembly().GetType(script);
            return type;
        }

        public Component AddScript(GameObject obj, string script)
        {
            return obj.AddComponent(GetType(script));
        }

        public Component AddScript(MonoBehaviour mono, string script)
        {
            return AddScript(mono.gameObject, script);
        }

        public Component AddScript<T>(MonoBehaviour mono) where T : Component
        {
            return AddScript<T>(mono.gameObject);
        }
        
        public Component AddScript<T>(GameObject obj) where T : Component
        {
            return obj.AddComponent<T>();
        }

        public object CreateInstance(string script,params object[] args)
        {
            try
            {
                var target = Instance.GameAssembly.CreateInstance(script,true,System.Reflection.BindingFlags.Default,null,args,null,null);
                return target;
            }
            catch (Exception e)
            {
                Debug.LogError($"创建类{script}失败 ，Exception ：{e}");
                throw;
            }
        }

        public object CreateInstance(Assembly assembly, string script,params object[] args)
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
