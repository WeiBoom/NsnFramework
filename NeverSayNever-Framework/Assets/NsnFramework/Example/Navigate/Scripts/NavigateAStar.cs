using System;
using System.Collections.Generic;
using Nsn;

namespace Nsn.Example
{
    public class NavigateAStar
    {
        // 上下左右方向相邻格子距离
        private static int FACTOR = 10;
        // 斜对角方向相邻格子的距离
        private static int FACTOR_DIAGONAL = 14;

        private bool m_Initialized;
        public bool Initialized => m_Initialized;

        // 当前地图
        private Navigate2DMapGridItem[,] m_Map;
        private Int2 m_MapSize;
        private Int2 m_StartPos;
        private Int2 m_TargetPos;


    }
}