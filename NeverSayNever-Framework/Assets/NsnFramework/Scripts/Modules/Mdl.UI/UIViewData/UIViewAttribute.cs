namespace Nsn
{
    public enum UIViewType
    {
        UIBasement = -1,
        UIWindow = 0,
        UIPopup = 1,
        UITips = 2
    };

    public enum UIViewLayer
    {
        Popup = 1001,
        Tips = 15001,
        Dialogue = 17001,
        Guide = 18001,
        Network = 19001,
        GM = 20000,
    }

    [System.Serializable]
    public struct UIViewAttribute 
    {
        public UIViewType ViewType;
        public int PanelOrder;
        public bool IsFullScreen;
        public bool IsFocusable;
        public bool MaskVisible;
        public string ViewName;
    }

}