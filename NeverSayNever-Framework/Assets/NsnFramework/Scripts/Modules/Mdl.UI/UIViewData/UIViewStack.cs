using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nsn
{
    public class UIViewStack
    {
        private List<UIViewItem> m_ViewList = new List<UIViewItem>();

        public List<UIViewItem> ViewList => m_ViewList;

        public int Count => m_ViewList.Count;

        public UIViewItem Top => m_ViewList.Count > 0 ? m_ViewList[m_ViewList.Count - 1] : UIViewItem.Empty;

        public UIViewItem Bottom => m_ViewList.Count > 0 ? m_ViewList[0] : UIViewItem.Empty;

        public UIViewItem this[int index]
        {
            get
            {
                if (index < 0 || index >= m_ViewList.Count)
                    return UIViewItem.Empty;
                return m_ViewList[index];
            }
        }

        public void Add(UIViewItem viewItem)
        {
            m_ViewList.Add(viewItem);
        }


        public void Remove(string viewName)
        {
            if (!string.IsNullOrEmpty(viewName))
                Pop(viewName);
        }
        
        public void Remove(UIViewItem view)
        {
            if (view != null)
                Remove(view.ViewName);
        }

        private int GetViewStackIndex(string viewName)
        {
            for (int i = m_ViewList.Count - 1; i >= 0; i--)
            {
                if (m_ViewList[i].ViewName == viewName)
                {
                    return i;
                }
            }
            return -1;
        }
        
        public UIViewItem Get(string viewName)
        {
            int viewIndex = GetViewStackIndex(viewName);
            UIViewItem viewItem = viewIndex < 0 ? UIViewItem.Empty : m_ViewList[viewIndex];
            return viewItem;
        }

        public UIViewItem Pop(UIViewItem viewItem)
        {
            if (viewItem.IsPrepared())
                return UIViewItem.Empty;
            return Pop(viewItem.ViewName);
        }
        
        private UIViewItem Pop(string viewName)
        {
            int viewIndex = GetViewStackIndex(viewName);
            if (viewIndex > 0)
            {
                UIViewItem viewItem = m_ViewList[viewIndex];
                m_ViewList.RemoveAt(viewIndex);
                return viewItem;
            }
            return UIViewItem.Empty;
        }

        public void Push(UIViewItem viewItem)
        {
            if (!viewItem.IsPrepared())
                throw new Exception("Can't push empty UIViewItem!");
            if(Contains(viewItem.ViewName))
                throw new Exception($"{viewItem.ViewName} has exist!");

            int insertIndex = -1;
            
            for (int i = 0; i < m_ViewList.Count; i++)
            {
                if (m_ViewList[i].Layer == viewItem.Layer)
                    insertIndex = i + 1;
            }

            if (insertIndex == -1)
            {
                for (int i = 0; i < m_ViewList.Count; i++)
                {
                    if (viewItem.Layer > m_ViewList[i].Layer)
                        insertIndex = i + 1;
                }
            }
            // 空栈的情况下，默认为0，插入到首位
            insertIndex = Mathf.Max(0, insertIndex);
            m_ViewList.Insert(insertIndex, viewItem);
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= m_ViewList.Count)
                return false;

            m_ViewList.RemoveAt(index);
            return true;
        }

        public bool Contains(int viewID)
        {
            foreach (UIViewItem item in m_ViewList)
            {
                if(item.ViewID == viewID)
                    return true;
            }
            return false;
        }

        public bool Contains(string viewName)
        {
            if (string.IsNullOrEmpty(viewName))
                return false;
            foreach (UIViewItem item in m_ViewList)
            {
                if (item.ViewName.Equals(viewName))
                    return true;
            }
            return false;
        }

        public void Clear()
        {
            m_ViewList?.Clear();
        }
    }

}