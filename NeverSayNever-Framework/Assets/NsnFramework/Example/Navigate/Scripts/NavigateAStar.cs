using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nsn;
using UnityEngine;

namespace Nsn.Example
{
    using Int2 = NavigateInt2;
    
    public enum NavigateEvaluationType {
        Euclidean,
        Manhattan,
        Diagonal,
    }
    
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

        private Dictionary<Int2, NavigateNode> m_OpenDic = new Dictionary<Int2, NavigateNode>();
        private Dictionary<Int2, NavigateNode> m_ClosedDic = new Dictionary<Int2, NavigateNode>();

        private NavigateNode m_DestinationNode;

        private NavigateEvaluationType m_EvaluationType;

        /// <summary>
        /// 初始化寻路数据
        /// </summary>
        /// <param name="map">mao数据</param>
        /// <param name="mapSize">地图大小</param>
        /// <param name="start">初始位置</param>
        /// <param name="destination">目标位置</param>
        /// <param name="evaluationType">距离计算类型</param>
        public void Init(Navigate2DMapGridItem[,] map, Int2 mapSize, Int2 start, Int2 destination,NavigateEvaluationType evaluationType = NavigateEvaluationType.Diagonal)
        {
            m_Map = map;
            m_MapSize = mapSize;
            m_StartPos = start;
            m_TargetPos = destination;
            m_EvaluationType = evaluationType;

            m_OpenDic.Clear();
            m_ClosedDic.Clear();

            m_DestinationNode = null;

            var startNode = new NavigateNode(start, null, 0, 0);
            AddNodeToOpenDic(startNode);

            m_Initialized = true;
        }

        /// <summary>
        /// 执行寻路
        /// </summary>
        /// <returns></returns>
        public IEnumerator Execute()
        {
            while (m_OpenDic.Count > 0 && m_DestinationNode == null)
            {
                // 根据FGH值对节点排序，取第一个来进行寻路计算
                m_OpenDic = m_OpenDic.OrderBy(pair => pair.Value.F).ThenBy(pair => pair.Value.H).ToDictionary(p=>p.Key,o=>o.Value);
                var node = m_OpenDic.First().Value;
                m_OpenDic.Remove(node.Position);
                // 计算相邻节点的计算结果
                OperateNeighborNodes(node);
                // 把当前节点添加到close列中
                AddNodeToClosedDic(node);
                yield return null;
            }

            if (m_DestinationNode == null)
                NsnLog.Error("找不到目标点");
            else
                ShowPath(m_DestinationNode);
        }

        /// <summary>
        /// 添加到Open列中
        /// </summary>
        /// <param name="node"></param>
        private void AddNodeToOpenDic(NavigateNode node)
        {
            m_OpenDic[node.Position] = node;
            RefreshAStarHint(node);
        }

        /// <summary>
        /// 添加到关闭列中
        /// </summary>
        /// <param name="node"></param>
        public void AddNodeToClosedDic(NavigateNode node)
        {
            m_ClosedDic.Add(node.Position,node);
            // 把地图上对应的item设置为close的状态
            var mapItem = m_Map[node.Position.x, node.Position.y];
            mapItem.ChangeInOpenStateToInClose();
        }

        /// <summary>
        /// 刷新Node对应的地图格子的 Hint信息
        /// </summary>
        /// <param name="node"></param>
        private void RefreshAStarHint(NavigateNode node)
        {
            var mapItem = m_Map[node.Position.x, node.Position.y];
            var forward = Vector2.zero;
            if (node.Parent != null)
            {
                float offsetX = node.Parent.Position.x - node.Position.x;
                float offsetY = node.Parent.Position.y - node.Position.y;
                forward = new Vector2(offsetX, offsetY);
            } 
            mapItem.RefreshAStarHint(node.G,node.H,node.F,forward);
        }


        /// <summary>
        /// 计算相邻的节点
        /// </summary>
        /// <param name="node"></param>
        private void OperateNeighborNodes(NavigateNode node)
        {
            // 遍历上下左右的节点
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if(i == 0 && j == 0) continue;

                    Int2 pos = new Int2(node.Position.x + i, node.Position.y + j);
                    // 地图外
                    if(pos.x < 0 || pos.x >= m_MapSize.x || pos.y < 0 || pos.y >= m_MapSize.y)
                        continue;
                    // 已经计算过
                    if(m_ClosedDic.ContainsKey(pos))
                        continue;
                    // 障碍物
                    var mapItem = m_Map[pos.x, pos.y];
                    if(mapItem.ItemState == NavigateGridItemState.Obstacle)
                        continue;
                    // 水平or垂直的点
                    if (i == 0 || j == 0)
                        AddNeighborNodeToDic(node, pos, FACTOR);
                    // 斜对角的点
                    else
                        AddNeighborNodeToDic(node, pos, FACTOR_DIAGONAL);
                }
            }
        }

        private void AddNeighborNodeToDic(NavigateNode parentNode, Int2 position, int g)
        {
            int nodeG = parentNode.G + g;
            if (m_OpenDic.TryGetValue(position, out var node))
            {
                if (nodeG < node.G)
                {
                    node.G = nodeG;
                    node.Parent = parentNode;
                    RefreshAStarHint(node);
                }
            }
            else
            {
                NavigateNode newNode = new NavigateNode(position, parentNode, nodeG, GetH(position));
                if (position == m_TargetPos)
                    m_DestinationNode = newNode;
                else
                    AddNodeToOpenDic(newNode);
            }
        }

        private int GetH(Int2 position)
        {
            switch (m_EvaluationType)
            {
                case NavigateEvaluationType.Diagonal:
                    return GetDiagonalDistance(position);
                case NavigateEvaluationType.Euclidean:
                    return Mathf.CeilToInt(GetEuclideanDistance(position));
                case NavigateEvaluationType.Manhattan:
                    return GetManhattanDistance(position);
            }
            return 0;
        }

        /// <summary>
        /// 计算对角线距离
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private int GetDiagonalDistance(Int2 position)
        {
            // Octile 举例 参考 Nsn.AStarHeuristic.OctileDist()
            int x = Mathf.Abs(m_TargetPos.x - position.x);
            int y = Mathf.Abs(m_TargetPos.y - position.y);
            int min = Mathf.Min(x, y);
            return min * FACTOR_DIAGONAL + Mathf.Abs(x - y) * FACTOR;
        }
        
        /// <summary>
        /// 计算曼哈顿距离(上下左右)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        int GetManhattanDistance(Int2 position) {
            // 参考 Nsn.AStarHeuristic.ManhattanDist()
            return Mathf.Abs(m_TargetPos.x - position.x) * FACTOR + Mathf.Abs(m_TargetPos.y - position.y) * FACTOR;
        }
        
        /// <summary>
        /// 计算欧式距离(两点之间的直接距离)
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        float GetEuclideanDistance(Int2 position) {
            // 参考 Nsn.AStarHeuristic.EuclideanDist()
            return Mathf.Sqrt(Mathf.Pow((m_TargetPos.x - position.x) * FACTOR, 2) + Mathf.Pow((m_TargetPos.y - position.y) * FACTOR, 2));
        }
        
        /// <summary>
        /// 显示路径
        /// </summary>
        /// <param name="node"></param>
        private void ShowPath(NavigateNode node)
        {
            while (node != null)
            {
                var mapItem = m_Map[node.Position.x, node.Position.y];
                mapItem.ChangeToPathState();
                node = node.Parent;
            }
        }
        
        

        public void Clear()
        {
            foreach(var pos in m_OpenDic.Keys) {
                m_Map[pos.x, pos.y].ClearAStarHint();
            }
            m_OpenDic.Clear();

            foreach(var pos in m_ClosedDic.Keys) {
                m_Map[pos.x, pos.y].ClearAStarHint();
            }
            m_ClosedDic.Clear();

            m_DestinationNode = null;
            m_Initialized = false;
        }
    }
}