using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace Nsn.EditorToolKit
{
    public class NsnFlowEditorWindow : NsnBaseEditorWindow
    {
        private static NsnFlowEditorWindow m_Window;

        [MenuItem("Nsn/ToolKit/FlowGraph &#F")]
        private static void ShowWindow()
        {
            Display(ref m_Window, "Nsn-FlowGraph(Alt + Shift + F)");

            var ip = NEditorTools.GetLocalIPAddress();
            Debug.LogError(ip);
        }
    }
}

