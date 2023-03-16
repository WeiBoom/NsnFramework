using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsn.EditorToolKit
{
    public static class NsnDialogueUtility
    {

        public static void SaveDialogueGraphView(NsnDialogueGraphView graphView, string graphName)
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
    }
}
