---@class UI_GameMain
local GameMainAttribute = {gameObject = nil, transfrom = nil};
local this = GameMainAttribute;

function GameMainAttribute.OnInitializePanel(obj)
    this.gameObject = obj;
    this.transfrom = obj.transform;
    this.UIInfos = obj:GetComponent("UIPanelInfo");

	this.tmp_title = this.UIInfos:GetUICollection("tmp_title")
	this.btn_start = this.UIInfos:GetUICollection("btn_start")
	this.btn_option = this.UIInfos:GetUICollection("btn_option")
	this.btn_quit = this.UIInfos:GetUICollection("btn_quit")


end

return this;