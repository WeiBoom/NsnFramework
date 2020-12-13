using Sirenix.OdinInspector;
using NeverSayNever.Utilities;

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
        [NSNEnum("UI 底层")]
        Bottom,
        [NSNEnum("UI 核心层")]
        Main,
        [NSNEnum("UI 窗口")]
        Window,
        [NSNEnum("UI 弹出框（公告）")]
        Pop,
        [NSNEnum("UI 消息（跑马灯）")]
        Message,
        [NSNEnum("UI 引导")]
        Guide,
        [NSNEnum("UI 顶层")]
        Top,
    }

    /// <summary>
    /// UITween动画类型
    /// </summary>
    [EnumPaging]
    public enum EPanelTween
    {
        [NSNEnum("无动画")]
        None,
        [NSNEnum("缩放动画")]
        Scale,
        [NSNEnum("渐入动画")]
        Move,
    }
    
    /// <summary>
    /// UI元素类型
    /// </summary>
    [EnumPaging]
    public enum UIElementType
    {
        [NSNEnum("空")]
        None,
        [NSNEnum("节点")]
        Node,        
        [NSNEnum("按钮")]
        Button,
        [NSNEnum("文本")]
        Text,
        [NSNEnum("Mesh文本")]
        TextMeshPro,
        [NSNEnum("Sprite图片")]
        Image,
        [NSNEnum("Tex图片")]
        Texture,
        [NSNEnum("滚动列表")]
        Scroll,
        [NSNEnum("网格")]
        Grid,
    }
}
