using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Nsn
{
    public class Subject<T> : SubjectBase
    {
        public class Subscription  :ISubscription<T>
        {
            private Subject<T> m_Subject;
            private Action<T> m_Action;
            private System.Threading.SynchronizationContext m_Context;

            private bool m_Disposed = false;
            
            public string Key { get; private set; }

            public Subscription(Subject<T> subject, Action<T> action)
            {
                m_Subject = subject;
                m_Action = action;
                Key = Guid.NewGuid().ToString();
                m_Subject.Add(this);
            }

            ~Subscription()
            {
                Dispose(false);
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            
            public ISubscription<T> ObserveOn(SynchronizationContext context)
            {
                m_Context = context ?? throw new ArgumentNullException("context");
                return this;
            }
            
            protected virtual void Dispose(bool disposing)
            {
                if (m_Disposed) return;
                try
                {
                    if (m_Disposed) return;
                    m_Subject?.Remove(this);
                    m_Context = null;
                    m_Action = null;
                    m_Subject = null;
                }
                catch (Exception e)
                {
                }
                m_Disposed = false;
            }

            public void Publish(T message)
            {
                try
                {
                    if (m_Context != null)
                        m_Context.Post(state => m_Action((T)state), message);
                    else
                        m_Action(message);
                }
                catch (Exception e)
                {
                }
            }
        }
        
        private readonly ConcurrentDictionary<string, WeakReference<Subscription>> subscriptions =
            new ConcurrentDictionary<string, WeakReference<Subscription>>();

        
        
        public override void Publish(object message)
        {
            Publish((T)message);
        }

        public void Publish(T message)
        {
            if (subscriptions.Count <= 0)
                return;

            foreach (var VARIABLE in subscriptions)
            {
                if (VARIABLE.Value.TryGetTarget(out Subscription subscription))
                    subscription.Publish(message);
                else
                    subscriptions.TryRemove(VARIABLE.Key, out _);
            }
        }

        public void Add(Subscription subscription)
        {
            var reference = new WeakReference<Subscription>(subscription, false);
            subscriptions.TryAdd(subscription.Key, reference);
        }

        public void Remove(Subscription subscription)
        {
            subscriptions.TryRemove(subscription.Key, out _);
        }
        
        
        
    }
}