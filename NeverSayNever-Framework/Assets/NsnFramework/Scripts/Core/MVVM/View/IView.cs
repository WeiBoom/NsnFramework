using UnityEngine;

namespace Nsn.MVVM
{
    public interface IView
    {
        string Name { get; set; }
        GameObject Go { get; }
        Transform Transform { get; }
        Transform Parent { get; }
        
    }
}