using System.Collections.Generic;
using UnityEngine;

namespace Nsn.EditorToolKit
{
    using Random = UnityEngine.Random;

    public class NsnDialogueErrorData
    {
        public Color Color { get; set; }

        public NsnDialogueErrorData()
        {
            Color = new Color32(
                (byte)Random.Range(65, 256),
                (byte)Random.Range(50, 176),
                (byte)Random.Range(50, 176),
                255
            );
        }
    }

    public class NsnDialogueNodeErrorData
    {
        public NsnDialogueErrorData ErrorData { get; set; }

        public List<NsnDialogueNode> Nodes { get; set; }

        public NsnDialogueNodeErrorData()
        {
            ErrorData = new NsnDialogueErrorData();
            Nodes = new List<NsnDialogueNode>();
        }
    }

    public class NsnDialogueGroupErrorData
    {
        public NsnDialogueErrorData ErrorData { get; set; }

        public List<NsnDialogueGroup> Groups;

        public NsnDialogueGroupErrorData()
        {
            ErrorData = new NsnDialogueErrorData();
            Groups = new List<NsnDialogueGroup>();
        }
    }

}
