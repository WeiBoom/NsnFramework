using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SOCommonConfig : SerializedScriptableObject
{
    [SerializeField]
    private string luaScriptPath = Application.dataPath.Replace("Assets", "LuaScripts");
    /// <summary>
    /// lua脚本存放的路径
    /// </summary>
    public string LuaSciprtDirectory => luaScriptPath;

}
