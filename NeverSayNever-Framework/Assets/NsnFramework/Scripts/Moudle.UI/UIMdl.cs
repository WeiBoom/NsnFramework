using NeverSayNever;
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
        private UIViewAttribute attribute;
        private UIViewConfig config;

        public int ID => config.ID;
        public string Name => config.Name;

        public UIViewInfo(UIViewConfig config)
        {
            this.config = config;
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
            UIViewTask task = m_UITaskQueue.Get(view);
            // task is not empty
            if(!task.Equals(UIViewTask.Empty))
            {
                if(task.TaskType == UIViewTaskType.Close)
                {
                    NsnLog.Error($"[NsnFramework], UIMdl.Open , {view} is closing but try open it");
                }
                task.TaskType = UIViewTaskType.Open;
            }
            else
            {
                AddTaskToQueue(view);
            }
        }

        public void Close(string view)
        {
        }

        private UIViewTask AddTaskToQueue(string view)
        {
            m_ViewInfosName2ID.TryGetValue(view, out UIViewInfo viewInfo);
            if (viewInfo != null)
            {
                UIViewTask task = new UIViewTask()
                {
                    TaskType = UIViewTaskType.Open,
                    ViewName = viewInfo.Name,
                    ViewID = viewInfo.ID,
                };
                m_UITaskQueue.Enqueue(task);
                return task;
            }
            else
            {
                NsnLog.Error($"[NsnFramework], UIMdl.Open , {view} doesn't exist in ViewInfo Dictionary");
            }
            return UIViewTask.Empty;
        }

        private void AddViewToStack()
        {
            // todo
        }

    }


}
