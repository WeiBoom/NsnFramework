using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.NodeGraphView
{
    [NodeName("Float")]
    [NodePath("Base/Value/FloatNode")]
    public class FloatNode : ValueNode
    {
        [PortInfo("Float", 1, typeof(float))]
        public float floatValue;
    }
}