namespace NeverSayNever
{
    internal interface ISingleton
    {
        void OnInitialize(params object[] args);
        void OnUpdate();
        void OnDispose();
    }
}