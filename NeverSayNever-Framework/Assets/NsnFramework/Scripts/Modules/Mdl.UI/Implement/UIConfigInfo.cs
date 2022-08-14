
using System;

namespace NeverSayNever
{
    [Serializable]
    public class UIConfigInfo
    {
        public int ID;
        public string Path;
        public string Name;
        public int Layer;
        public bool Lua;// 是否是Lua 的界面

        public static UIConfigInfo DeserializeFromJson(string json)
        {
            UIConfigInfo configInfo = null;
            if(!string.IsNullOrEmpty(json))
                configInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<UIConfigInfo>(json);
            return configInfo;
        }
    }
}
