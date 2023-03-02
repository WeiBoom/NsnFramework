using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nsn.EditorToolKit
{
    public enum DialogueNodeType
    {
        SingleChoice,
        MultipleChoice,
    }

    public class NsnDialogueNode : NsnBaseNode
    {
        protected NsnDialogueGraphView m_DialogueGraphView;

        private Color m_DefaultBackgroundColor;

        public string ID { get; set; }

        public string DialogueName { get; set; }

        public string TextContent { get; set; }

        public NsnDialogueGroup DialogueGroup { get; set; } 


        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Disconnect Input Ports", action => DisconnectInputPorts());
            evt.menu.AppendAction("Disconnect Output Ports", action => DisconnectOutputPorts());

            base.BuildContextualMenu(evt);
        }

        public virtual void OnInitialize(string nodeName, NsnDialogueGraphView graphView, Vector2 position)
        {
            ID = Guid.NewGuid().ToString();

            DialogueName = nodeName;
            TextContent = "Dialogue Content";
            
            m_DialogueGraphView = graphView;
            m_DefaultBackgroundColor = new Color(29f / 255f, 29f / 255f, 30f / 255f);

            mainContainer.AddToClassList("ds-node__main-container");
            extensionContainer.AddToClassList("ds-node__extension-container");

            SetPosition(new Rect(position, Vector2.zero));
        }

        public virtual void OnDraw()
        {
            DrawTitle();
            DrawPorts();
            DrawExtensionContainer();
        }

        private void DrawTitle()
        {
            TextField dialogueNameTF = new TextField() { value = DialogueName };

            dialogueNameTF.AddToClassList("ds-node__text-field");
            dialogueNameTF.AddToClassList("ds-node__text-field__hidden");
            dialogueNameTF.AddToClassList("ds-node__filename-text-field");

            dialogueNameTF.RegisterValueChangedCallback((ce) =>
            {
                TextField target = (TextField)ce.target;
                target.value = ce.newValue; // todo : RemoveWhitespaces().RemoveSpecialCharacters();

                if (string.IsNullOrEmpty(target.value))
                {
                    if (!string.IsNullOrEmpty(DialogueName))
                        ++m_DialogueGraphView.NameErrorAmount;
                }
                else
                {
                    if (string.IsNullOrEmpty(DialogueName))
                        --m_DialogueGraphView.NameErrorAmount;
                }

                if (DialogueGroup == null)
                {
                    m_DialogueGraphView.RemoveUngroupedNode(this);
                    DialogueName = target.value;
                    m_DialogueGraphView.AddUngroupedNode(this);
                }
                else
                {
                    NsnDialogueGroup group = DialogueGroup;
                    m_DialogueGraphView.RemoveGroupedNode(this, DialogueGroup);
                    DialogueName = target.value;
                    m_DialogueGraphView.AddGroupedNode(this, group);
                }
            });
        }

        private void DrawPorts()
        {
            Port inputPort = this.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
            inputPort.name = "Dialogue Connection";
            inputContainer.Add(inputPort);
        }

        private void DrawExtensionContainer()
        {
            VisualElement customDataContainer = new VisualElement();

            customDataContainer.AddToClassList("ds-node__custom-data-container");

            Foldout contentTestFoldout = new Foldout()
            {
                text = title,
            };

            TextField contentTextField = new TextField() { value = "Dialogue Text" };
            contentTextField.AddToClassList("ds-node__text-field");
            contentTextField.AddToClassList("ds-node__quote-text-field");

            contentTestFoldout.Add(contentTextField);
            customDataContainer.Add(contentTestFoldout);
            extensionContainer.Add(customDataContainer);
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
                m_DialogueGraphView.DeleteElements(port.connections);
            }
        }

        #endregion

        #region Style



        #endregion

    }
}

