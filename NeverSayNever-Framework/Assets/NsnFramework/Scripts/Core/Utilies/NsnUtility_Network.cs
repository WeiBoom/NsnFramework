namespace Nsn
{
    using UnityEngine;
    
    /*
     * Network 相关
     */
    public static partial class NsnUtility
    {
        /// <summary>
        /// 当前网络是否可用
        /// </summary>
        public static bool IsNetAvailable => Application.internetReachability != NetworkReachability.NotReachable;

        /// <summary>
        /// 当前是否为无线网络
        /// </summary>
        public static bool IsWifi =>
            Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;

    }
}