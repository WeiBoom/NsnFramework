namespace Nsn
{
    using UnityEngine;
    
    /*
     * Camera 相关
     */
    public static partial class NsnUtility
    {
        private static Camera s_MainCamera;
        public static Camera MainCamera
        {
            get
            {
                if (s_MainCamera == null)
                    s_MainCamera = Camera.main;
                return s_MainCamera;
            }
        }
        
        /// <summary>
        /// 用一个新相机去处理截图
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="cropRect"></param>
        /// <param name="onComplete"></param>
        /// <param name="waitTime"></param>
        public static void CaptureTextureWithNewCamera(Camera camera, Rect cropRect, System.Action<Texture2D> onComplete, float waitTime = 0)
        {
            if (camera == MainCamera)
            {
                NsnLog.Error("CaptureTextureWithNewCamera Only use by New Camera!");
                return;
            }
            if (camera != null)
                CoroutineMgr.Instance.ExecuteCoroutine(Wait2CaptureNewCameraTexture(camera, cropRect, onComplete,
                    waitTime));
        }
        
        private static System.Collections.IEnumerator Wait2CaptureNewCameraTexture(Camera camera, Rect cropRect,
            System.Action<Texture2D> onComplete, float waitTime)
        {
            if (waitTime > 0f)
            {
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                var waitFrame = new WaitForEndOfFrame();
                yield return waitFrame;
                yield return waitFrame; //必须等下一帧
            }

            var screenSize = GetScreenSize();
            var prev = RenderTexture.active;
            RenderTexture renderTexture = RenderTexture.GetTemporary(screenSize.x, screenSize.y, 0, UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm);
            camera.targetTexture = renderTexture;
            camera.Render();
            RenderTexture.active = renderTexture;
            Texture2D texture = GetScreenTexture(cropRect, true);
            texture.wrapMode = TextureWrapMode.Clamp;
            RenderTexture.active = prev;
            RenderTexture.ReleaseTemporary(renderTexture);
            //回调
            onComplete?.Invoke(texture);
        }

        public static Vector2Int GetScreenSize()
        {
#if UNITY_EDITOR
            // 直接通过反射获取
            System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
            System.Reflection.MethodInfo getGameViewSize_Method = T.GetMethod("GetSizeOfMainGameView",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            System.Object Res = getGameViewSize_Method.Invoke(null, null);
            Vector2 vec2 = (Vector2)Res;
            return new Vector2Int(Mathf.RoundToInt(vec2.x), Mathf.RoundToInt(vec2.y));
#else
            return new Vector2Int(Screen.width, Screen.height);
#endif
        }
    }
}