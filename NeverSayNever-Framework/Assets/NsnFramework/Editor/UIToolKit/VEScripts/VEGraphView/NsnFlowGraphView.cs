using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nsn.EditorToolKit
{

    public class NsnFlowGraphView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<NsnFlowGraphView, UxmlTraits> { }

        public NsnFlowGraphView()
        {
            GridBackground gridBackground = new GridBackground();
            Insert(0, gridBackground);

            // 视图缩放
            this.AddManipulator(new ContentZoomer());
            // 允许鼠标拖动一个或多个元素
            this.AddManipulator(new ContentDragger());
            // 选项拖动
            this.AddManipulator(new SelectionDragger());
            // 矩形选择框
            this.AddManipulator(new RectangleSelector());
            // 自由选择工具
            this.AddManipulator(new FreehandSelector());

            StyleSheet styleSheet = VEToolKit.LoadVEAssetStyleSheet("NsnFlowGraphView");
            styleSheets.Add(styleSheet);


        }
    }
}
