using System;
namespace NeverSayNever
{
    public interface IGameModule
    {
        void OnInitialize();
        void OnUpdate(float deltaTime);
        void OnDispose();
    }
}
