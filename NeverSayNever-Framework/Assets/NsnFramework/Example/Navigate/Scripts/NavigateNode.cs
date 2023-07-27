namespace Nsn.Example
{
    public class NavigateNode
    {
        private Int2 m_Position;
        public Int2 Position => m_Position;

        public NavigateNode Parent;

        private int m_G;
        private int m_F;
        private int m_H;

        public int G
        {
            get => m_G;
            set
            {
                m_G = value;
                m_F = m_G + m_H;
            }
        }

        public int H
        {
            get => m_H;
            set
            {
                m_H = value;
                m_F = m_G + m_H;
            }
        }

        public int F => m_F;

        public NavigateNode(Int2 pos, NavigateNode parent, int g, int h)
        {
            m_Position = pos;
            Parent = parent;
            m_G = g;
            m_H = h;
            m_F = g + h;
        }
    }
}