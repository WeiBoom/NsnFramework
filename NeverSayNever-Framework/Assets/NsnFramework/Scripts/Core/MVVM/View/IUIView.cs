using UnityEngine;

namespace Nsn
{
    public interface IUIView : IView
    {
        RectTransform RectTransform { get; }
     
        CanvasGroup CanvasGroup { get; }
        
    }
}