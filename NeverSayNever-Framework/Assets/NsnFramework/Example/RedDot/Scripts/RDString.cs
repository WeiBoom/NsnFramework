using System;

namespace Nsn.Example
{
    public class RDString : IEquatable<RDString>
    {
        private readonly string m_Source;

        private readonly int m_StartIndex;
        private readonly int m_EndIndex;
        private int m_Length;

        private readonly bool m_SourceIsNullOrEmpty;
        private readonly int m_HashCode;

        public RDString(string source, int startIndex, int endIndex)
        {
            m_Source = source;
            m_StartIndex = startIndex;
            m_EndIndex = endIndex;
            m_Length = endIndex - startIndex + 1;
            m_SourceIsNullOrEmpty = string.IsNullOrEmpty(source);
            if (!m_SourceIsNullOrEmpty)
            {
                for (int i = m_StartIndex; i <= m_EndIndex; i++)
                    m_HashCode = 31 * m_HashCode + m_Source[i];
            }
        }
        
        public bool Equals(RDString other)
        {
            if (other == null)
                return false;

            bool isOtherNullOrEmpty = string.IsNullOrEmpty(other.m_Source);
            if (m_SourceIsNullOrEmpty && isOtherNullOrEmpty)
                return true;
            if (m_SourceIsNullOrEmpty || isOtherNullOrEmpty)
                return false;

            if (m_Length != other.m_Length)
                return false;
            
            for (int i = m_StartIndex, j = other.m_StartIndex; i <= m_EndIndex; i++, j++)
            {
                if (m_Source[i] != other.m_Source[j])
                    return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RDString)obj);
        }

        public override int GetHashCode()
        {
            return m_HashCode;
        }
    }
}