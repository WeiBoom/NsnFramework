using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nsn
{
    public class UIBase : MonoBehaviour
    {
        private void Awake() => OnAwake();

        private void Start() => OnStart();

        private void OnEnable() => OnShow();

        private void FixedUpdate() => OnFixedUpdate();

        private void Update() => OnUpdate();

        private void LateUpdate() => OnLateUpdate();

        private void OnDisable() => OnUIHide();

        private void OnDestroy() => OnUIDestroy();

        protected virtual void OnAwake() { }

        protected virtual void OnStart() { }

        protected virtual void OnShow() { }

        protected virtual void OnFixedUpdate() { }

        protected virtual void OnUpdate() { }

        protected virtual void OnLateUpdate() { }

        protected virtual void OnUIHide() { }

        protected virtual void OnUIDestroy() { }
    }
}
