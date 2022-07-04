using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

using NeverSayNever.BehaviourTree;

public class NodeGraphEditorView : GraphView
{
    public new class UxmlFactory : UxmlFactory<NodeGraphEditorView, GraphView.UxmlTraits>{}

    public System.Action<TreeNodeView> OnNodeSelected;
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

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;


        if(tree.rootNode == null)
        {
            tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();
        }

        // create node view
        tree.nodes.ForEach(n => CreateNodeView(n));

        // create edges
        tree.nodes.ForEach(n =>
        {
            var children = tree.GetChildren(n);
            children.ForEach(c =>
            {
                TreeNodeView parentView = FindNodeView(n);
                TreeNodeView childView = FindNodeView(c);

                Edge edge = parentView.output.ConnectTo(childView.input);
                AddElement(edge);
            });
        });

    }

    TreeNodeView FindNodeView(TreeNode node)
    {
        return GetNodeByGuid(node.guid) as TreeNodeView;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
        AppendActionByType<ActionNode>(evt);
        AppendActionByType<DecoratorNode>(evt);
        AppendActionByType<CompositeNode>(evt);
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
        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }


    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if(graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                TreeNodeView nodeView = elem as TreeNodeView;
                if(nodeView != null)
                {
                    tree.DeleteNode(nodeView.node);
                }

                Edge edge = elem as Edge;
                if(edge != null)
                {
                    TreeNodeView parentView = edge.output.node as TreeNodeView;
                    TreeNodeView childView = edge.input.node as TreeNodeView;

                    tree.RemoveChild(parentView.node, childView.node);
                }
            });
        }

        if(graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                TreeNodeView parentView = edge.output.node as TreeNodeView;
                TreeNodeView childView = edge.input.node as TreeNodeView;

                tree.AddChild(parentView.node, childView.node);
            });
        }

        return graphViewChange;
    }
}

