using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace NeverSayNever.Core.HUD
{
    public class NImage : UnityEngine.UI.Image
    {
        [HideInInspector]
        public new CanvasRenderer canvasRenderer;
        public new int depth => canvasRenderer.absoluteDepth;
        

        protected override void Awake()
        {
            base.Awake();
            canvasRenderer = GetComponent<CanvasRenderer>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if(!raycastTarget)
                GraphicRegistry.UnregisterGraphicForCanvas(canvas, this);
        }

        protected override void OnTransformParentChanged()
        {
            base.OnTransformParentChanged();
            if (!raycastTarget)
                GraphicRegistry.UnregisterGraphicForCanvas(canvas, this);
        }

        protected override void OnCanvasHierarchyChanged()
        {
            base.OnCanvasHierarchyChanged();
            if(!raycastTarget)
                GraphicRegistry.UnregisterGraphicForCanvas(canvas, this);
        }

        public void SetSprite(string name)
        {
            // todo
            //var sprite = UIManager.Instance.GetSprite(name);
            //this.sprite = sprite;
            //
        }

    }
}