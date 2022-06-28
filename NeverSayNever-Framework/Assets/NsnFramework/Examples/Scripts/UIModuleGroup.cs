using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.Example
{
    public class UIModule
    {
        public string ModuleName { get; private set; }
        public string NameSpace { get; private set; }

        public UIModule(string moduleName, string nameSpace = "")
        {
            ModuleName = moduleName;
            NameSpace = nameSpace;
        }

        public string GetModuleName()
        {
            if (NameSpace.IsNullOrEmpty())
            {
                return ModuleName;
            }
            else
            {
                return string.Format("{0}.{1}", NameSpace, ModuleName);
            }
        }
    }


    public static class UIModuleGroup
    {
        public static UIModule GameMain = new UIModule("GameMain","NeverSayNever.Example");
        public static UIModule GameLoading = new UIModule("GameLoading", "NeverSayNever.Example");
    }
}