using System.Threading;

namespace NeverSayNever
{
    public interface INsnThreadLogicCore
    {
        void MainLoop();

        void OnStart();

        void OnStop();

        string ThreadName { get; }
    }

    public interface INsnThread
    {
        public bool IsRunning { get; }

        public void Start(INsnThreadLogicCore logicCore);

        public void Stop();
    }

    public class NsnThread : INsnThread
    {
        private static System.Threading.Thread mWorkingThread;
        public static System.Threading.Thread WokingThread => mWorkingThread;

        private bool isRunning = false;
        public bool IsRunning => IsRunning;

        private INsnThreadLogicCore mLogicCore;

        public void Start(INsnThreadLogicCore logicCore)
        {
            if (logicCore == null)
                return;
            mLogicCore = logicCore;
            mLogicCore.OnStart();

            mWorkingThread = new Thread(new ThreadStart(mLogicCore.MainLoop));
            mWorkingThread.Name = logicCore.ThreadName;
            mWorkingThread.Start();

            isRunning = true;
        }

        public void Stop()
        {
            mLogicCore.OnStop();
            mLogicCore = null;

            mWorkingThread?.Join();
            mWorkingThread = null;

            isRunning = false;
        }
    }

}
