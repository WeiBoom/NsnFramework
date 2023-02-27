using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Nsn.EditorToolKit
{

    [System.Serializable]
    public class VEMenuConfig
    {
        public string MenuPath;
        public string MenuName;
        public string MenuType;
    }

    public class VEMenuConfigList
    {
        public VEMenuConfig[] Values;
    }

    /// <summary>
    /// Visual Element ToolKit Config
    /// </summary>
    public static class VEConfig
    {
        public static List<VEMenuConfig> MenuConfigList { get; private set; }

        public static void LoadConfig()
        {
            if(MenuConfigList == null)
                MenuConfigList = new List<VEMenuConfig>();
            else
                MenuConfigList.Clear();

            string menuConfigPath = $"{NEditorConst.NsnUIToolKitConfigPath}/NsnVEMenuConfig.json";

            if(System.IO.File.Exists(menuConfigPath))
            {
                var jsonStr = System.IO.File.ReadAllText(menuConfigPath);
                VEMenuConfigList configList = JsonUtility.FromJson<VEMenuConfigList>(jsonStr);
                if(configList != null && configList.Values != null)
                {
                    foreach(VEMenuConfig config in configList.Values)
                    {
                        MenuConfigList.Add(config);
                    }
                }
            }
            else
            {
                NsnLog.Error("NsnUIToolKit , NsnVEMenuConfig is not exist!");
            }
        }
    }

}

