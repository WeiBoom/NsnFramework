
--print("获取UIManager")
---@class UIManager
UIMgr = CS.NeverSayNever.Core.UIManager.Instance;
--print("获取ResourceManager")
---@class ResourceManager
ResMgr = CS.NeverSayNever.Core.Asset.ResourceManager;
UIListener = CS.NeverSayNever.Core.UIListener


require("UIModuleDefine")
UIMgr:OpenPanel(UIModules.GameMain.Panel);
