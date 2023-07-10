using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsn.MVVM
{
    public class PropertyChangedMessage<T> :MessageBase
    {
        public string PropertyName { get; private set; }
        
        public T OldValue { get; private set; }
        
        public T NewValue { get; private set; }
        
        public PropertyChangedMessage(T oldValue, T newValue, string propertyName) : this(null, oldValue, newValue, propertyName)
        {
        }

        public PropertyChangedMessage(object sender, T oldValue, T newValue, string propertyName) : base(sender)
        {
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
