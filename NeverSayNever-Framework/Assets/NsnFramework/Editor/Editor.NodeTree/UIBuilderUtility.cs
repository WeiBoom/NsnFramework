using NeverSayNever.NodeGraphView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Callbacks;
using UnityEditor;

namespace Assets.NsnFramework.Editor.Editor.NodeTree
{
    internal class UIBuilderUtility
    {

        [OnOpenAsset]
        public static bool OnOpenNodeGraphAsset(int instanceId, int line)
        {
            // open behaviour node editor
            if (Selection.activeObject is NodeGraphTree)
            {
                NodeGraphEditor.OpenWindow();
                return true;
            }
            // open dialogue node editor
            if (Selection.activeObject is DialogueContainer)
            {
                DialogueGraph.OpenDialogueGraphWindow();
                return true;
            }
            return false;
        }

    }
}
