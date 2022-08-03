using System;
using System.Collections.Generic;

namespace NeverSayNever.NodeGraphView
{
    public class DialogueContainer : UnityEngine.ScriptableObject
    {
        public List<DialogueNodeLinkData> NodeLinks = new List<DialogueNodeLinkData>();
        public List<DialogueNodeData> DialogueNodeData = new List<DialogueNodeData>();
        public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();
        public List<DialogueCommentBlockData> CommentBlockData = new List<DialogueCommentBlockData>();
    }
}
