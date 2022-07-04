
namespace NeverSayNever
{
    public interface IManager
    {
        void OnInitialize(params object[] args);
        void OnUpdate(float deltaTime);
        void OnDispose();
    }
}