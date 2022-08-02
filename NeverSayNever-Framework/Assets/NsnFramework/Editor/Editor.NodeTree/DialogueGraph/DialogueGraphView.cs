using System.Collections;
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


        public DialogueGraphView()
        {
            var styleSheet = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/NsnFramework/Editor/Editor.NodeTree/Data/DialogueGraph.uss", typeof(StyleSheet));
            styleSheets.Add(styleSheet as StyleSheet);
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

        // create new dialogue node

        public DialogueNode CreateDialogueNode(string nodeName)
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
            dialogueNode.SetPosition(new Rect(Vector2.zero, DefaultNodeSize));

            return dialogueNode;
        }

        private void AddChoicePort(DialogueNode dialogueNode)
        {

        }

    }
}
