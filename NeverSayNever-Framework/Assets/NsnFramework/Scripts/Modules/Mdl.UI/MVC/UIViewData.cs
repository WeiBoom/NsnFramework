
namespace Nsn.MVC
{
    public class UIViewData
    {
        /// <summary>
        /// 界面名字
        /// </summary>
        public string Name;

        /// <summary>
        /// 资源路径
        /// </summary>
        public string PrefabPath;

        /// <summary>
        /// 是否正在加载
        /// </summary>
        public bool IsLoading;

        /// <summary>
        /// 加载预制完成
        /// </summary>
        public bool LoadFinish;

        /// <summary>
        /// 界面参数
        /// </summary>
        public UIViewParam ViewParam;
        
        /// <summary>
        /// 界面层级
        /// </summary>
        public UILayerData Layer;

        /// <summary>
        /// Model 实例
        /// </summary>
        public BaseUIModel Model;

        /// <summary>
        /// View 实例
        /// </summary>
        public BaseUIView View;

        /// <summary>
        /// Control 实例
        /// </summary>
        public BaseUICtrl Ctrl;
    }

    public class UIViewParam
    {
        public int Param1;
        public int Param2;
        public long Param3;
        public float Param4;
        public float Param5;

        public object ObjParam1;
        public object ObjParam2;
    }
}