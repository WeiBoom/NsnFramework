using System;

namespace Nsn.MVVM
{
    public class MessageBase : EventArgs
    {
        public object Sender { get; protected set; }
        
        public MessageBase(object sender)
        {
            Sender = sender;
        }
    }
}