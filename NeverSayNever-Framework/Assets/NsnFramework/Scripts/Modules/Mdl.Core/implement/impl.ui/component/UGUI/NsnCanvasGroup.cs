using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NsnCanvasGroup : MonoBehaviour
{
    private CanvasRenderer[] rendererList;

    private float value = 1;
    public float Value
    {
        get => value;
        set
        {
            SetCanvasGroupValue();
        }
    }

    private void Awake()
    {
        rendererList = GetComponentsInChildren<CanvasRenderer>();
    }

    private void SetCanvasGroupValue()
    {
        if (rendererList.Length > 0)
        {
            foreach (var renderer in rendererList)
            {
                renderer.SetAlpha(value);
            }
        }
    }

}
