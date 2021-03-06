//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NeverSayNever.Example
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using NeverSayNever.Core.HUD;
    
    
    public class GameLoadingMessenger : NeverSayNever.Core.HUD.UIPanelMessenger
    {
        // 场景加载进度
        public AsyncOperation operation;
        // 加载完成后的行为
        public event System.Action loadCompleteAction;
        // 加载中的行为
        public event System.Action<AsyncOperation> loadingAction;

        private List<System.Action<AsyncOperation>> loadingActionList = new List<Action<AsyncOperation>>();
        private List<System.Action> CompleteActionList = new List<Action>();

        public GameLoadingMessenger(string name) : 
                base(name)
        {
        }

        public override bool OnPreOpen(params object[] args)
        {
            if(args.Length > 0 && args[0] != null)
            {
                operation = args[0] as AsyncOperation;
            }
            if (args.Length > 1 && args[1] != null)
            {
                loadingActionList.Add(args[1] as System.Action<AsyncOperation>);
            }
            if (args.Length > 2 && args[2] != null)
            {
                CompleteActionList.Add(args[2] as System.Action);
            }

            foreach (var act in loadingActionList)
                loadingAction += act;
            foreach (var act in CompleteActionList)
                loadCompleteAction += act;

            return true;
        }

        public void OnLoading()
        {
            loadingAction?.Invoke(operation);
        }

        public void OnLoadComplete()
        {
            loadCompleteAction?.Invoke();
            operation.allowSceneActivation = true;
            operation = null;
            foreach (var act in loadingActionList)
                loadingAction -= act;
            foreach (var act in CompleteActionList)
                loadCompleteAction -= act;

            loadingActionList.Clear();
            CompleteActionList.Clear();

            NeverSayNever.Core.UIManager.Instance.ClosePanel(UIModuleDefine.GameLoading);
        }


    }
}
