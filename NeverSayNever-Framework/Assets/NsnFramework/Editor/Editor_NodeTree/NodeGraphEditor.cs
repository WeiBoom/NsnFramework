using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using NeverSayNever.EditorUtilitiy;
using NeverSayNever.Utilitiy;

public class NodeGraphEditor : EditorWindow
{
    NodeGraphEditorView treeView;
    InspectorView inspectorView;

    [MenuItem("NeverSayNever/UI Toolkit/NodeGraphEditor")]
    public static void OpenWindow()
    {
        NodeGraphEditor wnd = GetWindow<NodeGraphEditor>();
        wnd.titleContent = new GUIContent("NodeGraphEditor");
    }
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/NsnFramework/Editor/Editor_NodeTree/NodeGraphEditor.uxml");
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/NsnFramework/Editor/Editor_NodeTree/NodeGraphEditor.uss");
        root.styleSheets.Add(styleSheet);

        treeView = root.Q<NodeGraphEditorView>();
        inspectorView = root.Q<InspectorView>();


        OnSelectionChange();
    }

    private void OnSelectionChange()
    {
        ULog.Print("[NodeGraphEditor] {0}", "OnSelectionChange");
        NodeGraphTree tree = Selection.activeObject as NodeGraphTree;
        if(tree != null)
        {
            treeView.PolulateView(tree);
        }
    }
}