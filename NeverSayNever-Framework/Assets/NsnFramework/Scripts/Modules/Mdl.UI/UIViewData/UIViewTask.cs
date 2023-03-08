using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nsn
{
    public enum UIViewTaskType
    {
        Open,
        Close,
    }

    public enum UIViewTaskStatus
    {
        None,
        Running,
        Complete,
        End,
    }

    public struct UIViewTask
    {
        public UIViewTaskType TaskType;
        public string ViewName;
        public int ViewID;
        public object[] Params;

        private UIViewTaskStatus m_Status;
        public UIViewTaskStatus Status => m_Status;

        private System.Action<UIViewItem> m_TaskCompleteCallback;
        
        public static UIViewTask Empty => default;

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is UIViewTask)
            {
                var task = (UIViewTask)obj;
                return task.ViewID == ViewID && task.ViewName == ViewName;
            }
            return false;
        }

        public static bool operator ==(UIViewTask left, UIViewTask right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(UIViewTask left, UIViewTask right)
        {
            return !left.Equals(right);
        }

        public void Stop()
        {
            m_Status = UIViewTaskStatus.End;
        }

        public void Tick()
        {
            if (m_Status == UIViewTaskStatus.None)
            {
                m_Status = UIViewTaskStatus.Running;
                Framework.GetManager<IResMgr>().LoadAsset<GameObject>(
                    ViewName, OnComplete);
            }
            
            if (m_Status == UIViewTaskStatus.Running)
            {

            }

            if (m_Status == UIViewTaskStatus.Complete)
            {
                m_Status = UIViewTaskStatus.End;
            }
        }

        private void OnComplete(object obj)
        {     
            m_Status = UIViewTaskStatus.Complete;       

            UIViewItem viewItem = new UIViewItem(this.ViewName, this.ViewID);
            viewItem.OnCreate((GameObject)obj);
            viewItem.OnRefresh(this.Params);
            m_TaskCompleteCallback?.Invoke(viewItem);
        }

        public void RegisterCompleteCallback(System.Action<UIViewItem> callback)
        {
            m_TaskCompleteCallback = callback;
        }
    }

    public class UIViewTaskQueue
    {
        private List<UIViewTask> tasks = new List<UIViewTask>(10);

        public int Count
        {
            get { return tasks.Count; }
        }

        public void Clear()
        {
            for(int  i =0; i < Count; i++)
            {
                var task = tasks[i];
                task.Stop();
            }
            tasks.Clear();
        }

        public bool Contains(string viewName) => Get(viewName) != UIViewTask.Empty;

        public bool Contains(int viewID) => Get(viewID) != UIViewTask.Empty;

        public UIViewTask Get(string viewName)
        {
            if(!string.IsNullOrEmpty(viewName))
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    UIViewTask task = tasks[i];
                    if (task.ViewName.Equals(viewName))
                        return task;
                }
            }
            return UIViewTask.Empty;
        }

        public UIViewTask Get(int viewID)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                UIViewTask task = tasks[i];
                if (task.ViewID == viewID)
                    return task;
            }
            return UIViewTask.Empty;
        }


        public void Enqueue(UIViewTask task)
        {
            tasks.Add(task);
        }

        public UIViewTask Dequeue()
        {
            int length = tasks.Count;
            if(length > 0 )
                return RemoveAt(0);
            return UIViewTask.Empty;
        }

        public void Remove(string viewName)
        {
            if(!string.IsNullOrEmpty(viewName))
            {
                int index = -1;
                for (int i = 0; i < tasks.Count; i++)
                {
                    UIViewTask task = tasks[i];
                    if (task.ViewName == viewName)
                    {
                        index = i;
                        break;
                    }
                }
                if (index >= 0)
                    RemoveAt(index);
            }
        }

        private UIViewTask RemoveAt(int index)
        {
            if(index < 0 || index >= tasks.Count)
                return UIViewTask.Empty;
            UIViewTask task = tasks[index];
            tasks.RemoveAt(index);
            return task;
        }
    }

    public static class UIViewTaskExtent
    {
        public static bool IsEmpty(this UIViewTask task)
        {
            return task == UIViewTask.Empty;
        }
    }
}