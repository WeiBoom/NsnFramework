using System;

namespace Nsn.MVVM
{
    public interface ISubscription<T> : IDisposable
    {
        ISubscription<T> ObserveOn(System.Threading.SynchronizationContext context);
    }
}
