using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverSayNever.NodeGraphView
{
    [NodeName("FloatList")]
    [NodePath("Base/Value/FloatListNode")]
    public class FloatListNode : ValueNode
    {
        [PortInfo("FloatList", 1, typeof(List<float>))]
        public List<float> floatList;
    }
}
