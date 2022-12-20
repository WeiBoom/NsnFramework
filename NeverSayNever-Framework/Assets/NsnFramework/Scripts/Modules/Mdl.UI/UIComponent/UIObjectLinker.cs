using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace Nsn
{
    [RequireComponent(typeof(UIBaseView))]
    public class UIObjectLinker : UIBase
    {
        public enum LinkedObjectType
        {
            GameObj,
            Transfrom,
            Button,
            Image,
            Texture,
            Label,
            InputField,
            Grid,
            Scroll,
            Slider,
        }

        [Serializable]
        public struct LinkedObject
        {
            public string name;
            public LinkedObjectType type;
            public UnityEngine.Object obj;

            public T GetObject<T>() where T : UnityEngine.Object
            {
                var target = GetObject();
                return target as T;
            }

            public UnityEngine.Object GetObject() => obj;
        }

        [SerializeField]
        protected Dictionary<string,UnityEngine.Object> mFixedLinkedObjectDic = new Dictionary<string,UnityEngine.Object>();
        [SerializeField]
        protected Dictionary<string, UnityEngine.Object> mDynamicFixedObjectDic = new Dictionary<string, UnityEngine.Object>();

        private void CollectControl()
        {

        }
    }
}
