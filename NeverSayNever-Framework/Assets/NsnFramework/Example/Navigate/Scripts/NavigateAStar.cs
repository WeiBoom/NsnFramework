using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nsn;
using UnityEngine;

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

        private Dictionary<Int2, NavigateNode> m_OpenDic = new Dictionary<Int2, NavigateNode>();
        private Dictionary<Int2, NavigateNode> m_ClosedDic = new Dictionary<Int2, NavigateNode>();

        private NavigateNode m_DestinationNode;

        /// <summary>
        /// 初始化寻路数据
        /// </summary>
        /// <param name="map">mao数据</param>
        /// <param name="mapSize">地图大小</param>
        /// <param name="start">初始位置</param>
        /// <param name="destination">目标位置</param>
        public void Init(Navigate2DMapGridItem[,] map, Int2 mapSize, Int2 start, Int2 destination)
        {
            m_Map = map;
            m_MapSize = mapSize;
            m_StartPos = start;
            m_TargetPos = destination;

            m_OpenDic.Clear();
            m_ClosedDic.Clear();

            m_DestinationNode = null;

            var startNode = new NavigateNode(start, null, 0, 0);
            AddNodeToOpenDic(startNode);

            m_Initialized = true;
        }

        public IEnumerator Execute()
        {
            while (m_OpenDic.Count > 0 && m_DestinationNode == null)
            {
                m_OpenDic = m_ClosedDic.OrderBy(pair => pair.Value.F).ThenBy(pair => pair.Value.H).ToDictionary(p=>p.Key,o=>o.Value);
                var node = m_OpenDic.First().Value;
                m_OpenDic.Remove(node.Position);
                
                
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
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if(i == 0 && j == 0) continue;
                }
            }
        }

        private void ShowPath(NavigateNode node)
        {
            while (node != null)
            {
                var mapItem = m_Map[node.Position.x, node.Position.y];
                mapItem.ChangeToPathState();
                node = node.Parent;
            }
        }
    }
}