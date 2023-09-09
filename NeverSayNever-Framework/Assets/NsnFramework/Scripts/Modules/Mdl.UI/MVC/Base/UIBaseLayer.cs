using UnityEngine;
using UnityEngine.UI;

namespace Nsn.MVC
{
    public struct UILayerData
    {
        public string Name;
        public int PlaneDistance;
        public int OrderInLayer;

        public UILayerData(string name, int distance, int order)
        {
            Name = name;
            PlaneDistance = distance;
            OrderInLayer = order;
        }
    }

    public class LayerDefine
    {
        public const string UI = "UI";
    }

    public static class UILayers
    {
        /// <summary>
        /// 场景UI
        /// </summary>
        public static readonly UILayerData SceneLayer = new UILayerData("Scene", 1000, 0);
        /// <summary>
        /// 背景UI
        /// </summary>
        public static readonly UILayerData BackgroundLayer = new UILayerData("Background", 900, 1000);
        /// <summary>
        /// 普通UI
        /// </summary>
        public static readonly UILayerData NormalLayer = new UILayerData("Normal", 800, 2000);
        /// <summary>
        /// 信息UI（跑马灯，广播等）
        /// </summary>
        public static readonly UILayerData InfoLayer = new UILayerData("Info", 700, 3000);
        /// <summary>
        /// 提示UI （网络连接，错误探窗等）
        /// </summary>
        public static readonly UILayerData TipLayer = new UILayerData("Tip", 600, 4000);
        /// <summary>
        /// 顶层UI（Loading场景等）
        /// </summary>
        public static readonly UILayerData TopLayer = new UILayerData("Scene", 500, 5000);
    }
    
    public class UIBaseLayer : UIBaseComp
    {
        private int topViewOrder;
        private int minViewOrder;

        // 窗口最大可使用的相對order in layer
        private int maxOrderRelativeWindow = 15;

        public void OnCreate(UILayerData layer,int matchValue)
        {
            m_UIName = transform.name;
            IUIMgr uiMgr = Framework.GetManager<IUIMgr>();
            
            m_Canvas = GetOrAddComponent<Canvas>();
            m_Canvas.renderMode = RenderMode.ScreenSpaceCamera;
            m_Canvas.worldCamera = uiMgr.UICamera2D;
            m_Canvas.sortingLayerName = LayerDefine.UI;
            m_Canvas.sortingOrder = layer.OrderInLayer;
            m_Canvas.planeDistance = layer.PlaneDistance;

            m_CanvasScaler = GetOrAddComponent<CanvasScaler>();
            m_CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            m_CanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            m_CanvasScaler.referenceResolution = uiMgr.DesignResolution;
            m_CanvasScaler.matchWidthOrHeight = matchValue;

            m_GraphicRaycaster = GetOrAddComponent<GraphicRaycaster>();
            
            topViewOrder = layer.OrderInLayer;
            minViewOrder = layer.OrderInLayer;
        }


        public int PopViewOrder()
        {
            int cur = topViewOrder;
            topViewOrder += maxOrderRelativeWindow;
            return cur;
        }

        public void ResortViewOrder()
        {
            topViewOrder = minViewOrder;
            int childCount = transform.childCount;
            for (int i = 0; i < childCount - 1; i++)
            {
                var childCanvas = transform.GetChild(i).GetComponent<Canvas>();
                if (childCanvas != null && childCanvas.gameObject.activeSelf)
                {
                    childCanvas.sortingOrder = topViewOrder;
                    if (topViewOrder < minViewOrder)
                    {
                        NsnLog.Error($"Layer 层级设置错误， 当前层 {transform.name} , 层级 {topViewOrder}");
                    }
                    topViewOrder = topViewOrder + maxOrderRelativeWindow;
                }
            }
        }
    }
}