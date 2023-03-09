using Nsn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsn
{
    public class UIViewTaskQueue
    {
        private List<UIViewTask> tasks = new List<UIViewTask>(10);

        public int Count
        {
            get { return tasks.Count; }
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
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
            if (!string.IsNullOrEmpty(viewName))
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
            if (length > 0)
                return RemoveAt(0);
            return UIViewTask.Empty;
        }

        public void Remove(string viewName)
        {
            if (!string.IsNullOrEmpty(viewName))
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
            if (index < 0 || index >= tasks.Count)
                return UIViewTask.Empty;
            UIViewTask task = tasks[index];
            tasks.RemoveAt(index);
            return task;
        }
    }
}
