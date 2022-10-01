using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{
    /// <summary>
    /// Luaģ�飬������xlua
    /// </summary>
    public interface ILuaMdl : INsnModule
    {
        XLua.LuaTable Global { get;}
        XLua.LuaTable NewTable();
        XLua.LuaTable CallNewLuaWindowFunc(string scriptName);

        void LoadScriptBundle(string name);
        string ReadLuaFile(string fileName);
        object[] DoFile(string fileName);
    }

}
