using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverSayNever
{
    public interface INsnModule
    {
        void OnCreate(params object[] args);
        void OnUpdate(float deltaTime);
        void OnDispose();
    }
}
