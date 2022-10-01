using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverSayNever
{
    public interface IEventGroup
    {
        void AddListencer();

        void RemoveListencer();
    }
}
