using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{
    public class FrameworkCore: Singleton<FrameworkCore>
    {
        public IEventManager mEventMgr = new EventManager();

        private static Dictionary<string, IManager> mManagerDic;

        public static void Initialize()
        {
            mManagerDic = new Dictionary<string, IManager>(); 
            Inst.AddManager<IResourceMgr>();
            Inst.AddManager<IEventManager>();
            Inst.AddManager<IUIMgr>();
            Inst.AddManager<ILuaMgr>();
            Inst.AddManager<ITimerMgr>();
            Inst.AddManager<IFSMMgr>();
        }

        /// <summary>
        /// 更新游戏内逻辑
        /// </summary>
        public void Update(float deltaTime)
        {
            UpdateMgr(deltaTime);
        }

        public T GetManager<T>() where T : IManager
        {
            string key = typeof(T).ToString();
            mManagerDic.TryGetValue(key, out IManager mgr);
            return (T)mgr;
        }


        private void AddManager<T>() where T : IManager
        {
            System.Reflection.Assembly assembly = typeof(T).Assembly;
            System.Type[] types = assembly.GetTypes();
            {
                foreach (var type in types)
                {
                    if (!type.IsAbstract && type.GetInterfaces().Length > 0)
                    {
                        string typeStr = type.ToString();
                        object inst = NsnRuntime.CreateInstance(typeStr);
                        T script = (T)inst;
                        if (script != null)
                            mManagerDic.Add(typeStr, script);
                    }
                }
            }
        }


        private void UpdateMgr(float deltaTime)
        {
            var e = mManagerDic.GetEnumerator();
            while (e.MoveNext())
            {
                e.Current.Value.OnUpdate(deltaTime);
            }
            e.Dispose();
        }
    }

}
