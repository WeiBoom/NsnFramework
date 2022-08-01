using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace NeverSayNever.BehaviourTree
{
    [NodeName("String")]
    [NodePath("Base/Value/StringNode")]
    public class StringNode : ValueNode
    {
        [PortInfo("String", 1, typeof(string))]
        public string stringValue;
    }
}
