namespace Nsn.MVVM
{
    public interface IAnimation
    {
        IAnimation OnStart(System.Action startFunc);

        IAnimation OnEnd(System.Action endFunc);

        IAnimation Play();
    }
}