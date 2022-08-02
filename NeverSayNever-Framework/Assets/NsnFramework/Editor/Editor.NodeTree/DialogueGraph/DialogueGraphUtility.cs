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

        #region SAVE

        public void SaveGraph(string fileName)
        {

        }

        private bool SaveNodes(string fileName, DialogueContainer dialogueContainerObject)
        {
            return false;
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
