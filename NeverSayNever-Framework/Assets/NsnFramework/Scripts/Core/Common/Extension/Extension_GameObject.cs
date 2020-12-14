using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension_GameObject
{
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

    public static GameObject SetActive(this GameObject obj, bool state)
    {
        obj.SetActive(state);
        return obj;
    }
}
