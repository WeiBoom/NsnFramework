using UnityEngine;

namespace Nsn.MVVM
{
    public interface IUIView : IView
    {
        RectTransform RectTransform { get; }
     
        CanvasGroup CanvasGroup { get; }
        
    }
}