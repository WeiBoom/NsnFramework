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

        [LabelText("动画"), Tooltip("界面动画类型")]
        public EPanelTween tweenType = EPanelTween.None;

        [LabelText("层级"), Tooltip("界面层级")]
        public EPanelLayer panelLayer = EPanelLayer.Main;

        [SceneObjectsOnly]
        private Button mask;

        public Transform Content { get; private set; }

        public EPanelLayer PanelLayer => panelLayer;

        private Tween tween;

        public bool IsActive { get; private set; } = false;

        protected override void OnAwake()
        {
            Content = transform.Find("Content");
        }

        protected override void OnShow()
        {
            base.OnShow();
            IsActive = true;
            OnPlayShowAnim();
        }

        protected override void OnHide()
        {
            base.OnHide();
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
