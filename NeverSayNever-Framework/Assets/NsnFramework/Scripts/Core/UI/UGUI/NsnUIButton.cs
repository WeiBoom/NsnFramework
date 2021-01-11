using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NsnUIButton : Button
{
    public TMPro.TextMeshProUGUI buttonLabel;
    public Image buttonSprite;

    protected override void Awake()
    {
        base.Awake();
        buttonSprite = GetComponent<Image>();
    }

    public void SetSprite(string spriteName)
    {
    }

    public void SetText(string text)
    {
        if (buttonLabel != null)
            buttonLabel.text = text;
        else
            NeverSayNever.Utilities.ULog.Warning($"{this.gameObject.name}'s buttonlabel is null, please check component");
    }
}
