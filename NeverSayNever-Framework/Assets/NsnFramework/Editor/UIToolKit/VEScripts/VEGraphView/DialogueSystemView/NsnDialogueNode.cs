using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nsn.EditorToolKit
{
    public class NsnDialogueNode : NsnBaseNode
    {
        protected NsnGraphView m_GraphView;

        public string ID { get; set; }

        public string DialogueName { get; set; }

        public string TextContent { get; set; }


        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Disconnect Input Ports", action => DisconnectInputPorts());
            evt.menu.AppendAction("Disconnect Output Ports", action => DisconnectOutputPorts());

            base.BuildContextualMenu(evt);
        }

        public virtual void Initialize()
        {
            // todo
        }

        #region Ports

        private void DisconnectInputPorts()
        {
            DisconnectPorts(inputContainer);
        }

        private void DisconnectOutputPorts()
        {
            DisconnectPorts(outputContainer);
        }

        private void DisconnectPorts(VisualElement container)
        {
            foreach(Port port in container.Children())
            {
                if(port.connected == false)
                    continue;
                m_GraphView.DeleteElements(port.connections);
            }
        }

        #endregion

    }
}

