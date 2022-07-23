using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XLua;

namespace NeverSayNever
{
        [LuaCallCSharp]
    public static class UIListener
    {
        public static void AddLuaButtonClick(Button btn, LuaFunction function)
        {
            if(btn != null)
                btn.AddClickListener(() => { function?.Call(btn); });
        }

        public static void AddButtonClick(Button btn, UnityAction action)
        {
            if (btn != null)
                btn.AddClickListener(action);
        }

        public static void RemoveButtonClick(Button btn, UnityAction action)
        {
            if (btn != null)
                btn.RemoveClickListener(action);
        }

        public static void RemoveButtonEvent(Button btn, UnityAction action)
        {
            
        }

        public static void RemoveButtonAllEvent(Button btn)
        {

        }
    }
}