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

        public UIViewItem Add(UIView view,int viewID = 0)
        {
            if (view == null) return UIViewItem.Empty;

            UIViewItem item = new UIViewItem()
            {
                ViewName = view.ViewInfo.ViewName,
                ViewID = viewID,
                View = view,
            };
            m_ViewList.Add(item);
            return item;
        }

        public UIViewItem Remove(UIView view)
        {
            if(view == null) return UIViewItem.Empty;
            for (int i = m_ViewList.Count - 1; i >= 0; i--)
            {
                if (m_ViewList[i].View == view)
                {
                    UIViewItem viewItem = m_ViewList[i];
                    m_ViewList.RemoveAt(i);
                    return viewItem;
                }
            }
            return UIViewItem.Empty;
        }

        public UIViewItem Pop()
        {
            if (m_ViewList.Count > 0)
            {
                int index = m_ViewList.Count - 1;
                UIViewItem viewItem = m_ViewList[index];
                m_ViewList.RemoveAt(index);
                return viewItem;
            }
            return UIViewItem.Empty;
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

        public bool Contains(UIView view)
        {
            foreach (UIViewItem item in m_ViewList)
            {
                if (item.View == view)
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