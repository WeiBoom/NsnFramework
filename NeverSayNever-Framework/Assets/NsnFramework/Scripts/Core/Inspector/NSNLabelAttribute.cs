using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.EditorUtilitiy
{
    public class NSNLabelAttribute : PropertyAttribute
    {
        public readonly string Name;
        public NSNLabelAttribute(string name)
        {
            Name = name;
        }
    }
}