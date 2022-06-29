using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

using NeverSayNever.EditorUtilitiy;

public class NodeGraphEditorView : GraphView
{
    public new class UxmlFactory : UxmlFactory<NodeGraphEditorView, GraphView.UxmlTraits>{}

    NodeGraphTree tree;

    public NodeGraphEditorView()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet =
            AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/NsnFramework/Editor/Editor_NodeTree/NodeGraphEditor.uss");
        styleSheets.Add(styleSheet);
    }
    
    internal void PolulateView(NodeGraphTree tree)
    {
        this.tree = tree;
        DeleteElements(graphElements);
        tree.nodes.ForEach(n => CreateNodeView(n));
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
        AppendActionByType<ActionNode>(evt);
        AppendActionByType<DecoratorNode>(evt);
    }

    private void AppendActionByType<T>(ContextualMenuPopulateEvent evt) where T: TreeNode
    {
        var types = TypeCache.GetTypesDerivedFrom<T>();
        foreach (var type in types)
        {
            evt.menu.AppendAction($"[{type.BaseType.Name}]{type.Name}", a => CreateNode(type));
        }
    }

    void CreateNode(System.Type type)
    {
        TreeNode node = tree.CreateNode(type);
        CreateNodeView(node);
    }

    void CreateNodeView(TreeNode node)
    {
        TreeNodeView nodeView = new TreeNodeView(node);
        AddElement(nodeView);
    }
}

