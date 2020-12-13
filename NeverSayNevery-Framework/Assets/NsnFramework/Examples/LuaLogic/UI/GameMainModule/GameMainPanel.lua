---@class V_GameMain
local GameMainPanel = {isActive = false};
local this = GameMainPanel;
---@type UI_GameMain
local panel = require("GameMainAttribute")

function GameMainPanel.OnAwake(obj)
     panel.OnInitializePanel(obj);

     UIListener.AddLuaButtonClick(panel.btn_start,function(btn) print(btn.gameObject.name) end);
     UIListener.AddLuaButtonClick(panel.btn_option,function(btn) print(btn.gameObject.name) end);
     UIListener.AddLuaButtonClick(panel.btn_quit,function(btn) print(btn.gameObject.name) end);


     print("GameMainPanel OnAwake")
end

function GameMainPanel.OnShow()
     print("GameMainPanel OnShown")
end

return this;