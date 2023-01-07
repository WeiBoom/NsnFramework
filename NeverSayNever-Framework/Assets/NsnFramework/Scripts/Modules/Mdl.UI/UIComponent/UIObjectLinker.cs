using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nsn
{

    [RequireComponent(typeof(UIBaseView))]
    public class UIObjectLinker : UIBase
    {
        [SerializeField]
        protected Dictionary<string, UnityEngine.Component> mLinkedObjectDic;

        /// <summary>
        /// 获取指定的组件
        /// </summary>
        private void CollectControl()
        {

        }
    }
}
