using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverSayNever
{
    public interface IFSM : IDisposable
    {

        void SetId(int id);

        void Update(float deltaTime);

        void SetTransition(int stateId);

        void AddTransition(int stateId, IFSMState state);
    }
}
