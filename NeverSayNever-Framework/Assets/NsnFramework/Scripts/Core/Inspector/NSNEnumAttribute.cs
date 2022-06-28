using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.EditorUtilitiy
{
    public class NSNEnumAttribute : PropertyAttribute
    {
        /// <summary> 枚举名称 </summary>
        public readonly string Name;

        private int[] _order;

        public NSNEnumAttribute(string name, params int[] order)
        {
            Name = name;
            _order = order;
        }
    }
}