namespace NeverSayNever.Core
{
    public interface ISingleton
    {
        void OnInitialize(params object[] args);
        void OnUpdate();
        void OnDispose();
    }
}