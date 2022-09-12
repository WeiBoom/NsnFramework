using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverSayNever
{
    using AStarNodeStatus = AStarNode.AStarNodeStatus;
    public static class AStarPathFinding
    {
        public static List<AStarNode> FindPath(AStarNode startNode, AStarNode targetNode)
        {
            // 搜索列表
            var toSearch = new List<AStarNode>() { startNode };
            // 已经检查过的节点列表
            var processed = new List<AStarNode>();

            while (toSearch.Any())
            {
                var current = toSearch[0];
                // 获取到搜索列表中 F值最小 或 同等F情况下H值最小的节点
                foreach(var t in toSearch)
                {
                    if (t.F < current.F || t.F == current.F && t.H < current.H)
                        current = t;
                }

                current.SetStatus(AStarNodeStatus.Closed);

                // 如果当前节点是目标节点的话
                if(current == targetNode)
                {
                    var currentPathTile = targetNode;
                    var path = new List<AStarNode>();
                    // 回溯节点，得到最终路径
                    while(currentPathTile != startNode)
                    {
                        path.Add(currentPathTile);
                        currentPathTile = currentPathTile.Connection;
                    }
                    foreach (var tile in path) tile.SetStatus(AStarNodeStatus.Selected);
                    startNode.SetStatus(AStarNodeStatus.Selected);
                    return path;
                }

                // 添加到已检查的列表中
                processed.Add(current);
                // 从搜索列表中移除当前节点
                toSearch.Remove(current);

                // 遍历所有邻居节点，检查是否可通行且并没有被检查过
                foreach(var neighbor in current.Neighbors.Where(t=>t.Walkable && !processed.Contains(t)))
                {
                    // 检查是否在搜索列表中
                    var inSearch = toSearch.Contains(neighbor);
                    // 计算到邻居节点所需要花费的代价
                    var costToNeighbor = current.G + current.GetDistance(neighbor);
                    // 如果没在搜索队列中 或者 到达该节点的代价值 小于 到达邻居节点的代价值
                    if(!inSearch || costToNeighbor < neighbor.G)
                    {
                        // 更新邻居节点的G值
                        neighbor.SetG(costToNeighbor);
                        // 设置连接节点
                        neighbor.SetConnection(current);
                        if(!inSearch)
                        {
                            // 添加到搜索列表并更新H值
                            neighbor.SetH(neighbor.GetDistance(targetNode));
                            toSearch.Add(neighbor);
                            neighbor.SetStatus(AStarNodeStatus.Open);
                        }
                    }
                }

            }
            return null;
        }
    }
}
