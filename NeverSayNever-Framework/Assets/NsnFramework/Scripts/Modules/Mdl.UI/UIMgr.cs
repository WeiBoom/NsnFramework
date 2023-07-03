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
        }
    }

    public class UIMgr : IUIMgr
    {
        private UIRoot m_UIRoot;
        private UIViewStack m_UIViewStack;
        private UIViewTaskQueue m_UIViewTaskQueue;

        private Dictionary<string, UIViewInfo> m_UIViewInfoDic;

        private UIViewTask mCurTask;

        public void OnInitialized(params object[] args)
        {
            m_UIViewInfoDic = new Dictionary<string, UIViewInfo>();
            m_UIViewStack = new UIViewStack();
            m_UIViewTaskQueue = new UIViewTaskQueue();

            // todo 
            // 1、UI模块初始化后，需要加载UIViewInfo，初始化各项配置的UI信息
            // 2、配合UIMgr，需要先在工具库新增UI配置相关的工具
        }

        public void OnDisposed()
        {
            m_UIViewInfoDic.Clear();
            m_UIViewInfoDic = null;
            
            m_UIViewStack.Clear();
            m_UIViewStack = null;

            m_UIViewTaskQueue.Clear();
            m_UIViewTaskQueue = null;
        }

        public void OnUpdate(float deltaTime)
        {
            if(mCurTask.IsEmpty())
            {
                mCurTask = m_UIViewTaskQueue.Dequeue();
            }
            if (mCurTask != null && mCurTask != UIViewTask.Empty)
            {
                mCurTask.Tick();
            }
        }

        public void Open(string view, params object[] userData)
        {
            UIViewTask task = m_UIViewTaskQueue.Get(view);
            // Situation 1 当前存在加载任务
            if (!task.IsEmpty())
            {
                // Situation 1.1 task正在执行关闭操作,则停止,并移除队列中
                if (task.TaskType == UIViewTaskType.Close)
                {
                    NsnLog.Warning($"[NsnFramework], UIMgr.Open , {view} is closing but try open it");
                    task.Stop();
                    m_UIViewTaskQueue.Remove(task.ViewName);
                }
                else
                {
                    // Situation 1.2 更新task数据, 不做其他任何处理
                    NsnLog.Warning($"[NsnFramework], UIMgr.Open , {view} is opening, just update userdata");
                    task.Params = userData;
                    return;
                }
            }
            
            // Situation 2 . 已经存在UI Item
            UIViewItem viewItem = m_UIViewStack.Get(view);
            if(viewItem.IsPrepared())
            {
                m_UIViewStack.Pop(viewItem);
                m_UIViewStack.Push(viewItem);
                viewItem.OnRefresh(userData);
                return;
            }

            // Situation 3 . 不存在UI，重新创建加载task
            task = AddTaskToQueue(view);
            if (task.IsEmpty())
                return;

            task.Params = userData;
            
            // Situation 4 . ֻ如果只有一个任务，则立即执行
            if(m_UIViewTaskQueue.Count == 1)
                ExecuteTask(task);
        }

        public void Close(string view)
        {
            UIViewItem viewItem = m_UIViewStack.Get(view);
            if (!viewItem.IsPrepared())
                return;

            m_UIViewStack.Remove(viewItem);
            viewItem.OnClose();
            
        }

        public bool IsOpened(string view)
        {
            if(m_UIViewStack.Contains(view))
                return true;
            return false;
        }


        private UIViewTask AddTaskToQueue(string view)
        {
            m_UIViewInfoDic.TryGetValue(view, out UIViewInfo viewInfo);
            if (viewInfo != null)
            {
                UIViewTask task = new UIViewTask()
                {
                    TaskType = UIViewTaskType.Open,
                    ViewName = viewInfo.Name,
                    ViewID = viewInfo.ID,
                };
                task.RegisterCompleteCallback(OnTaskComplete);
                m_UIViewTaskQueue.Enqueue(task);
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
            if(task.Status != UIViewTaskStatus.None)
            {
                NsnLog.Error($"task [{task.ViewID}] is running!");
                return;
            }
            task.Tick();
        }

        private void OnTaskComplete(UIViewItem viewItem)
        {
            m_UIViewStack.Push(viewItem);
        }

    }

}
