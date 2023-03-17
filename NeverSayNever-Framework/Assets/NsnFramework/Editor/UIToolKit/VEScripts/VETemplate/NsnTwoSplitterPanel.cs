using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NsnTwoSplitterPanel : MonoBehaviour
{
    static readonly string s_UssPath = $"Assets/Scripts/Shared/TestbedCore/Editor/UIElements/TwoPaneSplitView.uss";

    static readonly string k_UssClassName = "unity-two-pane-split-view";
    static readonly string k_ContentContainerClassName = "unity-two-pane-split-view__content-container";
    static readonly string k_HandleDragLineClassName = "unity-two-pane-split-view__dragline";
    static readonly string k_HandleDragLineVerticalClassName = k_HandleDragLineClassName + "--vertical";
    static readonly string k_HandleDragLineHorizontalClassName = k_HandleDragLineClassName + "--horizontal";
    static readonly string k_HandleDragLineAnchorClassName = "unity-two-pane-split-view__dragline-anchor";
    static readonly string k_HandleDragLineAnchorVerticalClassName = k_HandleDragLineAnchorClassName + "--vertical";
    static readonly string k_HandleDragLineAnchorHorizontalClassName = k_HandleDragLineAnchorClassName + "--horizontal";
    static readonly string k_VerticalClassName = "unity-two-pane-split-view--vertical";
    static readonly string k_HorizontalClassName = "unity-two-pane-split-view--horizontal";

    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    public new class UxmlFactory : UxmlFactory<TwoPaneSplitView, UxmlTraits>
    {
    }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlIntAttributeDescription m_FixedPaneIndex = new UxmlIntAttributeDescription { name = "fixed-pane-index", defaultValue = 0 };
        UxmlIntAttributeDescription m_FixedPaneInitialSize = new UxmlIntAttributeDescription { name = "fixed-pane-initial-size", defaultValue = 100 };
        UxmlStringAttributeDescription m_Orientation = new UxmlStringAttributeDescription { name = "orientation", defaultValue = "horizontal" };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield break; }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var fixedPaneIndex = m_FixedPaneIndex.GetValueFromBag(bag, cc);
            var fixedPaneInitialSize = m_FixedPaneInitialSize.GetValueFromBag(bag, cc);
            var orientationStr = m_Orientation.GetValueFromBag(bag, cc);
            var orientation = orientationStr == "horizontal"
                ? Orientation.Horizontal
                : Orientation.Vertical;

            //((TwoPaneSplitView)ve).Init(fixedPaneIndex, fixedPaneInitialSize, orientation);
        }
    }
}
