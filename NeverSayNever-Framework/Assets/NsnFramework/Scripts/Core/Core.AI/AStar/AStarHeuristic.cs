using System;
using UnityEngine;

namespace NeverSayNever
{
    /// <summary>
    /// A* 启发式方法计算距离
    /// </summary>
    public static class AStarHeuristic
    {
        // 曼哈顿方法，适用于上下左右四方向移动的情景
        public static float ManhattanDist(Vector3 a, Vector3 b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }

        // 切比雪夫距离 , 适用于八方向，每个方向距离相同
        public static float ChebyshevDist(Vector3 a, Vector3 b)
        {
            float dx = Mathf.Abs(a.x - b.x);
            float dy = Mathf.Abs(a.y - b.y);
            return Mathf.Max(dx, dy);
        }

        //  Octile 距离
        public static float OctileDist(Vector3 a, Vector3 b)
        {
            float dx = Mathf.Abs(a.x - b.x);
            float dy = Mathf.Abs(a.y - b.y);
            // d + (根号2 - 1) * 
            return Mathf.Max(dx, dy) + (Mathf.Sqrt(2f) - 1f) * Mathf.Min(dx, dy);
        }

        // 欧式距离 直接计算两点之间的距离
        public static float EuclideanDist(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b);
        }
    }
}
