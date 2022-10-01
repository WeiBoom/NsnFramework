using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NeverSayNever
{
    public static class NsnMath
    {
        // 用于判断浮点数是否为0
        public const float Precision = 0.000001f;

        /// <summary>
        /// 判断一个浮点数是否为0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CompareToZero(float value)
        {
            return Mathf.Abs(value) <= Precision;
        }

        /// <summary>
        /// 1阶 贝塞尔曲线
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p0">起始位置</param>
        /// <param name="p1">目标位置</param>
        /// <returns></returns>
        public static Vector3 BezierCureCore(float t, Vector3 p0, Vector3 p1)
        {
            if (t > 1) t = 1;
            float t1 = (1 - t);
            return t1 * p0 + p1 * t;
        }

        /// <summary>
        /// N阶 贝塞尔曲线
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p">第一个节点为起始点，最后一个为目标点</param>
        /// <returns></returns>
        public static Vector3 BezierCureCore_N(float t, List<Vector3> p)
        {
            if (p.Count < 2)
                return p[0];
            var newP = new List<Vector3>();
            for (var i = 0; i < p.Count - 1; i++)
            {
                Debug.DrawLine(p[i], p[i + 1]);
                var bP = (1 - t) * p[i] + t * p[i + 1];
                newP.Add(bP);
            }

            return BezierCureCore_N(t, newP);
        }
    }
}