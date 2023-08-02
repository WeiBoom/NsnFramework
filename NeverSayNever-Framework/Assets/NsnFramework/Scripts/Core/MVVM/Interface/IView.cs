using UnityEngine;

namespace Nsn.MVVM
{
    public interface IView
    {
        string Name { get; set; }
        
        GameObject GameObj { get; }
        
        Transform Transform { get; }
        
        Transform Parent { get; }
    }
}