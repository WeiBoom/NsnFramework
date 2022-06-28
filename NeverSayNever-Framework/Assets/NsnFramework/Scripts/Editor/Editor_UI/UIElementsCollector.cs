using UnityEditor;
using UnityEngine;

namespace NeverSayNever.EditorUtilitiy
{
    using NeverSayNever.Core.HUD;

    public static class UIElementsCollector
    {

        private static UIBaseBehaviour GetSelectedUITarget()
        {
            UIBaseBehaviour target = null;
            var selectedObj = Selection.objects[0];
            if (selectedObj != null)
            {
                var uiObj = (selectedObj as GameObject);
                if (uiObj != null)
                    target = uiObj.GetComponent<UIBaseBehaviour>();
            }
            if (target == null)
            {
                EditorUtility.DisplayDialog("Never Say Never",
                     "Please select an object which has component as 'UIBaseBehaviour' ", "ok");
                Debug.LogError("请选择一个UI预制体");
            }
            return target;
        }

        [MenuItem("GameObject/NSNCore/UICollector", priority = 0)]
        public static void CollectElements()
        {
            UIBaseBehaviour panel = GetSelectedUITarget();
            if (panel != null)
            {
                NSNPanelElementsCollector.CollectPanelUIElements(panel);
            }
           
        }

        [MenuItem("GameObject/NSNCore/Build CSharp Script", priority = 1)]
        public static void BuildUIPanelCSharpScript()
        {
            UIBaseBehaviour target = GetSelectedUITarget();
            if (target != null)
            {
                UIScriptBuilderForCSharp.BuildCSharpScriptForPanel(target);
            }
            
        }
    }


}
