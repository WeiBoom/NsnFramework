using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // 保存Group数据
            foreach (NsnDialogueGroup group in data.groups)
            {
                var groupData = new NsnDialogueGroupSaveData()
                {
                    ID = group.ID,
                    Name = group.title,
                    Position = group.GetPosition().position,
                };
                graphData.Groups.Add(groupData);
            }
            // Group数据存储到SO文件中
            // todo
        }


        private static void SaveNodes(NsnDialogueGraphSaveDataSO graphData, NsnDialogueContainerSO dialogueContainer, NsnDialogueGraphSaveData data)
        {

        }
    }
}
