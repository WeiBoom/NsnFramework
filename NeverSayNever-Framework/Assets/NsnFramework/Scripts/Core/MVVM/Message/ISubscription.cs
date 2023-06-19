using System;

namespace Nsn
{
    public interface ISubscription<T> : IDisposable
    {
        ISubscription<T> ObserveOn(System.Threading.SynchronizationContext context);
    }
}
