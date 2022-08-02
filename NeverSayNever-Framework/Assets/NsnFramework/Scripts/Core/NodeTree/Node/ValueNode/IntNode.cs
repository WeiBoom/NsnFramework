using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace NeverSayNever.NodeGraphView
{
    [NodeName("Int")]
    [NodePath("Base/Value/IntNode")]
    public class IntNode : ValueNode
    {
        [PortInfo("Int", 1, typeof(int))]
        public int intValue;
    }
}
