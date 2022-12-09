using Boo.Lang;
using UnityEditor;

namespace Nsn
{
    public enum ViewTaskType
    {
        Open,
        Close,
        Pop,
        CloseWindowsPanels,
        CloseSysPanels,
        ClearHistory
    }

    public struct UIViewTask
    {
        public ViewTaskType TaskType;
        public string ViewName;
        public int ViewID;
        public UIViewAttribute ViewAttribute;
        public System.Object[] Params;

        public static UIViewTask Default => default(UIViewTask);
    }


    public class UIViewTaskQueue
    {
        private List<UIViewTask> tasks = new List<UIViewTask>(10);

        public int Count
        {
            get { return tasks.Count; }
        }

        public bool Contains(string viewName)
        {
            for(int i = 0; i < tasks.Count; i++)
            {
                UIViewTask task = tasks[i];
                if (task.ViewName == viewName)
                    return true;
            }
            return false;
        }

        public bool Contains(int viewID)
        {
            for(int i =0;i < tasks.Count; i++)
            {
                UIViewTask task = tasks[i];
                if (task.ViewID == viewID)
                    return true;
            }
            return false;
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
            return UIViewTask.Default;
        }


        private UIViewTask RemoveAt(int index)
        {
            if(index < 0 || index >= tasks.Count)
                return UIViewTask.Default;
            UIViewTask task = tasks[index];
            tasks.RemoveAt(index);
            return task;
        }
    }
}