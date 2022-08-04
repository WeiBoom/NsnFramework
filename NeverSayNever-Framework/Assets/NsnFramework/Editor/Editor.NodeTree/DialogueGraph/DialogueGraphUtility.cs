using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace NeverSayNever.NodeGraphView
{
    using Group = UnityEditor.Experimental.GraphView.Group;
    using Edge = UnityEditor.Experimental.GraphView.Edge;
    using Port = UnityEditor.Experimental.GraphView.Port;

    public class DialogueGraphUtility
    {
        private DialogueGraphView _graphView;
        private DialogueContainer _dialogueContainer;

        private List<UnityEditor.Experimental.GraphView.Edge> Edges => _graphView.edges.ToList();
        private List<DialogueNode> Nodes => _graphView.nodes.ToList().Cast<DialogueNode>().ToList();

        private List<Group> CommentBlocks => _graphView.graphElements.ToList().Where(x => x is Group).Cast<Group>().ToList();

        public static DialogueGraphUtility GetInstance(DialogueGraphView graphView)
        {
            return new DialogueGraphUtility
            {
                _graphView = graphView
            };
        }

        #region SAVE

        private string _dialogueObjectSaveRootPath = "Assets/Resources";

        private string _dialogueObjectSaveFolder = "DialogueAssetObjects";



        public void SaveGraph(string fileName)
        {
            var dialogueContainer = UnityEngine.ScriptableObject.CreateInstance<DialogueContainer>();
            // save data
            bool saveNodeResult = SaveNodes(fileName, dialogueContainer);
            if (!saveNodeResult)
                return;
            SaveExposedProperties(dialogueContainer);
            SaveCommentBlocks(dialogueContainer);

            // save as scriptableObject;
            SaveDialogueContainerObject(fileName, dialogueContainer);
        }

        private bool SaveNodes(string fileName, DialogueContainer dialogueContainerObject)
        {
            if (!Edges.Any()) return false;

            // 先找连接的点的信息
            var connectedSockets = Edges.Where(x => x.input.node != null).ToArray();
            for (int i = 0; i < connectedSockets.Length; i++)
            {
                var outputNode = connectedSockets[i].output.node as DialogueNode;
                var inputNode = connectedSockets[i].input.node as DialogueNode;

                dialogueContainerObject.NodeLinks.Add(new DialogueNodeLinkData
                {
                    BaseNodeGUID = outputNode.GUID,
                    PortName = connectedSockets[i].output.portName,
                    TargetNodeGUID = inputNode.GUID
                });
            }
            // 再遍历每个入口的点的信息
            foreach (var node in Nodes.Where(node => !node.EntryPoint))
            {
                dialogueContainerObject.DialogueNodeData.Add(new DialogueNodeData
                {
                    NodeGUID = node.GUID,
                    DialogueText = node.DialogueText,
                    Position = node.GetPosition().position
                });
            }

            return true;
        }

        private void SaveExposedProperties(DialogueContainer dialogueContainer)
        {
        }

        private void SaveCommentBlocks(DialogueContainer dialogueContainer)
        {
        }

        private void SaveDialogueContainerObject(string fileName, DialogueContainer dialogueContainer)
        {
            string savePath = $"{_dialogueObjectSaveRootPath}/{_dialogueObjectSaveFolder}";
            if (!UnityEditor.AssetDatabase.IsValidFolder(savePath))
            {
                var folderGUID = UnityEditor.AssetDatabase.CreateFolder(_dialogueObjectSaveRootPath, _dialogueObjectSaveFolder);
                UnityEngine.Debug.Log("Create Folder .. GUID :" + folderGUID);
            }

            UnityEngine.Object loadedAsset = UnityEditor.AssetDatabase.LoadAssetAtPath($"{savePath}/{fileName}.asset", typeof(DialogueContainer));

            if (loadedAsset == null || !UnityEditor.AssetDatabase.Contains(loadedAsset))
            {
                UnityEditor.AssetDatabase.CreateAsset(dialogueContainer, $"{savePath}/{fileName}.asset");
            }
            else
            {
                DialogueContainer container = loadedAsset as DialogueContainer;
                container.NodeLinks = dialogueContainer.NodeLinks;
                container.DialogueNodeData = dialogueContainer.DialogueNodeData;
                container.ExposedProperties = dialogueContainer.ExposedProperties;
                container.CommentBlockData = dialogueContainer.CommentBlockData;
                UnityEditor.EditorUtility.SetDirty(container);
            }
            UnityEditor.AssetDatabase.SaveAssets();
        }
        #endregion

        #region LOAD

        public void LoadGraph(string fileName)
        {
            _dialogueContainer = UnityEngine.Resources.Load<DialogueContainer>(fileName);
            if(_dialogueContainer == null)
            {
                UnityEditor.EditorUtility.DisplayDialog("File Not Found", "指定文件没找到", "了解");
                return;
            }

            ClearGraph();
            GenerateDialogueNodes();
            ConnectDialogueNodes();
            AddExposedProperties();
            GenerateCommentBlocks();
        }

        private void ClearGraph()
        {
            Nodes.Find(x => x.EntryPoint).GUID = _dialogueContainer.NodeLinks[0].BaseNodeGUID;
            foreach (var perNode in Nodes)
            {
                if (perNode.EntryPoint) continue;
                Edges.Where(x => x.input.node == perNode).ToList()
                    .ForEach(edge => _graphView.RemoveElement(edge));
                _graphView.RemoveElement(perNode);
            }
        }

        private void GenerateDialogueNodes()
        {
            foreach (var perNode in _dialogueContainer.DialogueNodeData)
            {
                var tempNode = _graphView.CreateDialogueNode(perNode.DialogueText, UnityEngine.Vector2.zero);
                tempNode.GUID = perNode.NodeGUID;
                _graphView.AddElement(tempNode);

                var nodePorts = _dialogueContainer.NodeLinks.Where(x => x.BaseNodeGUID == perNode.NodeGUID).ToList();
                nodePorts.ForEach(x => _graphView.AddChoicePort(tempNode, x.PortName));
            }
        }

        private void ConnectDialogueNodes()
        {
            for (var i = 0; i < Nodes.Count; i++)
            {
                var k = i; //Prevent access to modified closure
                var connections = _dialogueContainer.NodeLinks.Where(x => x.BaseNodeGUID == Nodes[k].GUID).ToList();
                for (var j = 0; j < connections.Count(); j++)
                {
                    var targetNodeGUID = connections[j].TargetNodeGUID;
                    var targetNode = Nodes.First(x => x.GUID == targetNodeGUID);
                    LinkNodesTogether(Nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);

                    targetNode.SetPosition(new UnityEngine.Rect(
                        _dialogueContainer.DialogueNodeData.First(x => x.NodeGUID == targetNodeGUID).Position,
                        _graphView.DefaultNodeSize));
                }
            }
        }

        private void AddExposedProperties()
        {
            _graphView.ClearBlackBoardAndExposedProperties();
            foreach (var exposedProperty in _dialogueContainer.ExposedProperties)
            {
                _graphView.AddPropertyToBlackBoard(exposedProperty);
            }
        }

        private void GenerateCommentBlocks()
        {
            foreach (var commentBlock in CommentBlocks)
            {
                _graphView.RemoveElement(commentBlock);
            }

            foreach (var commentBlockData in _dialogueContainer.CommentBlockData)
            {
                var block = _graphView.CreateCommentBlock(new UnityEngine.Rect(commentBlockData.Position, _graphView.DefaultCommentBlockSize),
                     commentBlockData);
                block.AddElements(Nodes.Where(x => commentBlockData.ChildNodes.Contains(x.GUID)));
            }
        }

        private void LinkNodesTogether(Port outputSocket, Port inputSocket)
        {
            var tempEdge = new Edge()
            {
                output = outputSocket,
                input = inputSocket
            };
            tempEdge?.input.Connect(tempEdge);
            tempEdge?.output.Connect(tempEdge);
            _graphView.Add(tempEdge);
        }


        #endregion

    }
}
