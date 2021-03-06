﻿using System;
using UnityEngine;

namespace NeverSayNever.Core
{
    public class UGameBehaviour : MonoBehaviour
    {
        private void Awake() => OnAwake();

        private void Start() => OnStart();

        private void OnEnable() => OnShow();

        private void FixedUpdate() => OnFixedUpdate();

        private void Update() => OnUpdate();

        private void LateUpdate() => OnLateUpdate();

        private void OnDisable() => OnHide();

        private void OnDestroy() => OnDestroyMe();

        protected virtual void OnAwake() { }

        protected virtual void OnStart() { }

        protected virtual void OnShow() { }

        protected virtual void OnFixedUpdate() { }

        protected virtual void OnUpdate() { }

        protected virtual void OnLateUpdate() { }

        protected virtual void OnHide() { }

        protected virtual void OnDestroyMe() { }
    }
}