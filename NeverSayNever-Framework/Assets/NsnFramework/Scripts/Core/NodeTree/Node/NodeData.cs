using System;
using UnityEngine;

namespace NeverSayNever.NodeGraphView
{
    [System.Serializable]
    public class NodeValueData
    {
        [SerializeField]
        string _name;
        [SerializeField]
        object _value;

        public NodeValueData(string name)
        {
            _name = name;
            _value = new object();
        }

        public string Name => _name;
        public object Value { get => _value; set => _value = value; }
    }

    [Serializable]
    public class NodePortData
    {
        [SerializeField]
        string m_FieldName;
        [SerializeField]
        int m_PortType;
        [SerializeField]
        int m_PortIndex;

        public string FieldName => m_FieldName;
        public int PortType => m_PortType;
        public int PortIndex { get => m_PortIndex; set => m_PortIndex = value; }

        public NodePortData(string fieldName, int portIndex, int portType)
        {
            m_FieldName = fieldName;
            m_PortType = portType;
            m_PortIndex = portIndex;
        }
    }

    [System.Serializable]
    public class NodeLinkData
    {
        public BaseNode sourceNode;
        public string outputValueName;
        public string inputValueName;

        public NodeLinkData(BaseNode sourceNode, string outputValueName, string inputValueName)
        {
            this.sourceNode = sourceNode;
            this.outputValueName = outputValueName;
            this.inputValueName = inputValueName;
        }
    }


}
