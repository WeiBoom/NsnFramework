using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{
    public class GameCore: Singleton<GameCore>
    {

        private Dictionary<string, IManager> mManagerDic;

        public void Initialize()
        {
            mManagerDic = new Dictionary<string, IManager>();
        }

        public void AddManager<T>() where T: IManager
        {
            //mManagerDic.Add(typeof(T).ToString(), typeof(T).Assembly.);
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
            UpdateMgr();
        }

        private void UpdateMgr()
        {
            var e = mManagerDic.GetEnumerator();
            while (e.MoveNext())
            {
                e.Current.Value.OnUpdate();
            }
            e.Dispose();
        }
    }

}
