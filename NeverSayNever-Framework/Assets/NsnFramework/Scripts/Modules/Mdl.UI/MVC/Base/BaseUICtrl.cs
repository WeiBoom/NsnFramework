namespace Nsn.MVC
{
    /*
     -- 功能描述：UI控制层基类
     -- 说明：
     -- 1、衔接MV层：UI控制层用于衔接Model层和View层
     -- 2、修改数据：界面操作相关数据直接写Model层、游戏逻辑相关数据写数据中心
     -- 3、游戏控制：发送网络请求、调用游戏控制逻辑函数
     -- 4、UI控制层不依赖View层，但是依赖Model层
     */
    public class BaseUICtrl
    {
        protected BaseUIModel model;
        
        public BaseUICtrl(BaseUIModel model)
        {
            if (model == null)
                throw new System.Exception("Model can't be null !");
            this.model = model;
        }

        ~BaseUICtrl() => model = null;

        public void PreLoad(System.Action callback, params object[] args)
        {
            callback?.Invoke();
        }
    }
}