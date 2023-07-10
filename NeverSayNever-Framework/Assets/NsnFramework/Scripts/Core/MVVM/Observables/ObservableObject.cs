using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Nsn.MVVM
{
    [System.Serializable]
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        private static readonly PropertyChangedEventArgs s_NullEventArgs = new PropertyChangedEventArgs(null);
        private static readonly Dictionary<string, PropertyChangedEventArgs> s_PropertyEventArgs = new Dictionary<string, PropertyChangedEventArgs>();

        private static PropertyChangedEventArgs GetPropertyChangedEventArgs(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return s_NullEventArgs;
            PropertyChangedEventArgs eventArgs;
            if (s_PropertyEventArgs.TryGetValue(propertyName, out eventArgs))
                return eventArgs;

            eventArgs = new PropertyChangedEventArgs(propertyName);
            s_PropertyEventArgs[propertyName] = eventArgs;
            return eventArgs;
        }
        
        // interface property
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventArgs eventArgs = GetPropertyChangedEventArgs(propertyName);
            OnPropertyChanged(eventArgs); 
        }

        protected virtual void OnPropertyChanged(params PropertyChangedEventArgs[] eventArgs)
        {
            foreach (var args in eventArgs)
            {
                OnPropertyChanged(args);
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs eventArgs,
            [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            PropertyChanged?.Invoke(this, eventArgs);
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        
        protected virtual string ParserPropertyName(System.Linq.Expressions.LambdaExpression propertyExpression)
        {
            if (propertyExpression == null)
                throw new System.ArgumentNullException("propertyExpression");

            var body = propertyExpression.Body as System.Linq.Expressions.MemberExpression;
            if (body == null)
                throw new System.ArgumentException("Invalid argument", "propertyExpression");

            var property = body.Member as System.Reflection.PropertyInfo;
            if (property == null)
                throw new System.ArgumentException("Argument is not a property", "propertyExpression");

            return property.Name;
        }

    }
}