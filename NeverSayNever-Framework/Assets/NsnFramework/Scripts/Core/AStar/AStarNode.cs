using System.Collections;
using System.Collections.Generic;

namespace Nsn
{
    public interface IAStarNodeCoord
    {
        float GetDistance(IAStarNodeCoord other);
        UnityEngine.Vector2 Pos { get; set; }
    }

    public class AStarNode
    {
        public enum AStarNodeStatus
        {
            None = 0,
            Selected,
            Open,
            Closed,
        }

        /// <summary>
        /// 相邻的节点
        /// </summary>
        public AStarNode Connection { get; private set; }
        /// <summary>
        /// 从起点移动到该节点的代价
        /// </summary>
        public float G { get; private set; }
        /// <summary>
        /// 从该节点移动到终点的代价
        /// </summary>
        public float H { get; private set; }
        /// <summary>
        /// 总的代价值
        /// </summary>
        public float F => G + H;
        /// <summary>
        /// 是否可通行
        /// </summary>
        public bool Walkable { get; private set; }
        /// <summary>
        /// 相邻的节点
        /// </summary>
        public List<AStarNode> Neighbors { get; protected set; }
        /// <summary>
        /// 当前节点的状态
        /// </summary>
        public AStarNodeStatus Status { get; protected set; }
        /// <summary>
        /// 节点所对应的坐标
        /// </summary>
        public IAStarNodeCoord Coord { get; protected set; }

        /// <summary>
        /// 节点状态改变时的回调
        /// </summary>
        public event System.Action<AStarNodeStatus> OnStatusChangedFunc;

        public void SetConnection(AStarNode node) => Connection = node;

        public void SetStatus(AStarNodeStatus status)
        {
            Status = status;
            OnStatusChangedFunc?.Invoke(status);
        }

        public void SetG(float g) => G = g;

        public void SetH(float h) => H = h;

        public float GetDistance(AStarNode other)
        {
            return Coord.GetDistance(other.Coord);
        }

        public void Clear()
        {
            G = 0;
            H = 0;
            Status = AStarNodeStatus.None;
            Connection = null;
            Neighbors?.Clear();
        }
    }
}

