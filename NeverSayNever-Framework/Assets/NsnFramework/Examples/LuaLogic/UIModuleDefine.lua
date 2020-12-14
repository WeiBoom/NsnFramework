
---@class UIModule
---@field ModuleName string
---@field Panel string
---@field Messenger string

---@type UIModule[]
UIModules = {
    GameMain = {ModuleName = "GameMain" ,Panel = "GameMainPanel",Messenger = "GameMainMessenger"};
}

local LuaModules = {}
---全局方法，由C#获取并缓存
function NewLuaWindow(name)
    if LuaModules[name] ~= nil then
        -- 重复打开只初始化数据信息
        LuaModules[name].OnShow();
        return LuaModules[name];
    else
        local moduleName = name
        LuaModules[name] = require(moduleName);
        if LuaModules[name] ~= nil then
            return LuaModules[name];
        else
            LogError("没找到Lua面板类 ：" .. name);
        end
    end
end

for k,v in pairs(UIModules) do
    UIMgr:RegisterLuaPanel(v.Panel)
end



