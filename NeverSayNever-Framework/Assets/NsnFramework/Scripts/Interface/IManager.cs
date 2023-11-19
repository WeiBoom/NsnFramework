using System.Collections;
using UnityEngine;

namespace Nsn
{
    public interface IManager
    {
        void OnUpdate(float deltaTime);
        void OnDisposed();
    }
}