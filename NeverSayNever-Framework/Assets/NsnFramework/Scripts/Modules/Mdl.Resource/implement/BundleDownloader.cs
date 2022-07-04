using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{
    public class BundleDownLoader
    {
        private static float KBSize = 1024;

        private static float MBSize = 1024 * 1024;

        private static float BYTES_2_MB = 1f / (1024 * 1024);

        public static string GetDownloadDisplaySize(float downloadSize)
        {
            if (downloadSize >= MBSize)
                return string.Format("{0:f2}MB", downloadSize * BYTES_2_MB);

            if (downloadSize >= KBSize)
                return string.Format("{0:f2}KB", downloadSize / KBSize);

            return string.Format("{0:f2}B", downloadSize);
        }

        public static string GetDownloadDisplaySpeed(float downloadSpeed)
        {
            if (downloadSpeed >= MBSize)
                return string.Format("{0:f2}MB/s", downloadSpeed * BYTES_2_MB);

            if (downloadSpeed >= MBSize)
                return string.Format("{0:f2}KB/s", downloadSpeed / MBSize);

            return string.Format("{0:f2}B/s", downloadSpeed);
        }


        internal void OnUpdate()
        {

        }


    }
}