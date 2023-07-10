using System;

namespace Nsn.MVVM
{
    public interface IMessager
    {
        ISubscription<T> Subscribe<T>(Action<T> action);

        ISubscription<T> Subscribe<T>(string channel, Action<T> action);

        ISubscription<object> Subscribe(Type type, Action<object> action);

        ISubscription<object> Subscribe(string channel, Type type, Action<object> action);

        void Publish<T>(T message);
        
        void Publish<T>(string channel, T message);

        void Publish(object message);

        void Publish(string channel, object message);
    }

}