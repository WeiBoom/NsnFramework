namespace Nsn.EditorToolKit
{
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    
    public class NsnPopupField<T> : VisualElement
    {
        private List<T> m_list;

        private int m_Index;
        private Action<ChangeEvent<T>> m_OnValueChaned;

        private Button m_ItemMenu;
        private Label m_ItemLabel;

        public T Value
        {
            get
            {
                if (m_list == null || m_list.Count == 0)
                    return default(T);
                return m_list[m_Index];
            }
        }

        public NsnPopupField(string label, List<T> list = null, int index = 0,
            Action<ChangeEvent<T>> onValueChanged = null)
        {
            style.flexDirection = FlexDirection.Row;
            style.justifyContent = Justify.SpaceBetween;
            style.alignItems = Align.Center;

            m_OnValueChaned = onValueChanged;

            var visualTreeAsset = VEToolKit.LoadVEAssetVisualTree("VEPopupField");
            var popupList = visualTreeAsset.CloneTree();
            Add(popupList);
            popupList.StretchToParentWidth();
            m_ItemMenu = popupList.Q<Button>("ItemMenu");
            m_ItemLabel = popupList.Q<Label>("ItemLabel");
            m_ItemLabel.text = label;
            ListUpdate(list);
        }

        public void RegisterValueChangedEvent(Action<ChangeEvent<T>> cb)
        {
            m_OnValueChaned = cb;
        }

        /// <summary>
        /// 更新PopupList的列表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        public void ListUpdate(List<T> list, int index = 0)
        {
            if (list == null)
                list = new List<T>();
            m_list = list;

            m_ItemMenu.clickable = new Clickable(() =>
            {
                var menu = new GenericMenu();
                foreach (T item in list)
                {
                    menu.AddItem(new GUIContent(item.ToString()), m_Index == m_list.IndexOf(item),
                        () => { SetValue(list.IndexOf(item)); });
                }

                menu.DropDown(m_ItemMenu.worldBound);
            });

            SetValue(index);
        }

        //设置PopupList显示的值
        private void SetValue(int index)
        {
            index = Mathf.Max(Mathf.Min(m_list.Count - 1, index), 0);
            T previousValue = m_list[m_Index];
            T newValue = m_list[index];
            m_Index = index;
            m_ItemMenu.text = newValue == null ? string.Empty : newValue.ToString();
            ChangeEvent<T> ce = ChangeEvent<T>.GetPooled(previousValue, newValue);
            m_OnValueChaned?.Invoke(ce);
        }
    }
}