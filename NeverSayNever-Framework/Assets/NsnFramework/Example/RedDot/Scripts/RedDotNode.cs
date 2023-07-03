using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Nsn.Example
{
    public class RedDotNode
    {
        public string Name;
        public int Count;
        public RedDotNode Parent;
        public List<RedDotNode> Children;
        public GameObject Go;
        public GameObject ParentGo;

        public RedDotNode(string name, RedDotNode root)
        {
            Name = name;
            Parent = root;
        }
        
        // 注册红点
        public void Register(GameObject parentGo)
        {
            Transform goTrans = FindChild(parentGo.transform);
            if (goTrans)
                Go = goTrans.gameObject;
            else
            {
                Go = GetRDObject();
                if (Go != null)
                {
                    Go.name = "RedDot";
                    SetRedDotPosition();
                }
            }
        }

        // 注销
        public void UnRegister()
        {
            Go = null;
        }
        
        // 刷新红点提示
        public void Refresh()
        {
            if (Go == null) return;

            bool show = IsValid();
            Go.SetActive(show);
        }

        public void AddChild(string name)
        {
            if (Children == null)
                Children = new List<RedDotNode>();
            var node = new RedDotNode(name, this);
            Children.Add(node);
        }

        public RedDotNode GetChild(string name)
        {
            foreach (var VARIABLE in Children)
            {
                if (VARIABLE.Name == name)
                    return VARIABLE;
            }
            return null;
        }

        public void SetValid(bool isValid)
        {
            if (IsValid() == isValid && Children.Count <= 0)
                return;

            int addCount = isValid ? 1 : -1;
            Count += addCount;
            
            Refresh();
            if(Parent != null)
                Parent.SetValid(isValid);
        }

        // 是否有效
        public bool IsValid()
        {
            return Count > 0;
        }
        
        // 查找指定的红点节点的接口，测试用
        private Transform FindChild(Transform root)
        {
            return root.Find("RedDot");
        }

        // 根据不同的红点配置，获取不同的红点预制体
        private GameObject GetRDObject()
        {
            return Resources.Load<GameObject>("RedDot_Normal");
        }
        
        // 设置默认位置
        private void SetRedDotPosition()
        {
            RectTransform rect = Go.GetComponent<RectTransform>();
            rect.anchorMax = Vector2.one;
            rect.anchorMin = Vector2.one;
            rect.anchoredPosition = new Vector2(-16, -16);
        }
        
    }
    
    
}