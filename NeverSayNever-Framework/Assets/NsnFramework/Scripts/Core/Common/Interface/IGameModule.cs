using System;
namespace NeverSayNever.Core
{
    public interface IGameModule
    {
        void OnInitialize();
        void OnUpdate(float deltaTime);
        void OnDispose();
    }
}
