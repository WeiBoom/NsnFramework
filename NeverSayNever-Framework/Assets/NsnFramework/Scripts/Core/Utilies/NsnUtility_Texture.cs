namespace Nsn
{
    using System;
    using System.IO;
    using Unity.Collections;
    using UnityEngine;

    /* Texture 相关 */
    public static partial class NsnUtility
    {
        private enum eImageType
        {
            Jpg,
            Png,
            Tga
        };
       
        /// <summary>
        /// 压缩Texture 
        /// </summary>
        /// <param name="source">需要压缩的texture</param>
        /// <param name="compress"> 压缩百分比 分子</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns></returns>
        public static byte[] CompressTexture(Texture2D source, int compress, out int width, out int height)
        {
            Debug.Assert(source);
            int targetWidth = Mathf.CeilToInt(source.width * compress * 0.01f);
            int targetHeight = Mathf.CeilToInt(source.height * compress * 0.01f);
            Texture2D result = CompressTextureBySize(source, targetWidth, targetHeight, true);
            width = result.width;
            height = result.height;
            return result.EncodeToJPG();
        }
        
        /// <summary>
        /// 根据Size压缩Texture
        /// </summary>
        /// <param name="source"></param>
        /// <param name="targetWidth"></param>
        /// <param name="targetHeight"></param>
        /// <param name="linear"></param>
        /// <returns></returns>
        public static Texture2D CompressTextureBySize(Texture2D source, int targetWidth, int targetHeight,
            bool linear = true)
        {
            var format = source.format;
            Debug.Assert(format == TextureFormat.RGBA32 || format == TextureFormat.RGB24 ||
                         format == TextureFormat.ARGB32);
            Debug.Assert(source);
            Texture2D result = CreateTexture2D(targetWidth, targetHeight, source.format, false, linear);
            result.wrapMode = source.wrapMode;
            var colorBuffer = result.GetRawTextureData<byte>();
            float incX = 1 / ((float)targetWidth);
            float incY = 1 / ((float)targetHeight);
            int px = 0;
            for (int b = 0; b < colorBuffer.Length;)
            {
                float temp = (float)source.height / targetHeight;
                var tempWidth = source.width / temp;
                temp = (targetWidth - tempWidth) / 2;
                temp = (float)px % targetWidth - temp;
                temp /= tempWidth;
                Vector2 uv = new Vector2(temp, incY * Mathf.Floor(px / targetWidth));

                Color32 col = source.GetPixelBilinear(uv.x, uv.y);
                if (format == TextureFormat.ARGB32)
                {
                    colorBuffer[b] = col.a;
                    colorBuffer[b + 1] = col.r;
                    colorBuffer[b + 2] = col.g;
                    colorBuffer[b + 3] = col.b;
                    b += 4;
                }
                else if (format == TextureFormat.RGBA32)
                {
                    colorBuffer[b] = col.r;
                    colorBuffer[b + 1] = col.g;
                    colorBuffer[b + 2] = col.b;
                    colorBuffer[b + 3] = col.a;
                    b += 4;
                }
                else if (format == TextureFormat.RGB24)
                {
                    colorBuffer[b] = col.r;
                    colorBuffer[b + 1] = col.g;
                    colorBuffer[b + 2] = col.b;
                    b += 3;
                }

                px++;
            }

            result.Apply();
            return result;
        }
        
        /// <summary>
        /// 创建一个Texture2D 对象
        /// </summary>
        public static Texture2D CreateTexture2D(int width, int height, TextureFormat format = TextureFormat.RGBA32,
            bool mip = false, bool linear = false)
        {
            Texture2D tex = new Texture2D(width, height, format, mip, linear);
            return tex;
        }
        
        /// <summary>
        /// 当前屏幕截屏
        /// </summary>
        /// <param name="cropRect"></param>
        /// <param name="isTransBg"></param>
        /// <param name="linear"></param>
        /// <returns></returns>
        public static Texture2D GetScreenTexture(Rect cropRect, bool isTransBg, bool linear = true)
        {
            TextureFormat format = TextureFormat.RGB24;
            if (isTransBg)
                format = TextureFormat.ARGB32;
            Texture2D screenShot = CreateTexture2D((int)cropRect.width, (int)cropRect.height, format, false, linear);
            screenShot.name = "ScreenShot";
            screenShot.ReadPixels(cropRect, 0, 0);
            screenShot.Apply();
            return screenShot;
        }

        
    }
}