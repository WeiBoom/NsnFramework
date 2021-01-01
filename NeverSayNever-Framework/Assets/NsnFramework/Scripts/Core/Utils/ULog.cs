using System.Text;
using UnityEngine;

namespace NeverSayNever.Utilities
{
    public static class ULog
    {
        private static readonly StringBuilder LogInfos = new StringBuilder();

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
    }
}
