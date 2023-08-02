using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Nsn.MVVM
{
    public class ViewModelBase : ObservableObject, IViewModel
    {
        private IMessager m_Messenger;

        public virtual IMessager Messenger
        {
            get => m_Messenger;
            set => m_Messenger = value;
        }
        
        public ViewModelBase() : this(null) { }
        
        public ViewModelBase(IMessager messenger) => m_Messenger = messenger;

        ~ViewModelBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }

        public void NotifyPropertyChanged<T>(Expression<Func<T>> memberExpr)
        {
            string propertyName = ParserPropertyName(memberExpr);
            OnPropertyChanged(propertyName);
        }

        protected void Broadcast<T>(T oldValue,T newValue, string propertyValue)
        {
            try
            {
                var messenger = m_Messenger;
                if (messenger != null)
                    messenger.Publish(new PropertyChangedMessage<T>(this, oldValue, newValue, propertyValue));
            }
            catch(Exception e)
            {
                NsnLog.Error($"Set Property {propertyValue} , broadcast message failed! exception : {e}");
            }
        }

        protected bool Set<T>(ref T field, T newValue, Expression<Func<T>> propertyExpression, bool broadcast)
        {
            if (object.Equals(field, newValue))
                return false;
            var oldValue = field;
            field = newValue;
            var propertyName = ParserPropertyName(propertyExpression);
            OnPropertyChanged(propertyName);

            if(broadcast)
                Broadcast(oldValue,newValue,propertyName);

            return true;
        }

        protected bool Set<T>(ref T field, T newValue, string propertyName, bool broadcast)
        {
            if (object.Equals(field, newValue))
                return false;
            
            var oldValue = field;
            field = newValue;
            OnPropertyChanged(propertyName);

            if(broadcast)
                Broadcast(oldValue,newValue,propertyName);

            return true;
        }
        
        protected bool Set<T>(ref T field, T newValue, PropertyChangedEventArgs eventArgs, bool broadcast)
        {
            if (object.Equals(field, newValue))
                return false;

            var oldValue = field;
            field = newValue;
            OnPropertyChanged(eventArgs);

            if (broadcast)
                Broadcast(oldValue, newValue, eventArgs.PropertyName);
            return true;
        }
    }
}