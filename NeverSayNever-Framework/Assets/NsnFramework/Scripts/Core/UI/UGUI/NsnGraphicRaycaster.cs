using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//namespace NeverSayNever.Core.HUD
//{
public class NsnGraphicRaycaster : UnityEngine.UI.GraphicRaycaster
{
    // 优化GraphicRaycaster.get_eventCamera()
    private Camera targetCamera;

    // 源码中的 canvas 每次获取的时候都会有canvas != null 的判断，如果没有动态变化，常规下可以直接减少这个操作
    private Canvas Canvas { get; set; }

    protected override void Awake()
    {
        base.Awake();
        Canvas = GetComponent<Canvas>();
    }


    public override Camera eventCamera
    {
        get
        {
            // UGUI 源码，他并没有对camera进行缓存，而UI上的camera，一般来说是不会改变的。所以这里做一个缓存优化
            /* 
                 if (canvas.renderMode == RenderMode.ScreenSpaceOverlay || (canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera == null))
                    return null;
                 return canvas.worldCamera != null ? canvas.worldCamera : Camera.main;
            */
            if (targetCamera == null)
                targetCamera = base.eventCamera;
            return targetCamera;
        }
    }


    [NonSerialized] private List<Graphic> m_RaycastResults = new List<Graphic>();
    public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
    {
        // 拖动的时候，不会有其他点击事件发生，可以不再进行判定
        if (eventData.dragging) return;

        if (Canvas == null)
            return;
        var canvasGraphics = GraphicRegistry.GetGraphicsForCanvas(Canvas);
        if (canvasGraphics == null || canvasGraphics.Count <= 0)
            return;

        int displayIndex;
        var currentEventCamera = eventCamera; // Propery can call Camera.main, so cache the reference

        if (Canvas.renderMode == RenderMode.ScreenSpaceOverlay || currentEventCamera == null)
            displayIndex = Canvas.targetDisplay;
        else
            displayIndex = currentEventCamera.targetDisplay;

        var eventPosition = Display.RelativeMouseAt(eventData.position);
        if (eventPosition != Vector3.zero)
        {
            int eventDisplayIndex = (int)eventPosition.z;
            if (eventDisplayIndex != displayIndex)
                return;
        }
        else
        {
            eventPosition = eventData.position;
        }

        Vector2 pos;
        if (currentEventCamera == null)
        {
            float w = Screen.width;
            float h = Screen.height;
            if (displayIndex > 0 && displayIndex < Display.displays.Length)
            {
                w = Display.displays[displayIndex].systemWidth;
                h = Display.displays[displayIndex].systemHeight;
            }
            pos = new Vector2(eventPosition.x / w, eventPosition.y / h);
        }
        else
            pos = currentEventCamera.ScreenToViewportPoint(eventPosition);

        if (pos.x < 0f || pos.x > 1f || pos.y < 0f || pos.y > 1f)
            return;

        float hitDistance = float.MaxValue;

        Ray ray = new Ray();

        if (currentEventCamera != null)
            ray = currentEventCamera.ScreenPointToRay(eventPosition);

        if (Canvas.renderMode != RenderMode.ScreenSpaceOverlay && blockingObjects != BlockingObjects.None)
        {
            float distanceToClipPlane = 100.0f;

            if (currentEventCamera != null)
            {
                float projectionDirection = ray.direction.z;
                distanceToClipPlane = Mathf.Approximately(0.0f, projectionDirection)
                    ? Mathf.Infinity
                    : Mathf.Abs((currentEventCamera.farClipPlane - currentEventCamera.nearClipPlane) / projectionDirection);
            }

            /*
                ReflectionMethodsCache 是一个internal 的单例，在UnityEngine.dll里，这里无法访问
                ReflectionMethodsCache.Singleton.raycast3D 和 ReflectionMethodsCache.Singleton.raycast2D 都是获取的Physics和physics2D上的Raycast方法
                一般我们在UI上不需要操作Physical层面的东西，所里这里也直接剔除掉
            */
            /*
            // 相较于源码，剔除了这部分检测，
            if (blockingObjects == BlockingObjects.ThreeD || blockingObjects == BlockingObjects.All)
            {
                // raycast3D 是Physics上的射线检测
                //var raycast3DMethodInfo = typeof(Physics).GetMethod("Raycast", new[] { typeof(Ray), typeof(RaycastHit).MakeByRefType(), typeof(float), typeof(int) });
                //if (raycast3DMethodInfo != null)
                    //raycast3D = (Raycast3DCallback)Delegate.CreateDelegate(typeof(Raycast3DCallback), raycast3DMethodInfo);

                if (ReflectionMethodsCache.Singleton.raycast3D != null)
                {
                    var hits = ReflectionMethodsCache.Singleton.raycast3DAll(ray, distanceToClipPlane, (int)m_BlockingMask);
                    if (hits.Length > 0)
                        hitDistance = hits[0].distance;
                }
            }

            if (blockingObjects == BlockingObjects.TwoD || blockingObjects == BlockingObjects.All)
            {
                    // raycast2D 是Physics2D上的射线检测
                //var raycast2DMethodInfo = typeof(Physics2D).GetMethod("Raycast", new[] { typeof(Vector2), typeof(Vector2), typeof(float), typeof(int) });
                //if (raycast2DMethodInfo != null)
                    //raycast2D = (Raycast2DCallback)Delegate.CreateDelegate(typeof(Raycast2DCallback), raycast2DMethodInfo);

            if (ReflectionMethodsCache.Singleton.raycast2D != null)
                {
                    var hits = ReflectionMethodsCache.Singleton.getRayIntersectionAll(ray, distanceToClipPlane, (int)m_BlockingMask);
                    if (hits.Length > 0)
                        hitDistance = hits[0].distance;
                }
            }

            */

        }

        m_RaycastResults.Clear();
        // Raycast函数内修改了源码的检测内容
        Raycast(Canvas, currentEventCamera, eventPosition, canvasGraphics, m_RaycastResults);

        int totalCount = m_RaycastResults.Count;
        for (var index = 0; index < totalCount; index++)
        {
            var go = m_RaycastResults[index].gameObject;
            bool appendGraphic = true;

            if (ignoreReversedGraphics)
            {
                if (currentEventCamera == null)
                {
                    var dir = go.transform.rotation * Vector3.forward;
                    appendGraphic = Vector3.Dot(Vector3.forward, dir) > 0;
                }
                else
                {
                    var cameraFoward = currentEventCamera.transform.rotation * Vector3.forward;
                    var dir = go.transform.rotation * Vector3.forward;
                    appendGraphic = Vector3.Dot(cameraFoward, dir) > 0;
                }
            }
            if (appendGraphic)
            {
                float distance;
                Transform trans = go.transform;
                Vector3 transForward = trans.forward;

                if (currentEventCamera == null || Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                    distance = 0;
                else
                {
                    distance = (Vector3.Dot(transForward, trans.position - ray.origin) / Vector3.Dot(transForward, ray.direction));
                    if (distance < 0)
                        continue;
                }

                if (distance >= hitDistance)
                    continue;
                var castResult = new RaycastResult
                {
                    gameObject = go,
                    module = this,
                    distance = distance,
                    screenPosition = eventPosition,
                    index = resultAppendList.Count,
                    depth = m_RaycastResults[index].depth,
                    sortingLayer = Canvas.sortingLayerID,
                    sortingOrder = Canvas.sortingOrder,
                    worldPosition = ray.origin + ray.direction * distance,
                    worldNormal = -transForward
                };
                resultAppendList.Add(castResult);
            }
        }
    }

    // 源码中用一个list缓存所有检测倒的图片，并依次添加，这里不再这样处理
    // [NonSerialized] static readonly List<Graphic> s_SortedGraphics = new List<Graphic>();
    private static void Raycast(Canvas canvas, Camera eventCamera, Vector2 pointerPosition, IList<Graphic> foundGraphics, List<Graphic> results)
    {
        int totalCount = foundGraphics.Count;
        // 源码中，把foundGraphics内所有的可点击的图都添加到了列表中，但是实际上最终响应的是最上层的那张图
        // 在UI层面上，一般也只会关注最上层的的需要响应的元素，所以这里缓存层级最高的UI元素，只把最上层的添加到最终检测的元素列表中
        Graphic upGraphic = null;
        int upIndex = -1;

        for (int i = 0; i < totalCount; ++i)
        {
            Graphic graphic = foundGraphics[i];
            int depth = graphic.depth;
            if (depth == -1 || !graphic.raycastTarget || graphic.canvasRenderer.cull)
                continue;

            if (!RectTransformUtility.RectangleContainsScreenPoint(graphic.rectTransform, pointerPosition, eventCamera))
                continue;

            if (eventCamera != null && eventCamera.WorldToScreenPoint(graphic.rectTransform.position).z > eventCamera.farClipPlane)
                continue;

            if (graphic.Raycast(pointerPosition, eventCamera))
            {
                //s_SortedGraphics.Add(graphic);
                if (depth > upIndex)
                {
                    upIndex = depth;
                    upGraphic = graphic;
                }
            }
        }
        // 源码中把所有的元素先按层级进行排序，然后一次添加到result中，
        // 这里不需要，只添加最上层的元素,因为其他元素也会在后续的检测中全部剔除
        /*
        s_SortedGraphics.Sort((g1, g2) => g2.depth.CompareTo(g1.depth));
        totalCount = s_SortedGraphics.Count;
        for (int i = 0; i < totalCount; ++i)
            results.Add(s_SortedGraphics[i]);

        s_SortedGraphics.Clear();
        */
        if (upGraphic != null)
            results.Add(upGraphic);
    }
}
