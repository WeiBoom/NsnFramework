using Sirenix.OdinInspector;
using NeverSayNever.Utilitiy;

namespace NeverSayNever.Core.HUD
{
    /// <summary>
    /// UI事件
    /// </summary>
    public enum EPanelEvent
    {
        PanelOpen,
        PanelShow,
        PanelHide,
        PanelClose,
    }

    /// <summary>
    /// UI层级
    /// </summary>
    [EnumPaging]
    public enum EPanelLayer
    {
        [LabelText("UI 底层")]
        Bottom,
        [LabelText("UI 核心层")]
        Main,
        [LabelText("UI 窗口")]
        Window,
        [LabelText("UI 弹出框（公告）")]
        Pop,
        [LabelText("UI 消息（跑马灯）")]
        Message,
        [LabelText("UI 引导")]
        Guide,
        [LabelText("UI 顶层")]
        Top,
    }

    /// <summary>
    /// UITween动画类型
    /// </summary>
    [EnumPaging]
    public enum EPanelTween
    {
        [LabelText("无动画")]
        None,
        [LabelText("缩放动画")]
        Scale,
        [LabelText("渐入动画")]
        Move,
    }
    
    /// <summary>
    /// UI元素类型
    /// </summary>
    [EnumPaging]
    public enum UIElementType
    {
        [LabelText("空")]
        None,
        [LabelText("节点")]
        Node,        
        [LabelText("按钮")]
        Button,
        [LabelText("文本")]
        Text,
        [LabelText("Mesh文本")]
        TextMeshPro,
        [LabelText("Sprite图片")]
        Image,
        [LabelText("Tex图片")]
        Texture,
        [LabelText("滚动列表")]
        Scroll,
        [LabelText("网格")]
        Grid,
    }
}
