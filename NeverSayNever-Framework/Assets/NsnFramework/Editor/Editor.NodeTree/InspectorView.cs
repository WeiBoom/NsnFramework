using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;

using NeverSayNever.EditorUtilitiy;

public class InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits>{ }

    public InspectorView()
    {
    }

    Editor editor;

    internal void UpdateSelection(TreeNodeView nodeView)
    {
        Clear();
        UnityEngine.Object.DestroyImmediate(editor);

        editor = Editor.CreateEditor(nodeView.node);
        IMGUIContainer container = new IMGUIContainer(() =>
        {
            editor.OnInspectorGUI();
        }) ;
        Add(container);

    }
}
