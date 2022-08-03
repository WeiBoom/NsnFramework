using System.Collections.Generic;
using System.Linq;

namespace NeverSayNever.NodeGraphView
{
    using Group = UnityEditor.Experimental.GraphView.Group;

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


        private string DialogueObjectSavePath = "Assets/Ressources";

        #region SAVE

        public void SaveGraph(string fileName)
        {
            var dialogueContainer = UnityEngine.ScriptableObject.CreateInstance<DialogueContainer>();
            bool saveNodeResult = SaveNodes(fileName,dialogueContainer);
            if (!saveNodeResult)
                return;

            SaveExposedProperties(dialogueContainer);
            SaveCommentBlocks(dialogueContainer);
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
        #endregion

        #region LOAD

        public void LoadGraph(string fileName)
        {

        }

        #endregion

    }
}
