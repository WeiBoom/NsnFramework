using System.Text;
using UnityEngine;

namespace NeverSayNever
{
    public enum ENsnLogLevel
    {
        Debug = 0,
        Hint = 1,
        Warning = 2,
        Errlor = 3,
    }



    public static class NsnLog
    {
        private static readonly StringBuilder LogInfos = new StringBuilder();

        private static ENsnLogLevel mLogLevel = ENsnLogLevel.Debug;

        public static ENsnLogLevel LogLevel => mLogLevel;

        public static void SetLogLevel(ENsnLogLevel logLevel)
        {
            mLogLevel = logLevel;
        }

        public static void Print(string content, params string[] args)
        {
#if !DISABLE_DEBUG
            SetInfos(content, args);
            Debug.Log(LogInfos);
#endif
        }

        public static void Warning(string content, params string[] args)
        {
#if !DISABLE_DEBUG
            SetInfos(content, args);
            Debug.LogWarning(LogInfos);
#endif
        }

        public static void Error(string content, params string[] args)
        {
#if !DISABLE_DEBUG
            SetInfos(content, args);
            Debug.LogError(LogInfos);
#endif
        }

        public static StringBuilder DebugLog(string content, params string[] args)
        {
            StringBuilder stringBuilder = CombineLogContent(content, args);
            return stringBuilder;
        }

        private static StringBuilder CombineLogContent(string content, params string[] args)
        {
            if (args != null)
            {
                var sb = new StringBuilder();
                var length = args.Length;
                for (int i = 0; i < length; i++)
                {
                    sb.Append(args[i]);
                }
                return sb;
            }
            return null;
        }

        private static void SetInfos(string content, params string[] args)
        {
            LogInfos.Clear();
            LogInfos.Append(content);
            if (args == null) return;
            foreach (var info in args)
            {
                LogInfos.Append(info);
            }
        }

    }
}
