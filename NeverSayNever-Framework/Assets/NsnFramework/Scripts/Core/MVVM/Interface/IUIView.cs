using UnityEngine;

namespace Nsn.MVVM
{
    public interface IUIView : IView
    {
        RectTransform RectTransform { get; }
        
        // UI依赖的CanvasGroup组件
        CanvasGroup CanvasGroup { get; }
        
        // 界面的Alpha值
        float Alpha { get; set; }
        
        // 是否是可交互的界面
        bool Interactable { get; set; }
        
        // 进入动画
        IAnimation EnterAnimation { get; set; }
            
        // 退出动画
        IAnimation ExitAnimation { get; set; }

    }
}