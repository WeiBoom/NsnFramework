using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    public class UIMgr : IUIMgr
    {
        private Dictionary<int ,UIViewInfo> mViewInfos = new Dictionary<int ,UIViewInfo>();
        private Dictionary<string,UIViewInfo> mViewInfosName2ID  = new Dictionary<string,UIViewInfo>();

        private UIRoot mUIRoot;
        private UIViewStack mViewStack = new UIViewStack();
        private UIViewTaskQueue mUITaskQueue = new UIViewTaskQueue();

        private IResMgr mResMgr;

        private UIViewTask mCurTask;


        public void OnInitialized(params object[] args)
        {
        }

        public void OnDisposed()
        {
        }

        public void OnUpdate(float deltaTime)
        {
            if (mCurTask != null && mCurTask != UIViewTask.Empty)
            {
                mCurTask.Update();
            }
        }


        public void Open(string view)
        {
            UIViewTask task = mUITaskQueue.Get(view);
            if(task == UIViewTask.Empty)
            {
                AddTaskToQueue(view);
            }
            else
            {
                if (task.TaskType == UIViewTaskType.Close)
                    NsnLog.Error($"[NsnFramework], UIMdl.Open , {view} is closing but try open it");
                task.TaskType = UIViewTaskType.Open;
            }

            if(mUITaskQueue.Count == 1)
            {
                ExecuteTask(task);
            }
        }

        public void Close(string view)
        {
        }

        private UIViewTask AddTaskToQueue(string view)
        {
            mViewInfosName2ID.TryGetValue(view, out UIViewInfo viewInfo);
            if (viewInfo != null)
            {
                UIViewTask task = new UIViewTask()
                {
                    TaskType = UIViewTaskType.Open,
                    ViewName = viewInfo.Name,
                    ViewID = viewInfo.ID,
                };
                mUITaskQueue.Enqueue(task);
                return task;
            }
            else
            {
                NsnLog.Error($"[NsnFramework], UIMdl.Open , {view} doesn't exist in ViewInfo Dictionary");
            }
            return UIViewTask.Empty;
        }

        private void ExecuteTask(UIViewTask task)
        {
            if(task.Running || task.Completed)
            {
                NsnLog.Error($"task [{task.ViewID}] is running!");
                return;
            }
            task.Running = true;

            //mResMgr.LoadUIAsset(task.ViewName, OnUIAssetLoadCompleted);
        }

        private void OnUIAssetLoadCompleted(object obj)
        {

        }

    }


}
