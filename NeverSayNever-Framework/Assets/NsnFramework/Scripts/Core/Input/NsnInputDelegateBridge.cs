
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Nsn
{
    /// <summary>
    /// 输入模块的桥接类，用于注册相关的委托事件
    /// </summary>
    public class NsnInputDelegateBridge
    {
        /// <summary>
        /// 检查目标对象的点击事件是否会被指定UI遮挡的委托
        /// </summary>
        public delegate bool GuideCheckBlockingDelegate(GameObject raycastObj);

        /// <summary>
        /// 默认会遮挡的层级Order
        /// </summary>
        private static int m_DefaultUIOrder = 30;
        
        /// <summary>
        /// 委托 - Input事件白名单(无视遮挡条件，直接触发)
        /// </summary>
        private static Dictionary<GameObject, Dictionary<EventTriggerType, UnityEvent>> m_EventWhiteList =
            new Dictionary<GameObject, Dictionary<EventTriggerType, UnityEvent>>();

        /// <summary>
        /// 委托 - 检查当前检测到的对象是否满足遮挡条件
        /// </summary>
        private static GuideCheckBlockingDelegate m_CheckBlockingDelegate;

        /// <summary>
        /// 注册InputModule相关的事件
        /// </summary>
        public static void InitGuideInputDelegate()
        {
            NsnInputModule inputModule = (NsnInputModule)EventSystem.current.currentInputModule;
            if (inputModule != null)
            {
                inputModule.FilterBlockingEventCallback = FilterGuideBlockingEvent;
                inputModule.WhiteListEventCallback = InvokeWhiteListCallback;
            }
        }
        
        /// <summary>
        /// 执行白名单事件
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="type">事件类型</param>
        public static void InvokeWhiteListCallback(GameObject target, EventTriggerType type)
        {
            if (target == null)
                return;
            if (m_EventWhiteList.ContainsKey(target))
            {
                if (m_EventWhiteList[target].ContainsKey(type))
                {
                    m_EventWhiteList[target][type].Invoke();
                }
            }
        }

        /// <summary>
        /// 检查目标对象是否可以通过的遮挡条件
        /// </summary>
        public static bool FilterGuideBlockingEvent(GameObject raycastObj)
        {
            bool checkResult = true;
            if (m_CheckBlockingDelegate != null)
                checkResult = m_CheckBlockingDelegate.Invoke(raycastObj);
            return checkResult;
        }

        /// <summary>
        /// (Example) - 检查目标对象是否是UI的节点，如果是，则检查所属UI是否指定的UI层级
        /// </summary>
        /// <param name="raycastObj"></param>
        /// <returns></returns>
        private static bool FilterUIViewOrder(GameObject raycastObj)
        {
            // 根据UI得Order，自行决定使用哪个参数来排列，
            Canvas canvas = raycastObj.GetComponentInParent<Canvas>();
            if (canvas && (canvas.sortingOrder >= m_DefaultUIOrder))
                return true;
            /*
             * 这里可以添加一些特殊的UI对象,如GM、Console、Debug等
             */
            return false;
        }

    }
}