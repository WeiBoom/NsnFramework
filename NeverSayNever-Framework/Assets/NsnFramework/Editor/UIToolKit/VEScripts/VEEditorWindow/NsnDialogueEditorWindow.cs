using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nsn.EditorToolKit
{
    public class NsnDialogueEditorWindow : NsnBaseEditorWindow
    {
        private static NsnDialogueEditorWindow m_DialogueWindow;

        [MenuItem("Nsn/ToolKit/Dialogue &#D")]
        public static void ShowWindow()
        {
            Display(ref m_DialogueWindow ," Dialogue System");
        }

        private readonly string m_DefaultFileName = "DialogueFileName";

        private NsnDialogueGraphView m_DialogueGraphView;
        private static TextField m_FileNameTextField;
        private Button m_SaveBtn;
        private Button m_MiniMapBtn;


        protected override void OnCreateGUI()
        {
            AddDialogueGraphView();
            AddToolBar();

            var styleSheet = VEToolKit.LoadVEAssetStyleSheet("NsnDialogueVariables");
            m_Root.styleSheets.Add(styleSheet);
        }

        private void AddDialogueGraphView()
        {
            m_DialogueGraphView = new NsnDialogueGraphView(this);
            m_DialogueGraphView.StretchToParentSize();
            m_Root.Add(m_DialogueGraphView);
        }

        private void AddToolBar()
        {
            Toolbar toolbar = new Toolbar();
            m_FileNameTextField = new TextField() { value = m_DefaultFileName, label = "File Name:" };
            m_FileNameTextField.RegisterValueChangedCallback(ve => {
                m_FileNameTextField.value = ve.newValue;
            });

            m_SaveBtn = new Button() { text = "Save" };
            m_SaveBtn.RegisterValueChangedCallback(ce => { Save(); });

            Button loadBtn = new Button() { text = "Load" };
            loadBtn.RegisterValueChangedCallback(ce => { Load(); });

            Button clearBtn = new Button() { text = "Clear" };
            loadBtn.RegisterValueChangedCallback(ce => { Clear(); });

            Button resetBtn = new Button() { text = "Reset" };
            loadBtn.RegisterValueChangedCallback(ce => { ResetGraph(); });

            m_MiniMapBtn = new Button() { text = "MiniMap" };
            m_MiniMapBtn.RegisterValueChangedCallback(ce => { ToggleMiniMap(); });

            toolbar.Add(m_FileNameTextField);
            toolbar.Add(m_SaveBtn);
            toolbar.Add(loadBtn);
            toolbar.Add(clearBtn);
            toolbar.Add(resetBtn);
            toolbar.Add(m_MiniMapBtn);

            var styleSheet = VEToolKit.LoadVEAssetStyleSheet("NsnDialogueToolbarStyles");
            toolbar.styleSheets.Add(styleSheet);

            m_Root.Add(toolbar);
        }

        public void EnableSaving(bool enable)
        {
            m_SaveBtn.SetEnabled(enable);
        }

        private void Save()
        {
            if(string.IsNullOrEmpty(m_FileNameTextField.value))
            {
                EditorUtility.DisplayDialog("Invalid file name.", "Please ensure the file name you've typed in is valid.", "Roger!");
                return;
            }

            // todo
        }

        private void Load()
        {
            //string filePath = EditorUtility.OpenFilePanel("Dialogue Graphs", "Assets/Editor/DialogueSystem/Graphs", "asset");

            //if (string.IsNullOrEmpty(filePath))
                //return;
            // todo
            // Clear();
        }

        private void Clear()
        {
            m_DialogueGraphView.Clear();
        }

        private void ResetGraph()
        {
            Clear();
            UpdateFileName(m_DefaultFileName);
        }

        private void ToggleMiniMap()
        {
            m_DialogueGraphView.ToggleMiniMap();
            m_MiniMapBtn.ToggleInClassList("ds-toolbar__button__selected");
        }

        public static void UpdateFileName(string newFileName)
        {
            m_FileNameTextField.value = newFileName;
        }

    }
}

