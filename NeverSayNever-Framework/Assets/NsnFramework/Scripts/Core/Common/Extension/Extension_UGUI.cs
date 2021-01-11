using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using NeverSayNever.Core;

public static class Extension_UGUI
{

    #region Image

    public static void SetSprite(this Image img, string atlasName, string spriteName)
    {
        // todo
    }

    public static void SetSprite(this Image img, string spriteName)
    {
       
    }

    #endregion

    #region RawImage
    public static void LoadTexture(this RawImage rawImage, string textureName, bool isNativeSize = false)
    {
        NeverSayNever.Core.Asset.ResourceManager.LoadTexture(textureName, (obj) =>
         {
             var texture = (Texture)obj;
             rawImage.texture = texture;
             if (isNativeSize)
                 rawImage.SetNativeSize();
         });
    }

    public static void LoadTexture(this RawImage rawImage, string textureName, float sizeX, float sizeY)
    {
        rawImage.LoadTexture(textureName);
        var newSize = new Vector2(sizeX, sizeY);
        rawImage.rectTransform.sizeDelta = newSize;
    }
    #endregion

    #region Button

    public static Button ChangeSprite(this Button btn, string spriteName)
    {
        var image = btn.GetComponent<Image>();

        if (image != null)
            image.SetSprite(spriteName);

        return btn;
    }

    public static Button AddClickListener(this Button btn, UnityAction action)
    {
        btn.onClick.AddListener(action);
        return btn;
    }

    public static Button RemoveClickListener(this Button btn, UnityAction action)
    {
        btn.onClick.RemoveListener(action);
        return btn;
    }
    #endregion
}
