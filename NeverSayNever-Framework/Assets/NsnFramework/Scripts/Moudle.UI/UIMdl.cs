using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nsn
{
    [System.Serializable]
    public class UIViewConfig
    {
        public int ID;
        public string Name;
        public int LayerID;
    }

    [System.Serializable]
    public class UIViewInfo
    {
        private UIViewAttribute Attribute;
        private UIViewConfig Config;

        public UIViewInfo(UIViewConfig config)
        {
            Config = config;
            // TODO , 通过config 初始化Attribute数据
        }
    }


    public interface IUIMdl
    {
        void Open(string view);
        void Close(string view);
    }

    public class UIMdl : IUIMdl
    {

        private Dictionary<int ,UIViewInfo> m_ViewInfos = new Dictionary<int ,UIViewInfo>();
        private Dictionary<string,UIViewInfo> m_ViewInfosName2ID  = new Dictionary<string,UIViewInfo>();

        private UIRoot m_Root;
        private UIViewStack m_ViewStack = new UIViewStack();
        private UIViewTaskQueue m_UITaskQueue = new UIViewTaskQueue();


        public void Open(string view)
        {
            if(m_UITaskQueue.Contains(view))
                return;
            // todo
        }

        public void Close(string view)
        {
        }

        private UIViewTask CreateTask(string view)
        {
            UIViewTask task = new UIViewTask()
            {

            };

            return task;
        }

    }


}
