using System;

namespace Nsn
{
    public class ObservablePropertyBase<T>
    {
        private readonly object m_Lock = new object();

        private EventHandler m_ValueChanged;

        protected T m_Value;
        
        public event EventHandler ValueChanged
        {
            add{ lock (m_Lock) m_ValueChanged += value;}
            remove{ lock (m_Lock) m_ValueChanged -= value;}
        }

        public ObservablePropertyBase() : this(default(T)){}

        public ObservablePropertyBase(T value) => m_Value = value;
        
        public virtual Type Type => typeof(T);

        protected void RaiseValueChanged() => m_ValueChanged?.Invoke(this, EventArgs.Empty);

        protected virtual bool Equals(T x, T y)
        {
            if (x != null && y != null)
                return x.Equals(y);
            if (x != null || y != null)
                return false;
            return true;
        }
    }
    
    [Serializable]
    public class ObservableProperty:ObservablePropertyBase<object>, IObservableProperty
    {
        public ObservableProperty() : this(null){}
        
        public ObservableProperty(object value) : base(value){}

        public override Type Type => m_Value != null ? m_Value.GetType() : typeof(object);

        public virtual object Value
        {
            get => m_Value;
            set
            {
                if (this.Equals(m_Value, value))
                    return;

                m_Value = value;
                RaiseValueChanged();
            }
        }

        public override string ToString()
        {
            var v = Value;
            return Value == null ? string.Empty : v.ToString();
        }
    }

    [Serializable]
    public class ObservableProperty<T> : ObservablePropertyBase<T>, IObservableProperty<T>
    {
        public ObservableProperty() : this(default(T)){}
        
        public ObservableProperty(T value) : base(value){}
        
        public virtual T Value
        {
            get => m_Value;
            set
            {
                if (this.Equals(m_Value, value))
                    return;

                m_Value = value;
                RaiseValueChanged();
            }
        }

        object IObservableProperty.Value
        {
            get => Value;
            set => Value = (T)value;
        }
        
        public override string ToString()
        {
            var v = Value;
            return Value == null ? string.Empty : v.ToString();
        }

        public static implicit operator T(ObservableProperty<T> data)
        {
            return data.Value;
        }

        public static implicit operator ObservableProperty<T>(T data)
        {
            return new ObservableProperty<T>(data);
        }
    }
}