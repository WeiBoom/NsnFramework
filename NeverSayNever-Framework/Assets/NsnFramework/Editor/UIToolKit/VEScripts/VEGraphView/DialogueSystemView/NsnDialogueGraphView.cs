using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nsn.EditorToolKit
{

    using Nsn;

    public class NsnDialogueGraphView : NsnBaseGraphView
    {
        private SerializableDictionary<string, NsnDialogueNodeErrorData> m_UngroupedNodes;
        private SerializableDictionary<string, NsnDialogueGroupErrorData> m_Groups;
        private SerializableDictionary<NsnDialogueGroup, SerializableDictionary<string, NsnDialogueNodeErrorData>> m_GroupedNodes;

        // Visual Element
        private NsnDialogueEditorWindow m_DialogueEditorWindow;
        private NsnDialogueSearchEditorWindow m_DialogueSearchEditorWindow;
        private MiniMap m_MiniMap;

        private int m_NameErrorAmount = 0;
        public int NameErrorAmount
        {
            get { return m_NameErrorAmount; }
            set { 
                m_NameErrorAmount = value; 
                // m_NameErrorAmount 是递增的,只要曾大于1，就设为false，不用一直刷新
                if(NameErrorAmount == 0)
                    m_DialogueEditorWindow.EnableSaving(true);
                if(NameErrorAmount == 1)
                    m_DialogueEditorWindow.EnableSaving(false);
            }
        }

        public NsnDialogueGraphView(NsnDialogueEditorWindow dialogueEditorWindow)
        {
            m_DialogueEditorWindow = dialogueEditorWindow;

            m_UngroupedNodes = new SerializableDictionary<string, NsnDialogueNodeErrorData>();
            m_Groups = new SerializableDictionary<string, NsnDialogueGroupErrorData>();
            m_GroupedNodes = new SerializableDictionary<NsnDialogueGroup, SerializableDictionary<string, NsnDialogueNodeErrorData>>();

            AddManipulators();
            AddGridBackground();
            AddSearchWindow();
            AddMiniMap();

            AddSytleSheets();
            AddMiniMapStyles();

            OnElementsDeleted();
            OnGroupElementsAdded();
            OnGroupElementsRemoved();
            OnGroupRenamed();
            OnGraphViewChanged();
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();
            ports.ForEach(port =>
            {
                if (startPort == port)
                    return;
                if (startPort.node == port.node)
                    return;
                if (startPort.direction == port.direction)
                    return;
                compatiblePorts.Add(port);
            });
            return compatiblePorts;
        }


        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            this.AddManipulator(CreateNodeContextualMenu("Add Node (Single Choice)", DialogueNodeType.SingleChoice));
            this.AddManipulator(CreateNodeContextualMenu("Add Node (Multiple Choice)", DialogueNodeType.MultipleChoice));
            this.AddManipulator(CreateGroupContextualMenu());
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }

        private void AddSearchWindow()
        {
            if (m_DialogueSearchEditorWindow == null)
                m_DialogueSearchEditorWindow = ScriptableObject.CreateInstance<NsnDialogueSearchEditorWindow>();
            m_DialogueSearchEditorWindow.Initialize(this);
            nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), m_DialogueSearchEditorWindow);
        }

        private void AddMiniMap()
        {
            m_MiniMap = new MiniMap() { anchored = true };
            m_MiniMap.SetPosition(new Rect(15, 50, 200, 180));

            Add(m_MiniMap);
            m_MiniMap.visible = false;
        }

        private void AddSytleSheets()
        {
            var graphStyleSheet = VEToolKit.LoadVEAssetStyleSheet(NEditorConst.NsnStyleSheet_DialogueGraphView);
            this.styleSheets.Add(graphStyleSheet);

            var nodeStyleSheet = VEToolKit.LoadVEAssetStyleSheet(NEditorConst.NsnStyleSheet_DialogueNode);
            this.styleSheets.Add(nodeStyleSheet);
        }

        private void AddMiniMapStyles()
        {
            StyleColor backgroundColor = new StyleColor(new Color32(29, 29, 30, 255));
            StyleColor borderColor = new StyleColor(new Color32(51, 51, 51, 255));

            m_MiniMap.style.backgroundColor = backgroundColor;
            m_MiniMap.style.borderTopColor = borderColor;
            m_MiniMap.style.borderRightColor = borderColor;
            m_MiniMap.style.borderBottomColor = borderColor;
            m_MiniMap.style.borderLeftColor = borderColor;
        }

        public void AddGroupedNode(NsnDialogueNode node, NsnDialogueGroup group)
        {
            string nodeName = node.DialogueName.ToLower();
            node.Group = group;

            if (!m_GroupedNodes.ContainsKey(group))
                m_GroupedNodes.Add(group, new SerializableDictionary<string, NsnDialogueNodeErrorData>());
        
            if(!m_GroupedNodes[group].ContainsKey(nodeName))
            {
                NsnDialogueNodeErrorData nodeErrorData = new NsnDialogueNodeErrorData();
                nodeErrorData.Nodes.Add(node);
                m_GroupedNodes[group].Add(nodeName, nodeErrorData);
            }
            else
            {
                var targetGroup = m_GroupedNodes[group][nodeName];
                var groupedNodesList = targetGroup.Nodes;
                groupedNodesList.Add(node);
                Color errorColor = targetGroup.ErrorData.Color;
                if(groupedNodesList.Count == 2)
                {
                    ++NameErrorAmount;
                    groupedNodesList[0].SetErrorStyle(errorColor);
                }
            }
        }

        public void AddUngroupedNode(NsnDialogueNode node)
        {
            string nodeName = node.DialogueName.ToLower();

            if(!m_UngroupedNodes.ContainsKey(nodeName))
            {
                NsnDialogueNodeErrorData nodeErrorData = new NsnDialogueNodeErrorData();
                nodeErrorData.Nodes.Add(node);
                m_UngroupedNodes.Add(nodeName, nodeErrorData);
                return;
            }

            var ungroupedNodesList = m_UngroupedNodes[nodeName].Nodes;
            ungroupedNodesList.Add(node);

            Color errorColor = m_UngroupedNodes[nodeName].ErrorData.Color;
            node.SetErrorStyle(errorColor);

            if(ungroupedNodesList.Count == 2)
            {
                ++NameErrorAmount;
                ungroupedNodesList[0].SetErrorStyle(errorColor);
            }
        }

        public void RemoveGroupedNode(NsnDialogueNode node, NsnDialogueGroup group)
        {
            string nodeName = node.DialogueName.ToLower();
            node.Group = null;

            List<NsnDialogueNode> groupedNodeList = m_GroupedNodes[group][nodeName].Nodes;
            groupedNodeList.Remove(node);
            node.ResetErrorStyle();

            if (groupedNodeList.Count == 1)
            {
                --NameErrorAmount;
                groupedNodeList[0].ResetErrorStyle();
            }
            else if(groupedNodeList.Count == 0)
            {
                m_GroupedNodes[group].Remove(nodeName);
                if(m_GroupedNodes[group].Count == 0)
                    m_GroupedNodes.Remove(group);
            }
        }

        public void RemoveUngroupedNode(NsnDialogueNode node)
        {
            string nodeName = node.DialogueName.ToLower();
            List<NsnDialogueNode> groupedNodeList = m_UngroupedNodes[nodeName].Nodes;

            groupedNodeList.Remove(node);
            node.ResetErrorStyle();

            if(groupedNodeList.Count == 1)
            {
                --NameErrorAmount;
                groupedNodeList[0].ResetErrorStyle();
            }
            else if (groupedNodeList.Count == 0)
            {
                m_UngroupedNodes.Remove(nodeName);
            }
        }

        private void AddGroup(NsnDialogueGroup group)
        {
            string groupName = group.title.ToLower();

            if (!m_Groups.ContainsKey(groupName))
            {
                NsnDialogueGroupErrorData groupErrorData = new NsnDialogueGroupErrorData();
                groupErrorData.Groups.Add(group);

                m_Groups.Add(groupName, groupErrorData);
                return;
            }

            List<NsnDialogueGroup> groupsList = m_Groups[groupName].Groups;
            groupsList.Add(group);

            Color errorColor = m_Groups[groupName].ErrorData.Color;
            group.SetErrorStyle(errorColor);

            if (groupsList.Count == 2)
            {
                ++NameErrorAmount;
                groupsList[0].SetErrorStyle(errorColor);
            }
        }

        private void RemoveGroup(NsnDialogueGroup group)
        {
            string oldGroupName = group.PreTitle.ToLower();
            List<NsnDialogueGroup> groupsList = m_Groups[oldGroupName].Groups;
            groupsList.Remove(group);

            group.ResetStyle();
            if(groupsList.Count == 1)
            {
                --NameErrorAmount;
                groupsList[0].ResetStyle();
                return;
            }
            else if(groupsList.Count == 0)
            {
                m_Groups.Remove(oldGroupName);
            }
        }

        private void OnElementsDeleted()
        {
            deleteSelection = (operationName, askUser) => {
                Type groupType = typeof(NsnDialogueGroup);
                Type edgeType = typeof(Edge);

                List<NsnDialogueGroup> groupsToDelete = new List<NsnDialogueGroup>();
                List<NsnDialogueNode> nodesToDelete = new List<NsnDialogueNode>();
                List<Edge> edgesToDelete = new List<Edge>();

                foreach(GraphElement element in selection)
                {
                    if (element is NsnDialogueNode node)
                    {
                        nodesToDelete.Add(node);
                    }
                    else if (element.GetType() == edgeType)
                    {
                        Edge edge = (Edge)element;
                        edgesToDelete.Add(edge);
                    }
                    else if (element.GetType() == groupType)
                    {
                        NsnDialogueGroup group = (NsnDialogueGroup)element;
                        groupsToDelete.Add(group);
                    }
                }

                // delete group
                foreach (var group in groupsToDelete)
                {
                    List<NsnDialogueNode> groupNodes = new List<NsnDialogueNode>();
                    foreach(GraphElement element in group.containedElements)
                    {
                        if (!(element is NsnDialogueNode))
                            continue;
                        NsnDialogueNode node = (NsnDialogueNode)element;
                        groupNodes.Add(node);
                    }

                    group.RemoveElements(groupNodes);
                    RemoveGroup(group);
                    RemoveElement(group);
                }
                // delete edge
                DeleteElements(edgesToDelete);
                // delete node
                foreach (var nodeToDelete in nodesToDelete)
                {
                    if (nodeToDelete.Group != null)
                    {
                        nodeToDelete.Group.RemoveElement(nodeToDelete);
                    }

                    RemoveUngroupedNode(nodeToDelete);
                    nodeToDelete.DisconnectAllPorts();
                    RemoveElement(nodeToDelete);
                }
            };

        }

        private void OnGroupElementsAdded()
        {
            elementsAddedToGroup = (group, elements) =>
            {
                foreach (GraphElement element in elements)
                {
                    if (!(element is NsnDialogueNode))
                    {
                        continue;
                    }

                    NsnDialogueGroup dialogueGroup = (NsnDialogueGroup)group;
                    NsnDialogueNode node = (NsnDialogueNode)element;

                    RemoveUngroupedNode(node);
                    AddGroupedNode(node, dialogueGroup);
                }
            };
        }

        private void OnGroupElementsRemoved()
        {
            elementsRemovedFromGroup = (group, elements) =>
            {
                foreach (GraphElement element in elements)
                {
                    if (!(element is NsnDialogueNode))
                        continue;

                    NsnDialogueGroup dialogueGroup = (NsnDialogueGroup)group;
                    NsnDialogueNode node = (NsnDialogueNode)element;

                    RemoveGroupedNode(node, dialogueGroup);
                    AddUngroupedNode(node);
                }
            };
        }

        private void OnGroupRenamed()
        {
            groupTitleChanged = (group, newTitle) =>
            {
                NsnDialogueGroup dialogueGroup = (NsnDialogueGroup)group;
                dialogueGroup.title = newTitle;//newTitle.RemoveWhitespaces().RemoveSpecialCharacters();

                if (string.IsNullOrEmpty(dialogueGroup.title))
                {
                    if (!string.IsNullOrEmpty(dialogueGroup.PreTitle))
                        ++NameErrorAmount;
                }
                else
                {
                    if (string.IsNullOrEmpty(dialogueGroup.PreTitle))
                        --NameErrorAmount;
                }

                RemoveGroup(dialogueGroup);
                dialogueGroup.PreTitle = dialogueGroup.title;
                AddGroup(dialogueGroup);
            };
        }

        private void OnGraphViewChanged()
        {
            graphViewChanged = (changes) =>
            {
                if (changes.edgesToCreate != null)
                {
                    foreach (Edge edge in changes.edgesToCreate)
                    {
                        NsnDialogueNode nextNode = (NsnDialogueNode)edge.input.node;
                        NsnDialogueChoiceSaveData choiceData = (NsnDialogueChoiceSaveData)edge.output.userData;
                        choiceData.NodeID = nextNode.ID;
                    }
                }

                if (changes.elementsToRemove != null)
                {
                    Type edgeType = typeof(Edge);
                    foreach (GraphElement element in changes.elementsToRemove)
                    {
                        if (element.GetType() != edgeType)
                            continue;

                        Edge edge = (Edge)element;
                        NsnDialogueChoiceSaveData choiceData = (NsnDialogueChoiceSaveData)edge.output.userData;
                        choiceData.NodeID = "";
                    }
                }

                return changes;
            };
        }


        private IManipulator CreateNodeContextualMenu(string actionTitle, DialogueNodeType dialogueType)
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction(actionTitle,
                actionEvent => AddElement(CreateNode("DialogueName", dialogueType, GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))))
            );

            return contextualMenuManipulator;
        }

        private IManipulator CreateGroupContextualMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction("Add Group",
                actionEvent => CreateGroup("DialogueGroup", GetLocalMousePosition(actionEvent.eventInfo.localMousePosition)))
            );
            return contextualMenuManipulator;
        }

        public NsnDialogueNode CreateNode(string nodeName, DialogueNodeType dialogueType, Vector2 position, bool shouldDraw = true)
        {
            Type nodeType = Type.GetType($"Nsn.EditorToolKit.Dialogue{dialogueType}Node");

            NsnDialogueNode node = (NsnDialogueNode)Activator.CreateInstance(nodeType);
            node.OnInitialize(nodeName, this, position);

            if (shouldDraw)
                node.OnDraw();

            AddUngroupedNode(node);
            return node;
        }

        public NsnDialogueGroup CreateGroup(string title, Vector2 position)
        {
            NsnDialogueGroup group = new NsnDialogueGroup(title, position);
            AddGroup(group);

            AddElement(group);

            foreach (GraphElement selectedElement in selection)
            {
                if (!(selectedElement is NsnDialogueNode))
                {
                    continue;
                }

                NsnDialogueNode node = (NsnDialogueNode)selectedElement;

                group.AddElement(node);
            }

            return group;
        }

        public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false)
        {
            Vector2 worldMousePosition = mousePosition;

            if (isSearchWindow)
            {
                worldMousePosition = m_DialogueEditorWindow.rootVisualElement.ChangeCoordinatesTo(
                    m_DialogueEditorWindow.rootVisualElement.parent,
                    mousePosition - m_DialogueEditorWindow.position.position);
            }

            Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);
            return localMousePosition;
        }

        public void ClearGraph()
        {
            graphElements.ForEach(graphElement => RemoveElement(graphElement));

            m_Groups.Clear();
            m_GroupedNodes.Clear();
            m_UngroupedNodes.Clear();

            NameErrorAmount = 0;
        }

        public void ToggleMiniMap()
        {
            m_MiniMap.visible = !m_MiniMap.visible;
        }

    }
}

