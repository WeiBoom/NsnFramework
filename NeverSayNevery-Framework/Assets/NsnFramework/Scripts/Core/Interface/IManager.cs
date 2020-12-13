
namespace NeverSayNever.Core
{
    public interface IManager
    {
        void OnInitialize();
        void OnUpdate();
        void OnDispose();
    }
}