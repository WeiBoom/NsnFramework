using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nsn.EditorToolKit
{
    public class NsnDialogueChoiceSaveData
    {
        [field: SerializeField] public string Content { get; set; }
        [field: SerializeField] public string NodeID { get; set; }
    }

    [Serializable]
    public class NsnDialogueGroupSaveData
    {
        [field: SerializeField] public string ID { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public Vector2 Position { get; set; }
    }

    [Serializable]
    public class NsnDialogueNodeSaveData
    {
        [field: SerializeField] public string ID { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string Text { get; set; }
        [field: SerializeField] public string GroupID { get; set; }
        [field: SerializeField] public DialogueNodeType DialogueType { get; set; }
        [field: SerializeField] public Vector2 Position { get; set; }
        [field: SerializeField] public List<NsnDialogueChoiceSaveData> Choices { get; set; }
    }

    public class DSGraphSaveDataSO : ScriptableObject
    {
        [field: SerializeField] public string FileName { get; set; }
        [field: SerializeField] public List<NsnDialogueGroupSaveData> Groups { get; set; }
        [field: SerializeField] public List<NsnDialogueNodeSaveData> Nodes { get; set; }
        [field: SerializeField] public List<string> OldGroupNames { get; set; }
        [field: SerializeField] public List<string> OldUngroupedNodeNames { get; set; }
        [field: SerializeField] public SerializableDictionary<string, List<string>> OldGroupedNodeNames { get; set; }

        public void Initialize(string fileName)
        {
            FileName = fileName;

            Groups = new List<NsnDialogueGroupSaveData>();
            Nodes = new List<NsnDialogueNodeSaveData>();
        }
    }


}

