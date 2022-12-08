using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NeverSayNever
{
    public static class UIListener
    {

        public static void AddButtonClick(Button btn, UnityAction action)
        {
            if (btn != null)
                btn.onClick.AddListener(action);
        }

        public static void RemoveButtonClick(Button btn, UnityAction action)
        {
            if (btn != null)
                btn.onClick.RemoveListener(action);
        }

        public static void RemoveButtonEvent(Button btn, UnityAction action)
        {
            
        }

        public static void RemoveButtonAllEvent(Button btn)
        {

        }

    }
}