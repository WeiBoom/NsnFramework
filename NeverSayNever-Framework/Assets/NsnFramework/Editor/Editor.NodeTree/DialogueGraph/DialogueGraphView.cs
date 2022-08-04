using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEditor.Experimental.GraphView;


namespace NeverSayNever.NodeGraphView
{
    public class DialogueGraphView : GraphView
    {
        public readonly Vector2 DefaultNodeSize = new Vector2(200, 150);
        public readonly Vector2 DefaultCommentBlockSize = new Vector2(300, 200);
        public DialogueNode EntryPointNode;

        #region StyleSheet

        private StyleSheet _dialogueViewStyleSheet;
        private StyleSheet DialogueViewStyleSheet
        {
            get
            {
                if (_dialogueViewStyleSheet == null)
                    _dialogueViewStyleSheet = LoadStyleSheet("DialogueGraph");
                //UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/NsnFramework/Editor/Editor.NodeTree/Data/DialogueGraph.uss", typeof(StyleSheet)) as StyleSheet;
                return _dialogueViewStyleSheet;
            }
        }

        private StyleSheet _dialogueNodeStyleSheet;
        private StyleSheet DialogueNodeStyleSheet
        {
            get
            {
                if (_dialogueNodeStyleSheet == null)
                    _dialogueNodeStyleSheet = LoadStyleSheet("DialogueNode");
                //UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/NsnFramework/Editor/Editor.NodeTree/Data/DialogueNode.uss", typeof(StyleSheet)) as StyleSheet;
                return _dialogueNodeStyleSheet;
            }
        }


        private StyleSheet _dialogueStartNodeStyleSheet;
        private StyleSheet DialogueStartNodeStyleSheet
        {
            get
            {
                if (_dialogueStartNodeStyleSheet == null)
                    _dialogueStartNodeStyleSheet = LoadStyleSheet("DialogueStartNode");
                //UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/NsnFramework/Editor/Editor.NodeTree/Data/DialogueStartNode.uss", typeof(StyleSheet)) as StyleSheet;
                return _dialogueStartNodeStyleSheet;
            }
        }

        #endregion

        public DialogueGraphView()
        {
            styleSheets.Add(DialogueViewStyleSheet);
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            // background grid;
            var gridBackground = new GridBackground();
            Insert(0, gridBackground);
            gridBackground.StretchToParentSize();

            EntryPointNode = GetEntryPointNodeInstance();
            // add entry node
            AddElement(EntryPointNode);
        }

        private StyleSheet LoadStyleSheet(string sheetName)
        {
            //var styleSheet = Resources.Load<StyleSheet>($"Theme/{sheetName}.uss");
            string filePath = $"Assets/NsnFramework/Editor/Editor.NodeTree/Data/{sheetName}.uss";
            var styleSheet= UnityEditor.AssetDatabase.LoadAssetAtPath(filePath, typeof(StyleSheet)) as StyleSheet;
            return styleSheet;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            var startPortView = startPort;

            ports.ForEach((port) =>
            {
                var portView = port;
                if (startPortView != portView && startPortView.node != portView.node)
                    compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }

        // generate entry node
        private DialogueNode GetEntryPointNodeInstance()
        {
            var nodeCache = new DialogueNode()
            {
                title = "START",
                GUID = Guid.NewGuid().ToString(),
                DialogueText = "ENTRYPOINT",
                EntryPoint = true
            };

            nodeCache.styleSheets.Add(DialogueStartNodeStyleSheet);
            // only one output port
            var generatedPort = GetPortInstance(nodeCache, Direction.Output);
            generatedPort.portName = "Next";
            nodeCache.outputContainer.Add(generatedPort);

            // can't move
            nodeCache.capabilities &= ~Capabilities.Movable;
            // can't delete
            nodeCache.capabilities &= ~Capabilities.Deletable;

            nodeCache.RefreshExpandedState();
            nodeCache.RefreshPorts();
            nodeCache.SetPosition(new Rect(100, 200, 100, 150));
            return nodeCache;
        }

        // generate port for node
        private Port GetPortInstance(DialogueNode node, Direction nodeDirection, Port.Capacity capacity = Port.Capacity.Single)
        {
            return node.InstantiatePort(Orientation.Horizontal, nodeDirection, capacity, typeof(float));
        }


        public void CreateNewDialogueNode(string fileName, Vector2 position)
        {
            DialogueNode node = CreateDialogueNode(fileName, position);
            AddElement(node);
        }

        // create new dialogue node

        public DialogueNode CreateDialogueNode(string nodeName, Vector2 position)
        {
            DialogueNode dialogueNode = new DialogueNode
            {
                name = nodeName,
                DialogueText = nodeName,
                GUID = System.Guid.NewGuid().ToString(),
            };

            // accept mulit input info
            var inputPort = GetPortInstance(dialogueNode, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "Input";
            dialogueNode.inputContainer.Add(inputPort);

            dialogueNode.styleSheets.Add(DialogueNodeStyleSheet);
            // button for add choice
            var button = new UnityEngine.UIElements.Button(() => { AddChoicePort(dialogueNode); })
            {
                text = "Add Choice"
            };

            // add button to titleContainer
            dialogueNode.title = "Add Choice";
            dialogueNode.titleContainer.Add(button);

            dialogueNode.RefreshExpandedState();
            dialogueNode.RefreshPorts();
            dialogueNode.SetPosition(new Rect(position, DefaultNodeSize));

            return dialogueNode;
        }

        public void AddChoicePort(DialogueNode nodeCache, string overriddenPortName = "")
        {
            var generatedPort = GetPortInstance(nodeCache, Direction.Output);
            var portLabel = generatedPort.contentContainer.Q<Label>("type");
            generatedPort.contentContainer.Remove(portLabel);

            var outputPortCount = nodeCache.outputContainer.Query("connector").ToList().Count();
            var outputPortName = string.IsNullOrEmpty(overriddenPortName)
                ? $"Option {outputPortCount + 1}"
                : overriddenPortName;

            var textField = new TextField()
            {
                name = string.Empty,
                value = outputPortName
            };
            textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
            generatedPort.contentContainer.Add(new Label("  "));
            generatedPort.contentContainer.Add(textField);
            var deleteButton = new Button(() => RemovePort(nodeCache, generatedPort))
            {
                text = "X"
            };
            generatedPort.contentContainer.Add(deleteButton);
            generatedPort.portName = outputPortName;
            nodeCache.outputContainer.Add(generatedPort);
            nodeCache.RefreshPorts();
            nodeCache.RefreshExpandedState();
        }

        private void RemovePort(Node node, Port socket)
        {
            var targetEdge = edges.ToList()
                .Where(x => x.output.portName == socket.portName && x.output.node == socket.node);
            if (targetEdge.Any())
            {
                var edge = targetEdge.First();
                edge.input.Disconnect(edge);
                RemoveElement(targetEdge.First());
            }

            node.outputContainer.Remove(socket);
            node.RefreshPorts();
            node.RefreshExpandedState();
        }


        public void ClearBlackBoardAndExposedProperties()
        {
            // ExposedProperties.Clear();
            // Blackboard.Clear();
        }

        public Group CreateCommentBlock(Rect rect, DialogueCommentBlockData commentBlockData = null)
        {
            // todo
            return null;
        }

        public void AddPropertyToBlackBoard(ExposedProperty property, bool loadMode = false)
        {
            // todo
        }
    }
}

