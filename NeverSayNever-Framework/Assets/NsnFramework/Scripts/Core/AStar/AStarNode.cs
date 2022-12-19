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
        /// ���ڵĽڵ�
        /// </summary>
        public AStarNode Connection { get; private set; }
        /// <summary>
        /// ������ƶ����ýڵ�Ĵ���
        /// </summary>
        public float G { get; private set; }
        /// <summary>
        /// �Ӹýڵ��ƶ����յ�Ĵ���
        /// </summary>
        public float H { get; private set; }
        /// <summary>
        /// �ܵĴ���ֵ
        /// </summary>
        public float F => G + H;
        /// <summary>
        /// �Ƿ��ͨ��
        /// </summary>
        public bool Walkable { get; private set; }
        /// <summary>
        /// ���ڵĽڵ�
        /// </summary>
        public List<AStarNode> Neighbors { get; protected set; }
        /// <summary>
        /// ��ǰ�ڵ��״̬
        /// </summary>
        public AStarNodeStatus Status { get; protected set; }
        /// <summary>
        /// �ڵ�����Ӧ������
        /// </summary>
        public IAStarNodeCoord Coord { get; protected set; }

        /// <summary>
        /// �ڵ�״̬�ı�ʱ�Ļص�
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

