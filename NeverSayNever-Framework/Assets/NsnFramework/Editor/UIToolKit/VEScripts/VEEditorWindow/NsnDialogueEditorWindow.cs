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
            // 先添加graphview ，再添加toolbar ，否则 toolbar会被graphview盖住
            AddDialogueGraphView();
            AddToolBar();

            var styleSheet = VEToolKit.LoadVEAssetStyleSheet(NEditorConst.NsnStyleSheet_Variables);
            m_Root.styleSheets.Add(styleSheet);
        }

        private void AddDialogueGraphView()
        {
            m_DialogueGraphView = new NsnDialogueGraphView(this);
            m_Root.Add(m_DialogueGraphView);
            m_DialogueGraphView.StretchToParentSize();
        }

        private void AddToolBar()
        {
            Toolbar toolbar = new Toolbar();
            m_FileNameTextField = new TextField() { value = m_DefaultFileName, label = "File Name:" };
            m_FileNameTextField.RegisterValueChangedCallback(ve => {
                m_FileNameTextField.value = ve.newValue;
            });

            m_SaveBtn = VEToolKit.CreateButton("Save", Save);

            Button loadBtn = VEToolKit.CreateButton("Load", Load);

            Button clearBtn = VEToolKit.CreateButton("Clear", Clear);

            Button resetBtn = VEToolKit.CreateButton("Reset", ResetGraph);

            m_MiniMapBtn = VEToolKit.CreateButton("MiniMap", ToggleMiniMap);

            toolbar.Add(m_FileNameTextField);
            toolbar.Add(m_SaveBtn);
            toolbar.Add(loadBtn);
            toolbar.Add(clearBtn);
            toolbar.Add(resetBtn);
            toolbar.Add(m_MiniMapBtn);

            var styleSheet = VEToolKit.LoadVEAssetStyleSheet(NEditorConst.NsnStyleSheet_ToolbarStyles);
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

            NsnDialogueUtility.SaveDialogue(m_DialogueGraphView, m_FileNameTextField.text);
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
            m_DialogueGraphView.ClearGraph();
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

