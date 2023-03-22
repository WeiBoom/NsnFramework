using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Graphs;

namespace Nsn.EditorToolKit
{
    public static class NsnDialogueUtility
    {

        public static void SaveDialogue(NsnDialogueGraphView graphView, string graphName)
        {
            NsnDialogueGraphSaveData data = new NsnDialogueGraphSaveData(graphView, graphName);

            // 创建默认文件夹
            CreateDefaultFolder(data);
            // 添加数据
            Type groupType = typeof(NsnDialogueGroup);
            graphView.graphElements.ForEach(graphElement=>
            {
                if (graphElement is NsnDialogueNode node)
                    data.nodes.Add(node);
                else if (graphElement.GetType() == groupType)
                    data.groups.Add((NsnDialogueGroup)graphElement);
            });

            NsnDialogueGraphSaveDataSO graphData = NEditorTools.CreateAsset<NsnDialogueGraphSaveDataSO>(NEditorConst.NsnToolKitDialogueAssetPath, $"{graphName}Graph");
            graphData.Initialize(graphName);

            NsnDialogueContainerSO dialogueContainer = NEditorTools.CreateAsset<NsnDialogueContainerSO>(data.containerFolderPath, graphName);
            dialogueContainer.Initialize(graphName);

            SaveGroups(graphData, dialogueContainer, data);
            SaveNodes(graphData, dialogueContainer, data);

            NEditorTools.SaveAsset(graphData);
            NEditorTools.SaveAsset(dialogueContainer);
        }

        private static void CreateDefaultFolder(NsnDialogueGraphSaveData data)
        {
            NEditorTools.CreateProjFolder("Assets/Editor/DialogueSystem", "Graphs");
            NEditorTools.CreateProjFolder("Assets", "DialogueSystem");
            NEditorTools.CreateProjFolder("Assets/DialogueSystem", "Dialogues");

            NEditorTools.CreateProjFolder("Assets/DialogueSystem/Dialogues", data.graphFileName);
            NEditorTools.CreateProjFolder(data.containerFolderPath, "Global");
            NEditorTools.CreateProjFolder(data.containerFolderPath, "Groups");
            NEditorTools.CreateProjFolder($"{data.containerFolderPath}/Global", "Dialogues");
        }

        private static void SaveGroups(NsnDialogueGraphSaveDataSO graphData, NsnDialogueContainerSO dialogueContainer, NsnDialogueGraphSaveData data)
        {
            List<string> groupNames = new List<string>();

            foreach (NsnDialogueGroup group in data.groups)
            {
                SaveGroupToGraph(group, graphData);
                SaveGraphToScriptableObject(group, dialogueContainer, data);
                groupNames.Add(group.title);
            }
            
            if(graphData.OldGroupNames != null && graphData.OldGroupNames.Count > 0)
            {
                var groupsToRemove = graphData.OldGroupNames.Except(groupNames).ToList();
                foreach (var group in groupsToRemove)
                {
                    NEditorTools.RemoveProjFolder($"{data.containerFolderPath}/Groups/{group}");
                }
            }
            graphData.OldGroupNames = new List<string>(groupNames);
        }

        private static void SaveGroupToGraph(NsnDialogueGroup group, NsnDialogueGraphSaveDataSO graphData)
        {
            string groupName = group.title;
            var groupData = new NsnDialogueGroupSaveData()
            {
                ID = group.ID,
                Name = groupName,
                Position = group.GetPosition().position,
            };
            graphData.Groups.Add(groupData);
        }

        private static void SaveGraphToScriptableObject(NsnDialogueGroup group, NsnDialogueContainerSO dialogueContainer, NsnDialogueGraphSaveData data)
        {
            string groupName = group.title;
            NEditorTools.CreateProjFolder($"{data.containerFolderPath}/Groups", groupName);
            NEditorTools.CreateProjFolder($"{data.containerFolderPath}/Groups/{groupName}", "Dialogues");
            var dialogueGroup = NEditorTools.CreateAsset<NsnDialogueGroupSO>($"{data.containerFolderPath}/Groups/{groupName}", groupName);
            dialogueGroup.Initialize(groupName);

            data.createdDialogueGroups.Add(group.ID, dialogueGroup);
            dialogueContainer.DialogueGroups.Add(dialogueGroup, new List<NsnDialogueNodeSO>());

            NEditorTools.SaveAsset(dialogueGroup);
        }

        private static void SaveNodes(NsnDialogueGraphSaveDataSO graphData, NsnDialogueContainerSO dialogueContainer, NsnDialogueGraphSaveData data)
        {
            var groupedNodeNames = new SerializableDictionary<string, List<string>>();
            var ungroupedNodeNames = new List<string>();

            foreach(var node in data.nodes)
            {
                SaveNodeToGraph(node, graphData, data);
                SaveNodeToScriptableObject(node, dialogueContainer, data);
                if (node.Group != null)
                {
                    groupedNodeNames.TryGetValue(node.Group.ID, out var group);
                    if(group != null)
                        group.Add(node.DialogueName);
                    else
                        groupedNodeNames.Add(node.Group.ID,new List<string> { node.DialogueName });
                    continue;
                }

                ungroupedNodeNames.Add(node.DialogueName);
            }

            UpdateDialoguesChoicesConnections(data);

            UpdateOldGroupedNodes(groupedNodeNames, graphData,data);
            UpdateOldUngroupedNodes(ungroupedNodeNames, graphData,data);

        }

        private static void SaveNodeToGraph(NsnDialogueNode node, NsnDialogueGraphSaveDataSO graphData, NsnDialogueGraphSaveData data)
        {
            List<NsnDialogueChoiceSaveData> choices = CloneNodeChoices(node.Choices);
            var nodeData = new NsnDialogueNodeSaveData()
            {
                ID = node.ID,
                Name = node.DialogueName,
                Choices = choices,
                Text = node.TextContent,
                GroupID = node.Group?.ID,
                DialogueType = node.DialogueNodeType,
                Position = node.GetPosition().position
            };

            graphData.Nodes.Add(nodeData);
        }

        private static void SaveNodeToScriptableObject(NsnDialogueNode node ,NsnDialogueContainerSO dialogueContainer, NsnDialogueGraphSaveData data)
        {

        }

        private static void UpdateDialoguesChoicesConnections(NsnDialogueGraphSaveData data)
        {
            foreach (var node in data.nodes)
            {
                var dialogue = data.createdDialogueNodes[node.ID];

                for (int choiceIndex = 0; choiceIndex < node.Choices.Count; ++choiceIndex)
                {
                    var nodeChoice = node.Choices[choiceIndex];
                    if (string.IsNullOrEmpty(nodeChoice.NodeID))
                        continue;

                   dialogue.Choices[choiceIndex].NextDialogue = data.createdDialogueNodes[nodeChoice.NodeID];

                   NEditorTools.SaveAsset(dialogue);
                }
            }
        }

        private static void UpdateOldGroupedNodes(SerializableDictionary<string, List<string>> currentGroupedNodeNames, NsnDialogueGraphSaveDataSO graphData, NsnDialogueGraphSaveData data)
        {
            if (graphData.OldGroupedNodeNames != null && graphData.OldGroupedNodeNames.Count != 0)
            {
                foreach (KeyValuePair<string, List<string>> oldGroupedNode in graphData.OldGroupedNodeNames)
                {
                    List<string> nodesToRemove = new List<string>();

                    if (currentGroupedNodeNames.ContainsKey(oldGroupedNode.Key))
                    {
                        nodesToRemove = oldGroupedNode.Value.Except(currentGroupedNodeNames[oldGroupedNode.Key]).ToList();
                    }

                    foreach (string nodeToRemove in nodesToRemove)
                    {
                        AssetDatabase.DeleteAsset($"{data.containerFolderPath}/Groups/{oldGroupedNode.Key}/Dialogues/{nodeToRemove}");
                    }
                }
            }

            graphData.OldGroupedNodeNames = new SerializableDictionary<string, List<string>>(currentGroupedNodeNames);
        }

        private static void UpdateOldUngroupedNodes(List<string> currentUngroupedNodeNames, NsnDialogueGraphSaveDataSO graphData, NsnDialogueGraphSaveData data)
        {
            if (graphData.OldUngroupedNodeNames != null && graphData.OldUngroupedNodeNames.Count != 0)
            {
                List<string> nodesToRemove = graphData.OldUngroupedNodeNames.Except(currentUngroupedNodeNames).ToList();
                foreach (string nodeToRemove in nodesToRemove)
                {
                    AssetDatabase.DeleteAsset($"{data.containerFolderPath}/Global/Dialogues/{nodeToRemove}");
                }
            }

            graphData.OldUngroupedNodeNames = new List<string>(currentUngroupedNodeNames);
        }



        private static List<NsnDialogueChoiceSaveData> CloneNodeChoices(List<NsnDialogueChoiceSaveData> nodeChoices)
        {
            var choices = new List<NsnDialogueChoiceSaveData>();

            foreach (var choice in nodeChoices)
            {
                var choiceData = new NsnDialogueChoiceSaveData()
                {
                    Content = choice.Content,
                    NodeID = choice.NodeID
                };

                choices.Add(choiceData);
            }

            return choices;
        }
    }
}
