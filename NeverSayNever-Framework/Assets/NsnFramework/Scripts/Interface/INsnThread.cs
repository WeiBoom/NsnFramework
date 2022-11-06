using System.Threading;

namespace NeverSayNever
{
    public interface IThreadLogicCore
    {
        void MainLoop();

        void OnStart();

        void OnStop();

        string ThreadName { get; }
    }

    public interface IThread
    {
        public bool IsRunning { get; }

        public void Start(IThreadLogicCore logicCore);

        public void Stop();
    }

    public class NsnThread : IThread
    {
        private static System.Threading.Thread mWorkingThread;
        public static System.Threading.Thread WokingThread => mWorkingThread;

        private bool isRunning = false;
        public bool IsRunning => IsRunning;

        private IThreadLogicCore mLogicCore;

        public void Start(IThreadLogicCore logicCore)
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
