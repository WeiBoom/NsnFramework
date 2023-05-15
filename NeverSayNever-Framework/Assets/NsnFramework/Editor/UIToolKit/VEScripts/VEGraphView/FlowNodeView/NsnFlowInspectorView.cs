using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nsn.EditorToolKit
{
    public class NsnFlowInspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<NsnFlowInspectorView, UxmlTraits> { }

        /// <summary>
        /// 加载指定类型的Inspector界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ShowInspector<T>() where T : NsnBaseEditorWidget
        {
            
        }

    }
}

