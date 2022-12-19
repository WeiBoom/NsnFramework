
namespace NeverSayNever
{
    public interface INsnManager
    {
        void OnInitialize(params object[] args);
        void OnUpdate(float deltaTime);
        void OnDispose();
    }
}