namespace NeverSayNever.Core.HUD
{
    public abstract class UIBaseMessenger
    {
        public string PanelName { get; protected set; }

        protected UIBaseMessenger(string name)
        {
            PanelName = name;
        }

        public abstract bool OnPreOpen(params object[] args);
        public abstract void OnPreHide(params object[] args);
        public abstract void OnPreClose(params object[] args);
        public abstract void OnSendMsg(params object[] args);
        public abstract void OnReceiveMsg(params object[] args);

    }
}