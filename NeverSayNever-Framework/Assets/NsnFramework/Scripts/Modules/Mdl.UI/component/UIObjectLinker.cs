using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjectLinker : MonoBehaviour
{
    public Dictionary<string, UnityEngine.Object> LinkedObjects = new Dictionary<string, Object>();

    public void CollectObjects()
    {
        LinkedObjects.Clear();
    }

    public UnityEngine.Object GetObject(string name)
    {
        LinkedObjects.TryGetValue(name, out UnityEngine.Object obj);
        return obj;
    }
}
