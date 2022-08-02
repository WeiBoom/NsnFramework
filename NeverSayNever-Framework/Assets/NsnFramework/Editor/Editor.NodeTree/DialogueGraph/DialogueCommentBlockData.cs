namespace NeverSayNever.NodeGraphView
{
    using System.Collections.Generic;

    [System.Serializable]
    public class DialogueCommentBlockData
    {
        public List<string> ChildNodes = new List<string>();
        public UnityEngine.Vector2 Position;
        public string Title = "Comment Block";
    }
}
