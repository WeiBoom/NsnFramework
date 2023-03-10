using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nsn.EditorToolKit
{
    public class DialogueMultipleChoiceNode : NsnDialogueNode
    {

        public override void OnInitialize(string nodeName, NsnDialogueGraphView graphView, Vector2 position)
        {
            base.OnInitialize(nodeName, graphView, position);
            DialogueNodeType = DialogueNodeType.MultipleChoice;

            NsnDialogueChoiceSaveData choiceData =  new NsnDialogueChoiceSaveData() { Content = "New Choice"};

            Choices.Add(choiceData);
        }

        public override void OnDraw()
        {
            base.OnDraw();

            // MAIN BUTTON && MAIN CONTAINER
            Button addChoiceButton = new Button(() =>
            {
                var data = new NsnDialogueChoiceSaveData() { Content = "New Choice" };
                Choices.Add(data);
                var port = CreateChoicePort(data);
                outputContainer.Add(port);
            }) { text = "Add Choice" };

            addChoiceButton.AddToClassList("ds-node__button");
            mainContainer.Add(addChoiceButton);

            // OUPUT CONTAINER
            foreach(var choice in Choices)
            {
                Port port = CreateChoicePort(choice);
                outputContainer.Add(port);
            }

            RefreshExpandedState();
        }

        private Port CreateChoicePort(object userData)
        {
            Port choicePort = CreatePort();
            choicePort.userData = userData;

            var choiceData = (NsnDialogueChoiceSaveData)userData;

            // DELETE BUTTON
            Button deleteChoiceButton = new Button(() =>
            {
                if (Choices.Count == 1)
                    return;

                if (choicePort.connected)
                    m_DialogueGraphView.DeleteElements(choicePort.connections);
                Choices.Remove(choiceData);

                m_DialogueGraphView.RemoveElement(choicePort);
            }){ text = "X" };

            deleteChoiceButton.AddToClassList("ds-node__button");

            TextField choiceTextField = new TextField() { value = choiceData.Content };
            choiceTextField.RegisterValueChangedCallback(ce => choiceData.Content = ce.newValue);
            choiceTextField.AddToClassList("ds-node__text-field");
            choiceTextField.AddToClassList("ds-node__text-field__hidden");
            choiceTextField.AddToClassList("ds-node__choice-text-field");

            choicePort.Add(choiceTextField);
            choicePort.Add(deleteChoiceButton);
            return choicePort;
        }
    }
}
