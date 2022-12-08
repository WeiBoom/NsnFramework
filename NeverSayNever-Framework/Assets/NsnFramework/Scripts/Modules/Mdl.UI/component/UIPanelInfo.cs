using System;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using Sirenix.OdinInspector;

namespace NeverSayNever
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(NsnGraphicRaycaster))]
    public class UIPanelInfo : UIBaseBehaviour
    {

        public AnimationCurve animationCurve;

        [LabelText("Anim"), Tooltip("Window Animation Type")]
        public EPanelTween tweenType = EPanelTween.None;

        [LabelText("Layer"), Tooltip("Displayer Layer")]
        public EPanelLayer panelLayer = EPanelLayer.Main;

        [SceneObjectsOnly]
        private Button mask;

        public Transform Content { get; private set; }

        public EPanelLayer PanelLayer => panelLayer;

        private Tween tween;

        public bool IsActive { get; private set; } = false;

        private void Awake()
        {
            Content = transform.Find("Content");
        }

        private void OnEnable()
        {
            IsActive = true;
            OnPlayShowAnim();
        }

        protected void OnDisable()
        {
            IsActive = false;
        }

        private void OnPlayShowAnim()
        {
            if (animationCurve != null)
                tween.SetEase(animationCurve);
            switch (tweenType)
            {
                case EPanelTween.None:
                    break;
                case EPanelTween.Scale:
                    DoScaleTween();
                    break;
                case EPanelTween.Move:
                    DoMoveTween();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DoScaleTween()
        {
            Content.localScale = Vector3.zero;
            tween = Content.DOScale(Vector3.one, 0.2f);
        }

        private void DoMoveTween()
        {

        }

        public void DoCloseTween(System.Action closeFunc)
        {
            if (tweenType == EPanelTween.None) return;
            tween.onComplete += () =>
            {
                closeFunc?.Invoke();
                tween.onComplete = null;
            };
            tween.PlayBackwards();
        }
    }
}
