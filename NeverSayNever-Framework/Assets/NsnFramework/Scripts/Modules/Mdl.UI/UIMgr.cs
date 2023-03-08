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
            // TODO , ͨ��config ��ʼ��Attribute����
        }
    }

    public class UIMgr : IUIMgr
    {
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
            if(mCurTask.IsEmpty())
            {
                mCurTask = mViewTaskQueue.Dequeue();
            }
            if (mCurTask != null && mCurTask != UIViewTask.Empty)
            {
                mCurTask.Tick();
            }
        }

        public void Open(string view, params object[] userData)
        {
            // �鿴��ǰ�Ƿ������ڽ��е�����
            UIViewTask task = mViewTaskQueue.Get(view);
            if (!task.IsEmpty())
            {
                // Situation 1 ��ǰUI�Ѿ�������ִ�е�ȻTask
                if (task.TaskType == UIViewTaskType.Close)
                {
                    NsnLog.Warning($"[NsnFramework], UIMgr.Open , {view} is closing but try open it");
                    task.Stop();
                    mViewTaskQueue.Remove(task.ViewName);
                }
                else
                {
                    task.Params = userData;
                    return;
                }
            }
            
            // Situation 2 . UI�����Ѿ���
            UIViewItem viewItem = mViewStack.Get(view);
            if(viewItem.IsPrepared())
            {
                // ��ջ����ջ���޸�UIջ��˳��,����������
                mViewStack.Pop(viewItem);
                mViewStack.Push(viewItem);
                viewItem.OnRefresh(userData);
                return;
            }

            // Situation 3 . ��ǰû��UITask
            task = AddTaskToQueue(view);
            task.Params = userData;
            
            // Situation 4 . ֻ��һ��task��ǰֱ֡��ִ��
            if(mViewTaskQueue.Count == 1)
                ExecuteTask(task);
        }

        public void Close(string view)
        {
            // todo
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
                task.RegisterCompleteCallback(OnTaskComplete);
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
            if(task.Status != UIViewTaskStatus.None)
            {
                NsnLog.Error($"task [{task.ViewID}] is running!");
                return;
            }
            task.Tick();
        }

        private void OnTaskComplete(UIViewItem viewItem)
        {
            mViewStack.Push(viewItem);
        }
    }


}
