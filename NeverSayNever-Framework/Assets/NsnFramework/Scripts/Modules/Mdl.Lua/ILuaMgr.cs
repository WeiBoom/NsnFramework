using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XLua;

namespace NeverSayNever
{
    public interface ILuaMgr : IManager
    {
        LuaTable Global { get;}

        LuaTable NewTable();

        void LoadScriptBundle(string name);

        string ReadLuaFile(string fileName);

        LuaTable CallNewLuaWindowFunc(string scriptName);

        object[] DoFile(string fileName);
    }

}
