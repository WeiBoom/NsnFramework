using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XLua;

namespace NeverSayNever.Core
{
        [LuaCallCSharp]
    public static class UIListener
    {
        public static void AddLuaButtonClick(Button btn, LuaFunction function)
        {
            btn.AddClickListener(() => { function?.Call(btn); });
        }

        public static void AddButtonClick(Button btn, UnityAction action)
        {
            btn.AddClickListener(action);
        }

        public static void RemoveButtonClick(Button btn, UnityAction action)
        {
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