---@class V_GameMain
local GameMainPanel = {isActive = false};
local this = GameMainPanel;
---@type UI_GameMain
local panel = require("GameMainAttribute")

function GameMainPanel.OnAwake(obj)
     -- 初始化界面组件相关信息
     panel.OnInitializePanel(obj);
     -- 注册按钮事件
     UIListener.AddLuaButtonClick(panel.btn_start, 
          function(btn) print(btn.gameObject.name) end);
     UIListener.AddLuaButtonClick(panel.btn_option,
          function(btn) print(btn.gameObject.name) end);
     UIListener.AddLuaButtonClick(panel.btn_quit, 
          function(btn) print(btn.gameObject.name) end);
     print("GameMainPanel OnAwake")
end
return this;