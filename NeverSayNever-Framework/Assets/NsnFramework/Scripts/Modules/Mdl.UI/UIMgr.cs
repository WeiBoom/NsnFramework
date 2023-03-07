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
        // dependent mgr
        private IResMgr mResMgr;

        private Dictionary<int ,UIViewInfo> mViewInfos;
        private Dictionary<string,UIViewInfo> mViewInfosName2ID;

        private UIRoot mUIRoot;
        private UIViewStack mViewStack;
        private UIViewTaskQueue mViewTaskQueue;

        private UIViewTask mCurTask;

        public void OnInitialized(params object[] args)
        {
            mViewInfos = new Dictionary<int ,UIViewInfo>();
            mViewInfosName2ID = new Dictionary<string,UIViewInfo>();

            mViewStack = new UIViewStack();
            mViewTaskQueue = new UIViewTaskQueue();

            mResMgr = Framework.GetManager<IResMgr>();
        }

        public void OnDisposed()
        {
            mViewInfos.Clear();
            mViewInfosName2ID.Clear();

            mViewInfos = null;
            mViewInfosName2ID = null;

            mViewStack.Clear();
            mViewTaskQueue.Clear();
        }

        public void OnUpdate(float deltaTime)
        {
            if(mCurTask == null || mCurTask == UIViewTask.Empty)
            {
                mCurTask = mViewTaskQueue.Dequeue();
            }
            if (mCurTask != null && mCurTask != UIViewTask.Empty)
            {
                mCurTask.Update();
            }
        }

        public void Open(string view)
        {
            if(mViewStack.Contains(view))
            {
                mViewStack.Pop();
            }


            UIViewTask task = mViewTaskQueue.Get(view);
            // 当前没有正在加载的UI
            if(task.IsEmpty())
            {
                AddTaskToQueue(view);
            }
            else
            {
                // 如果当前UI正在关闭，又打算打开，那么停止关闭的操作
                if (task.TaskType == UIViewTaskType.Close)
                    NsnLog.Error($"[NsnFramework], UIMdl.Open , {view} is closing but try open it");
                task.TaskType = UIViewTaskType.Open;
            }

            if(mViewTaskQueue.Count == 1)
            {
                ExecuteTask(task);
            }
        }

        public void Close(string view)
        {
            if(mViewTaskQueue.Contains(view))
            {
                mViewTaskQueue.Remove(view);
            }
        }

        public bool IsOpened(string view)
        {
            if(mViewStack.Contains(view))
                return true;
            return false;
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
                mViewTaskQueue.Enqueue(task);
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
            task.Run();

            // step1 : load ui asset
            //mResMgr.LoadUIAsset(task.ViewName, OnUIAssetLoadCompleted);
        }

        private void OnUIAssetLoadCompleted(object obj)
        {
            GameObject uiObj = (GameObject)obj;
            UIView uiView = uiObj.GetComponent<UIView>();
            mViewStack.Add(uiView);
        }

    }


}
