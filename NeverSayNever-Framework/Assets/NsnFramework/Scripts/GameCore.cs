using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{
    public class GameCore: Singleton<GameCore>
    {
        public IEventManager mEventDispatcher = new EventManager();

        private Dictionary<string, IManager> mManagerDic;

        public void Initialize()
        {
            mManagerDic = new Dictionary<string, IManager>();
        }

        public void AddManager<T>() where T : IManager
        {
            System.Reflection.Assembly assembly = typeof(T).Assembly;
            System.Type[] types = assembly.GetTypes();
            {
                foreach (var type in types)
                {
                    if (!type.IsAbstract && type.GetInterfaces().Length > 0)
                    {

                    }
                }
                //mManagerDic.Add(typeof(T).ToString(), typeof(T).Assembly.);
            }
        }

        public T GetManager<T>() where T: IManager
        {
            string key = typeof(T).ToString();
            mManagerDic.TryGetValue(key, out IManager mgr);
            return (T)mgr;
        }

        /// <summary>
        /// 更新游戏内逻辑
        /// </summary>
        public void UpdateLogic()
        {
            UpdateMgr(UnityEngine.Time.deltaTime);
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
