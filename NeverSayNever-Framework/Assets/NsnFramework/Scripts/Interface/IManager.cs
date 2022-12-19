using System.Collections;
using UnityEngine;

namespace Nsn
{
    public interface IManager
    {
        void OnInitialized(params object[] args);
        void OnUpdate(float deltaTime);
        void OnDisposed();
    }
}