using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;

namespace NeverSayNever.NodeGraphView
{
    public class DialogueGraph : EditorWindow
    {
        private DialogueGraphView _graphView;

        private string _fileName = "New Narrative";


        private static string _selectedGraphName = "";

        [MenuItem("NeverSayNever/GraphView/Open DialogueGraph")]
        public static void OpenDialogueGraphWindow()
        {
            var window = GetWindow<DialogueGraph>();
            window.titleContent = new GUIContent("Dialogue Graph");
        }

        private void CreateGUI()
        {
        }

        private void OnEnable()
        {
            ConstructDialogueGraphView();
            GenerateToolBar();
            GenerateMiniMap();

            OnNodeSelectionChanged();
        }

        private void OnDisable()
        {
            if(_graphView != null)
                rootVisualElement.Remove(_graphView);
        }

        private void OnNodeSelectionChanged()
        {
            DialogueContainer dialogue = Selection.activeObject as DialogueContainer;
            if (dialogue != null && AssetDatabase.CanOpenAssetInEditor(dialogue.GetInstanceID()))
            {
                string graphViewName = dialogue.name;
                var saveUtility = DialogueGraphUtility.GetInstance(_graphView);
                saveUtility.LoadGraph(graphViewName);
            }
        }

        private void ConstructDialogueGraphView()
        {
            _graphView = new DialogueGraphView
            {
                name = "Dialogue Graph View",
            };

            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }

        private void GenerateToolBar()
        {
            var toolbar = new UnityEditor.UIElements.Toolbar();

            var fileNameTextField = new TextField("File Name:");
            fileNameTextField.SetValueWithoutNotify(_fileName);
            fileNameTextField.MarkDirtyRepaint();
            fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);

            toolbar.Add(fileNameTextField);

            toolbar.Add(new Button(() => _graphView.CreateNewDialogueNode("New Node" , Vector2.zero)) { text = "Create Node" });
            toolbar.Add(new Button(() => RequestDataOperation(true)) { text = "Save Data" });

            toolbar.Add(new Button(() => RequestDataOperation(false)) { text = "Load Data" });
            // toolbar.Add(new Button(() => _graphView.CreateNewDialogueNode("Dialogue Node")) {text = "New Node",});
            rootVisualElement.Add(toolbar);
        }

        private void GenerateMiniMap()
        {
            var miniMap = new UnityEditor.Experimental.GraphView.MiniMap { anchored = true };
            //var cords = _graphView.contentViewContainer.WorldToLocal(new Vector2(this.maxSize.x - 10, 30));
            //miniMap.SetPosition(new Rect(cords.x, cords.y, 200, 140));
            miniMap.SetPosition(new Rect(10,30, 200, 140));
            _graphView.Add(miniMap);
        }

        // todo
        private void RequestDataOperation(bool save)
        {
            if (!string.IsNullOrEmpty(_fileName))
            {
                var saveUtility = DialogueGraphUtility.GetInstance(_graphView);
                if (save)
                    saveUtility.SaveGraph(_fileName);
                else
                    saveUtility.LoadGraph(_fileName);
            }
            else
            {
                EditorUtility.DisplayDialog("Invalid File name", "Please Enter a valid filename", "OK");
            }
        }
    }
}

