using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace NeverSayNever.BehaviourTree
{
    [NodeName("StringList")]
    [NodePath("Base/Value/StringListNode")]
    public class StringListNode : ValueNode
    {
        [PortInfo("FloatList", 1, typeof(List<float>))]
        public List<string> stringList = new List<string>();
    }
}
