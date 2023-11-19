using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Nsn
{
    [System.Serializable]
    public class UIViewAttribute
    {
        public UIViewType ViewType;
        public int PanelOrder;
        public bool IsFullScreen;
        public bool IsFocusable;
        public bool MaskVisible;
        public string ViewName;
    }

    [System.Serializable]
    public class UIViewInfo
    {
        private UIViewAttribute attribute;

        private int m_ID;
        private string m_Name;

        public int ID => m_ID;
        public string Name => m_Name;

        public UIViewInfo(int id ,string name)
        {
            m_ID = id;
            m_Name = name;
        }
    }

    public class UIMgr : IUIMgr
    {
        private UIRoot m_UIRoot;
        // UI的配置
        private Dictionary<int, UIViewInfo> m_UIRegisterInfo;
        // UI缓存的栈
        private UIViewStack m_UIViewStack;
        // UI操作队列
        private UIViewTaskQueue m_UIViewTaskQueue;

        private UIViewTask mCurTask;

        public Camera UICamera2D { get; }
        public Vector2 DesignResolution { get; }


        public void OnInitialized(params object[] args)
        {
            m_UIRegisterInfo = new Dictionary<int, UIViewInfo>();
            m_UIViewStack = new UIViewStack();
            m_UIViewTaskQueue = new UIViewTaskQueue();

            
            UIViewTask.RegisterCompleteCallback(OnTaskComplete);
            // todo 
            // 1、UI模块初始化后，需要注册UI
            // 2、配合UIMgr，需要先在工具库新增UI配置相关的工具
        }

        public void OnDisposed()
        {
            m_UIRegisterInfo.Clear();
            m_UIRegisterInfo = null;
            
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
        
        
        public void Register(int viewID)
        {
            m_UIRegisterInfo.TryGetValue(viewID, out var viewInfo);
            if(viewInfo != null) {
                Debug.LogError($"[UIMgr] ViewID [{viewID}] has been registed!");
                return;
            }
            string viewName = string.Empty;
            m_UIRegisterInfo.Add(viewID, new UIViewInfo(viewID, viewName));
        }

        public void Open(int viewID, params object[] userData)
        {
            
            UIViewTask task = m_UIViewTaskQueue.Get(viewID);
            // Situation 1 当前存在加载任务
            if (!task.IsEmpty())
            {
                // Situation 1.1 task正在执行关闭操作,则停止,并移除队列中
                if (task.TaskType == UIViewTaskType.Close)
                {
                    NsnLog.Warning($"[NsnFramework], UIMgr.Open , ViewID[{viewID}] is closing but try open it");
                    task.Stop();
                    m_UIViewTaskQueue.Remove(task.ViewName);
                }
                else
                {
                    // Situation 1.2 更新task数据, 不做其他任何处理
                    NsnLog.Warning($"[NsnFramework], UIMgr.Open , ViewID[{viewID}] is opening, just update userdata");
                    task.Params = userData;
                    return;
                }
            }
            
            // Situation 2 . 已经存在UI Item
            UIViewItem viewItem = m_UIViewStack.Get(viewID);
            if(viewItem.IsPrepared())
            {
                m_UIViewStack.Pop(viewItem);
                m_UIViewStack.Push(viewItem);
                viewItem.OnRefresh(userData);
                return;
            }

            // Situation 3 . 不存在UI，重新创建加载task
            task = AddOpenTaskToQueue(viewID);
            if (task.IsEmpty())
                return;

            task.Params = userData;
            
            // Situation 4 . ֻ如果只有一个任务，则立即执行
            if(m_UIViewTaskQueue.Count == 1)
                ExecuteTask(task);
        }

        public void Close(int viewID)
        {
            UIViewItem viewItem = m_UIViewStack.Get(viewID);
            if (!viewItem.IsPrepared())
                return;

            m_UIViewStack.Remove(viewItem);
            viewItem.OnClose();
            
        }

        public bool IsOpened(int viewID)
        {
            if(m_UIViewStack.Contains(viewID))
                return true;
            return false;
        }


        private UIViewTask AddOpenTaskToQueue(int viewID)
        {
            m_UIRegisterInfo.TryGetValue(viewID, out var viewInfo);
            if (viewInfo != null)
            {
                UIViewTask task = new UIViewTask()
                {
                    TaskType = UIViewTaskType.Open,
                    ViewName = viewInfo.Name,
                    ViewID = viewInfo.ID,
                };
                m_UIViewTaskQueue.Enqueue(task);
                return task;
            }
            else
            {
                NsnLog.Error($"[NsnFramework], UIMdl.Open , ViewID[{viewID}] doesn't exist in ViewInfo Dictionary");
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
