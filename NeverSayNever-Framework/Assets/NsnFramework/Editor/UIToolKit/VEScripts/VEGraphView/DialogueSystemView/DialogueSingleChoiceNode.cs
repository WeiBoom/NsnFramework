using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Nsn.EditorToolKit
{
    public class DialogueSingleChoiceNode : NsnDialogueNode
    {

        public override void OnInitialize(string nodeName, NsnDialogueGraphView graphView, Vector2 position)
        {
            base.OnInitialize(nodeName, graphView, position);

            DialogueNodeType = DialogueNodeType.SingleChoice;
            NsnDialogueChoiceSaveData choiceData = new NsnDialogueChoiceSaveData() { Content = "Next Dialogue" };

            Choices.Add(choiceData);
        }

        public override void OnDraw()
        {
            base.OnDraw();
            foreach(var choice in Choices)
            {
                Port port = CreatePort(choice.Content);
                port.portName = "Output";
                port.userData = choice;
                outputContainer.Add(port);
            }
            RefreshExpandedState();
        }
    }
}
