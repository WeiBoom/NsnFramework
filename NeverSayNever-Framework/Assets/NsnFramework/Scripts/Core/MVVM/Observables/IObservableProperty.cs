namespace Nsn
{
    public interface IObservableProperty
    {
        event System.EventHandler ValueChanged;
        
        System.Type Type { get; }
        
        object Value { get; set; }
    }

    public interface IObservableProperty<T> : IObservableProperty
    {
        new T Value { get; set; }
    }
}