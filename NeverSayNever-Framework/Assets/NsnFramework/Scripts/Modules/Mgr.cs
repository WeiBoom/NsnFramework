namespace Nsn
{
    public class Mgr
    {
        public static IUIMgr UI { get; private set; }
        public static IResMgr Res { get; private set; }
        public static IEventManager Event { get; private set; }
        
        public static void Initialize()
        {
            UI = new UIMgr();
            Res = new ResMgr();
            Event = new EventManager();
        }
    }
}