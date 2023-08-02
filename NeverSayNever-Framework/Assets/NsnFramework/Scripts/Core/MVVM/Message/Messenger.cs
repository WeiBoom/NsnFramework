using System;
using System.Collections.Concurrent;

namespace Nsn.MVVM
{
    public class Messenger : IMessager
    {
        public static readonly Messenger Default = new Messenger();
        
        // 采用并发字典
        private readonly ConcurrentDictionary<Type, SubjectBase> notifiers =
            new ConcurrentDictionary<Type, SubjectBase>();

        private readonly ConcurrentDictionary<string, ConcurrentDictionary<Type, SubjectBase>> channelNotifiers =
            new ConcurrentDictionary<string, ConcurrentDictionary<Type, SubjectBase>>();
        

        public ISubscription<T> Subscribe<T>(Action<T> action)
        {
            Type type = typeof(T);
            SubjectBase notifier;
            if (!notifiers.TryGetValue(type, out notifier))
            {
                notifier = new Subject<T>();
                if (!notifiers.TryAdd(type, notifier))
                    notifiers.TryGetValue(type, out notifier);
            }
            return (notifier as Subject<T>)?.Subscribe(action);
        }

        public ISubscription<T> Subscribe<T>(string channel, Action<T> action)
        {
            SubjectBase notifier = null;
            ConcurrentDictionary<Type, SubjectBase> dict = null;
            if (!channelNotifiers.TryGetValue(channel, out dict))
            {
                dict = new ConcurrentDictionary<Type, SubjectBase>();
                if (!channelNotifiers.TryAdd(channel, dict))
                    channelNotifiers.TryGetValue(channel, out dict);
            }

            if (dict != null && !dict.TryGetValue(typeof(T), out notifier))
            {
                notifier = new Subject<T>();
                if (!dict.TryAdd(typeof(T), notifier))
                    dict.TryGetValue(typeof(T), out notifier);
            }
            return (notifier as Subject<T>)?.Subscribe(action);
        }

        public ISubscription<object> Subscribe(Type type, Action<object> action)
        {
            SubjectBase notifier;
            if (!notifiers.TryGetValue(type, out notifier))
            {
                notifier = new Subject<object>();
                if (!notifiers.TryAdd(type, notifier))
                    notifiers.TryGetValue(type, out notifier);
            }
            return (notifier as Subject<object>)?.Subscribe(action);
        }

        public ISubscription<object> Subscribe(string channel, Type type, Action<object> action)
        {
            SubjectBase notifier = null;
            ConcurrentDictionary<Type, SubjectBase> dict = null;
            if (!channelNotifiers.TryGetValue(channel, out dict))
            {
                dict = new ConcurrentDictionary<Type, SubjectBase>();
                if (!channelNotifiers.TryAdd(channel, dict))
                    channelNotifiers.TryGetValue(channel, out dict);
            }

            if (dict != null && !dict.TryGetValue(type, out notifier))
            {
                notifier = new Subject<object>();
                if (!dict.TryAdd(type, notifier))
                    dict.TryGetValue(type, out notifier);
            }
            return (notifier as Subject<object>)?.Subscribe(action);
        }

        public void Publish<T>(T message)
        {
            if (message == null || notifiers.Count <= 0)
                return;

            Type messageType = message.GetType();
            foreach (var kv in notifiers)
            {
                if (kv.Key.IsAssignableFrom(messageType))
                    kv.Value.Publish(message);
            }
        }

        public void Publish<T>(string channel, T message)
        {
            if (string.IsNullOrEmpty(channel) || message == null)
                return;

            ConcurrentDictionary<Type, SubjectBase> dict = null;
            if (!channelNotifiers.TryGetValue(channel, out dict) || dict.Count <= 0)
                return;

            Type messageType = message.GetType();
            foreach (var kv in dict)
            {
                if (kv.Key.IsAssignableFrom(messageType))
                    kv.Value.Publish(message);
            }
        }

        public void Publish(object message)
        {
            this.Publish<object>(message);
        }

        public void Publish(string channel, object message)
        {
            this.Publish<object>(channel, message);
        }
    }
}