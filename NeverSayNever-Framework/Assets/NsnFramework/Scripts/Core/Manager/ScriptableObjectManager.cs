using UnityEngine;
using System.Collections.Generic;
namespace NeverSayNever
{
    public class ScriptableObjectManager : Singleton<ScriptableObjectManager>
    {
        private Dictionary<System.Type, ScriptableObject> SOCacheDic = new Dictionary<System.Type, ScriptableObject>();

        /// <summary>
        /// 获取ScriptableObject资源,从配置路径读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private static T GetScriptableObjectAsset<T>() where T : ScriptableObject
        {
            var name = typeof(T).Name;
            var finalPath = $"Setting/{name}";
            var asset = Resources.Load<T>(finalPath);
            return asset;
        }
        
        public T GetScriptableObject<T>() where T:ScriptableObject
        {
            var type = typeof(T);
            SOCacheDic.TryGetValue(type, out var target);
            if (target != null)
                return (T)target;

            target = GetScriptableObjectAsset<T>();
            if (target != null)
            {
                SOCacheDic.Add(type, target);
                return (T)target;
            }

            return null;
        }

    }
}

