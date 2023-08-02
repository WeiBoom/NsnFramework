namespace Nsn.MVVM
{
    public class MessageBase : System.EventArgs
    {
        public MessageBase(object sender)
        {
            Sender = sender;
        }
        
        public object Sender { get; protected set; }

    }
} 