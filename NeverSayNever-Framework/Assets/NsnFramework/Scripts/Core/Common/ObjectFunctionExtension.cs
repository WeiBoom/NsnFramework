using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public static class ObjectFunctionExtension
{
    #region Base Data

    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    public static int ToInt(this float num)
    {
        return (int)num;
    }

    public static int ToInt(this double num)
    {
        return (int)num;
    }

    public static int ToInt(this string str)
    {
        return int.Parse(str);
    }

    public static long ToLong(this float num)
    {
        return (long)num;
    }

    public static long ToLong(this double num)
    {
        return (long)num;
    }

    public static long ToLong(this string str)
    {
        return long.Parse(str);
    }

    #endregion

    #region Transform

    public static T GetOrAddComponent<T>(this Transform trans) where T : Component
    {
        var component = trans.GetComponent<T>();
        if (component == null)
        {
            component = trans.gameObject.AddComponent<T>();
        }
        return component;
    }

    public static void ClearChildren(this Transform trans)
    {
        if (trans == null)
            return;
        var childCount = trans.childCount;
        for (var i = childCount - 1; i >= 0; i++)
        {
            var child = trans.GetChild(i);
            if (child == null) continue;
            child.SetParent(null);
            UnityEngine.Object.Destroy(child);
        }
    }

    public static void ClearChildrenImmediate(this Transform trans)
    {
        if (trans == null)
            return;
        var childCount = trans.childCount;
        for (var i = childCount - 1; i >= 0; i++)
        {
            var child = trans.GetChild(i);
            if (child == null) continue;
            child.SetParent(null);
            UnityEngine.Object.DestroyImmediate(child);
        }
    }

    public static Transform SetParentAndNormalized(this Transform trans, Transform parent)
    {
        trans.SetParent(parent);
        trans.SetNormalized();
        return trans;
    }
    public static Transform SetParentAndScale(this Transform trans, Transform parent, float scale)
    {
        trans.SetParent(parent);
        trans.localPosition = Vector3.zero;
        trans.localScale = Vector3.one * scale;
        return trans;
    }

    public static Transform SetScale(this Transform trans, float scale)
    {
        trans.localScale = Vector3.one * scale;
        return trans;
    }


    public static Transform SetLocalPositionNormalized(this Transform trans)
    {
        trans.localPosition = Vector3.zero;
        return trans;
    }

    public static Transform SetScaleNormalized(this Transform trans)
    {
        trans.localScale = Vector3.one;
        return trans;
    }

    public static Transform SetNormalized(this Transform trans)
    {
        trans.localPosition = Vector3.zero;
        trans.localScale = Vector3.one;
        return trans;
    }

    public static Transform SetPositionX(this Transform trans, float x)
    {
        var position = trans.position;
        position = new Vector3(x, position.y, position.z);
        trans.position = position;
        return trans;
    }

    public static Transform SetPositionY(this Transform trans, float y)
    {
        var position = trans.position;
        position = new Vector3(position.x, y, position.z);
        trans.position = position;
        return trans;
    }


    public static Transform SetPositionZ(this Transform trans, float z)
    {
        var position = trans.position;
        position = new Vector3(position.x, position.y, z);
        trans.position = position;
        return trans;
    }

    public static Transform SetLocalPositionX(this Transform trans, float x)
    {
        var localPosition = trans.localPosition;
        localPosition = new Vector3(x, localPosition.y, localPosition.z);
        trans.localPosition = localPosition;
        return trans;
    }

    public static Transform SetLocalPositionY(this Transform trans, float y)
    {
        var localPosition = trans.localPosition;
        localPosition = new Vector3(localPosition.x, y, localPosition.z);
        trans.localPosition = localPosition;
        return trans;
    }

    public static Transform SetLocalPositionZ(this Transform trans, float z)
    {
        var localPosition = trans.localPosition;
        localPosition = new Vector3(localPosition.x, localPosition.y, z);
        trans.localPosition = localPosition;
        return trans;
    }

    public static Transform SetScaleX(this Transform trans, float x)
    {
        var localScale = trans.localScale;
        localScale = new Vector3(x, localScale.y, localScale.z);
        trans.localScale = localScale;
        return trans;
    }

    public static Transform SetScaleY(this Transform trans, float y)
    {
        var localScale = trans.localScale;
        localScale = new Vector3(localScale.x, y, localScale.z);
        trans.localScale = localScale;
        return trans;
    }

    public static Transform SetScaleZ(this Transform trans, float z)
    {
        var localScale = trans.localScale;
        localScale = new Vector3(localScale.x, localScale.y, z);
        trans.localScale = localScale;
        return trans;
    }

    public static Transform FlipX(this Transform trans)
    {
        var localScale = trans.localScale;
        localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        trans.localScale = localScale;
        return trans;
    }

    public static Transform FlipY(this Transform trans)
    {
        var localScale = trans.localScale;
        localScale = new Vector3(localScale.x, -localScale.y, localScale.z);
        trans.localScale = localScale;
        return trans;
    }

    public static Transform FlipZ(this Transform trans)
    {
        var localScale = trans.localScale;
        localScale = new Vector3(localScale.x, localScale.y, -localScale.z);
        trans.localScale = localScale;
        return trans;
    }

    #endregion

    #region GameObject

    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
    {
        var component = obj.GetComponent<T>();
        if (component == null)
        {
            component = obj.AddComponent<T>();
        }
        return component;
    }

    public static T GetOrAddComponent<T>(this MonoBehaviour mono) where T : Component
    {
        var component = mono.gameObject.GetComponent<T>();
        if (component == null)
        {
            component = mono.gameObject.AddComponent<T>();
        }
        return component;
    }

    public static Component GetOrAddComponent(this GameObject obj, System.Type type)
    {
        var component = obj.GetComponent(type);
        if (component == null)
        {
            component = obj.AddComponent(type);
        }
        return component;
    }

    public static GameObject SetLayer(this GameObject obj, string layerName, bool isCycle = false)
    {
        if (isCycle)
        {
            var children = obj.GetComponentsInChildren<Transform>(true);
            foreach (var item in children)
            {
                if (item != null)
                    item.gameObject.layer = LayerMask.NameToLayer(layerName);
            }
        }
        obj.layer = LayerMask.NameToLayer(layerName);
        return obj;
    }

    #endregion

    #region UGUI

    #region UGUI RawImage

    public static void LoadTexture(this RawImage rawImage, string textureName, bool isNativeSize = false)
    {
        NeverSayNever.ResourceManager.LoadTexture(textureName, (obj) =>
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

    #region UGUI Button

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

    #endregion
}
