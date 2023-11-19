namespace Nsn.MVC
{
    /*
    -- 功能描述：界面Model层数据基类，存储该界面相关数据
    -- 说明：
    -- 1、数据大体分为两类：界面逻辑数据、界面控制数据
    -- 2、界面逻辑数据：从游戏数据中心取数据，这里不做为数据源，只做中转和必要处理(如排序)---游戏中心数据改动以后在这里监听变化
    -- 3、界面控制数据：一般会影响到多个界面展示的控制数据，如：当受到选服界面操作的影响时，登陆界面显示当前选择的服务器
    -- 4、界面Model层在View层是只读不写的，一定不要在View层修改界面Model
    -- 5、界面Model层不依赖Ctrl层和View层，只影响View层
     */
    public class BaseUIModel
    {
        public BaseUIModel()
        {
        }

        ~BaseUIModel()
        {
            OnDisable();
        }
        
        /// <summary>
        /// 激活数据类
        /// </summary>
        public void OnEnable()
        {
        }

        /// <summary>
        /// 清理数据类
        /// </summary>
        public void OnDisable()
        {
        }
    }
}