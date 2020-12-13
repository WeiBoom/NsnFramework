namespace NeverSayNever.Core.HUD
{
    public class UIPanelMessenger : UIBaseMessenger
    {
        // 界面打开时传递的参数
        // 由派生类的messenger传递给对应的panel
        protected object[] ReceivedParameter;

        public UIPanelMessenger(string name) : base(name)
        {
        }

        /// <summary>
        /// 界面打开之前调用,预处理界面所需的数据，资源等
        /// </summary>
        /// <param name="args"></param>
        public override bool OnPreOpen(params object[] args)
        {
            return true;
        }

        /// <summary>
        /// 发送隐藏界面的消息
        /// </summary>
        /// <param name="args"></param>
        public override void OnPreHide(params object[] args)
        {
        }

        /// <summary>
        /// 发送关闭界面的消息
        /// </summary>
        /// <param name="args"></param>
        public override void OnPreClose(params object[] args)
        {
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="args"></param>
        public override void OnSendMsg(params object[] args)
        {
        }

        /// <summary>
        /// 从外部接受消息
        /// </summary>
        /// <param name="args"></param>
        public override void OnReceiveMsg(params object[] args)
        {
            ReceivedParameter = args;
        }
    }
}