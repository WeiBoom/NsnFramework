using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.Example
{
    using NeverSayNever;

    public static class SceneMgr
    {
        public static bool IsLoadingScene { get; private set; } = false;

        private static IEnumerator Coroutine_LoadSceneAsync(string sceneName, System.Action<AsyncOperation> loadingAction, System.Action loadComplete)
        {
            var operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;


            IUIMgr uimgr = GameCore.Instance.GetManager<IUIMgr>();
            // 打开加载界面
            uimgr.OpenPanel(UIModuleGroup.GameLoading.ModuleName, operation,loadingAction, loadComplete);

            yield return null;
            IsLoadingScene = false;
        }

        /// <summary>
        /// 异步加载场景，只允许单个场景加载
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="loadingAction"></param>
        /// <param name="loadComplete"></param>
        public static void LoadSceneAsync(string sceneName, System.Action<AsyncOperation> loadingAction = null, System.Action loadComplete = null)
        {
            if (IsLoadingScene) return;
            IsLoadingScene = true;

            //var loadingMessenger = UIMgr.Instance.GetUIMessenger<GameLoadingMessenger>();
            //if (loadingMessenger != null)
            //{
            //    loadingMessenger.loadingAction += loadingAction;
            //    loadingMessenger.loadCompleteAction += loadComplete;
            //}

            if (Framework.LoadType == EAssetLoadType.AssetBundle)
            {
                ResourceMgr.LoadScene(sceneName, obj => {
                    Debug.Log("场景加载完成");
                    // 协程加载场景
                    CoroutineMgr.Instance.AddCoroutine(Coroutine_LoadSceneAsync(sceneName, loadingAction, loadComplete));
                });
            }
            else
            {
                // 协程加载场景
                CoroutineMgr.Instance.AddCoroutine(Coroutine_LoadSceneAsync(sceneName, loadingAction, loadComplete));
            }

          
        }
    }
}
