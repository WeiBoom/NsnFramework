using System.Threading;

namespace Nsn
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
         bool IsRunning { get; }

         void Start(IThreadLogicCore logicCore);

         void Stop();
    }

    public class Thread : IThread
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

            mWorkingThread = new System.Threading.Thread(new ThreadStart(mLogicCore.MainLoop));
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
