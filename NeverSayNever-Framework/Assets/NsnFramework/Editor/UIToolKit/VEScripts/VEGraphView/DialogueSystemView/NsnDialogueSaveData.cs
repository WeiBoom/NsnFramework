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


    public class NsnDialogueGraphSaveData
    {
        public NsnDialogueGraphView graphView;

        public string graphFileName;
        public string containerFolderPath;

        public List<NsnDialogueNode> nodes;
        public List<NsnDialogueGroup> groups;

        public Dictionary<string, NsnDialogueGroupSO> createdDialogueGroups;
        public Dictionary<string, NsnDialogueNodeSO> createdDialogueNodes;

        public Dictionary<string, NsnDialogueGroup> loadedGroups;
        public Dictionary<string, NsnDialogueNode> loadedNodes;

        public NsnDialogueGraphSaveData(NsnDialogueGraphView dialogueGraph,string graphName)
        {
            graphView = dialogueGraph;
            graphFileName = graphName;

            containerFolderPath = $"{NEditorConst.NsnToolKitConfigPath}/{graphName}";

            nodes = new List<NsnDialogueNode>();
            groups = new List<NsnDialogueGroup>();

            createdDialogueGroups = new Dictionary<string, NsnDialogueGroupSO>();
            createdDialogueNodes = new Dictionary<string, NsnDialogueNodeSO>();

            loadedGroups = new Dictionary<string, NsnDialogueGroup>();
            loadedNodes = new Dictionary<string, NsnDialogueNode>();
        }
    }

    public class NsnDialogueNodeSO : ScriptableObject
    {
        [field: SerializeField] public string DialogueName { get; set; }
        [field: SerializeField][field: TextArea()] public string Text { get; set; }
        [field: SerializeField] public List<NsnDialogueChoiceSaveData> Choices { get; set; }
        [field: SerializeField] public DialogueNodeType DialogueType{ get; set; }
        [field: SerializeField] public bool IsStartingDialogue { get; set; }

        public void Initialize(string dialogueName, string text, List<NsnDialogueChoiceSaveData> choices, DialogueNodeType dialogueType, bool isStartingDialogue)
        {
            DialogueName = dialogueName;
            Text = text;
            Choices = choices;
            DialogueType = dialogueType;
            IsStartingDialogue = isStartingDialogue;
        }
    }

    public class NsnDialogueGroupSO : ScriptableObject
    {
        [field: SerializeField] public string GroupName { get; set; }

        public void Initialize(string groupName)
        {
            GroupName = groupName;
        }
    }

    public class NsnDialogueGraphSaveDataSO : ScriptableObject
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

