
--print("获取UIManager")
---@class UIManager
UIMgr = CS.NeverSayNever.UIManager.Instance;
--print("获取ResourceManager")
---@class ResourceManager
ResMgr = CS.NeverSayNever.ResourceManager;
UIListener = CS.NeverSayNever.UIListener


require("UIModuleDefine")
UIMgr:OpenPanel(UIModules.GameMain.Panel);
