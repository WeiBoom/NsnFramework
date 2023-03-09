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

        public void Stop()
        {
            UnRegisterCompleteCallback();
            m_Status = UIViewTaskStatus.End;
        }

        public void RegisterCompleteCallback(System.Action<UIViewItem> callback) => m_TaskCompleteCallback = callback;

        public void UnRegisterCompleteCallback() => m_TaskCompleteCallback = null;

    }

    public static class UIViewTaskExtent
    {
        public static bool IsEmpty(this UIViewTask task)
        {
            return task == UIViewTask.Empty;
        }
    }
}