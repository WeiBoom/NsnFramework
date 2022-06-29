using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

public class NodeGraphEditorView : GraphView
{
    public new class UxmlFactory : UxmlFactory<NodeGraphEditorView, GraphView.UxmlTraits>
    {
    }

    public NodeGraphEditorView()
    {
        Insert(0, new GridBackground());

        var styleSheet =
            AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/NsnFramework/Editor/Editor_NodeTree/NodeGraphEditor.uss");
        styleSheets.Add(styleSheet);
    }
}

