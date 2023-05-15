using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public AudioClip DialogueAudio { get; set; }
        
        public DialogueNodeType DialogueNodeType { get; set; }

        public NsnDialogueGroup Group { get; set; }

        public List<NsnDialogueChoiceSaveData> Choices { get; set; }

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
            TextContent = "Input Content ...";
            Choices = new List<NsnDialogueChoiceSaveData>();

            m_DialogueGraphView = graphView;
            m_DefaultBackgroundColor = new Color(29f / 255f, 29f / 255f, 30f / 255f);

            mainContainer.AddToClassList("ds-node__main-container");
            extensionContainer.AddToClassList("ds-node__extension-container");

            SetPosition(new Rect(position, Vector2.zero));
        }

        public virtual void OnDraw()
        {
            DrawTitle();
            DrawInputPort();
            DrawExtensionContainer();
        }

        protected void DrawTitle()
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

                if (Group == null)
                {
                    m_DialogueGraphView.RemoveUngroupedNode(this);
                    DialogueName = target.value;
                    m_DialogueGraphView.AddUngroupedNode(this);
                }
                else
                {
                    NsnDialogueGroup group = Group;
                    m_DialogueGraphView.RemoveGroupedNode(this, Group);
                    DialogueName = target.value;
                    m_DialogueGraphView.AddGroupedNode(this, group);
                }
            });

            titleContainer.Insert(0, dialogueNameTF);
        }

        protected void DrawInputPort()
        {
            Port inputPort = CreatePort("Dialogue Connection", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "Input";
            inputContainer.Add(inputPort);
        }

        protected void DrawExtensionContainer()
        {
            VisualElement customDataContainer = new VisualElement();
            customDataContainer.AddToClassList("ds-node__custom-data-container");
            // todo : this button add proterty of node, eg : audio , test, timeline asset
            Button addCompBtn = VEToolKit.CreateButton("Test Btn", () =>{ Debug.Log("Add Property .. "); });
            Foldout contentTestFoldout = new Foldout() { text = "Dialogue Content Info" };

            TextField contentTextField = new TextField() { value = TextContent };
            contentTextField.RegisterValueChangedCallback(ce => { TextContent = ce.newValue; });
            contentTextField.AddToClassList("ds-node__text-field");
            contentTextField.AddToClassList("ds-node__quote-text-field");

            contentTestFoldout.Add(addCompBtn);
            contentTestFoldout.Add(contentTextField);

            customDataContainer.Add(contentTestFoldout);
            
            extensionContainer.Add(customDataContainer);
        }

        protected Port CreatePort(string portName = "", Orientation orientation = Orientation.Horizontal, Direction direction = Direction.Output, Port.Capacity capacity = Port.Capacity.Single)
        {
            Port port = this.InstantiatePort(orientation, direction, capacity, typeof(bool));
            port.name = portName;
            return port;
        }


        #region Ports

        public bool IsStartingNode()
        {
            Port inputPort = (Port)inputContainer.Children().First();
            return !inputPort.connected;
        }

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

        public void DisconnectAllPorts()
        {
            DisconnectInputPorts();
            DisconnectOutputPorts();
        }

        #endregion

        #region Style

        public void SetErrorStyle(Color color) => mainContainer.style.backgroundColor = color;

        public void ResetErrorStyle() => mainContainer.style.backgroundColor = m_DefaultBackgroundColor;

        #endregion

    }
}

