using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension_Transform
{
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
            Object.Destroy(child);
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
            Object.DestroyImmediate(child);
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

}
