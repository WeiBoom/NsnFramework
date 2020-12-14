using UnityEngine;
using System.Collections;

namespace NeverSayNever.Core.HUD
{
    public enum EUIAnchorPresets
    {
        TopLeft,
        TopCenter,
        TopRight,

        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        BottomLeft,
        BottomCenter,
        BottomRight,

        HorStretchTop,
        HorStretchMiddle,
        HorStretchBottom,

        VerStretchLeft,
        VerStretchCenter,
        VerStretchRight,

        StretchAll,
    }

    public enum EUIPivotPresets
    {
        TopLeft,
        TopCenter,
        TopRight,

        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        BottomLeft,
        BottomCenter,
        BottomRight,
    }

    public static class UIAnchorTool
    {
        /// <summary>
        /// 设置锚点
        /// </summary>
        /// <param name="source"></param>
        /// <param name="anchor"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public static void SetAnchor(RectTransform source, EUIAnchorPresets anchor, int offsetX = 0, int offsetY = 0)
        {
            switch (anchor)
            {
                case (EUIAnchorPresets.TopLeft):
                    {
                        source.anchorMin = new Vector2(0, 1);
                        source.anchorMax = new Vector2(0, 1);
                        break;
                    }
                case (EUIAnchorPresets.TopCenter):
                    {
                        source.anchorMin = new Vector2(0.5f, 1);
                        source.anchorMax = new Vector2(0.5f, 1);
                        break;
                    }
                case (EUIAnchorPresets.TopRight):
                    {
                        source.anchorMin = new Vector2(1, 1);
                        source.anchorMax = new Vector2(1, 1);
                        break;
                    }

                case (EUIAnchorPresets.MiddleLeft):
                    {
                        source.anchorMin = new Vector2(0, 0.5f);
                        source.anchorMax = new Vector2(0, 0.5f);
                        break;
                    }
                case (EUIAnchorPresets.MiddleCenter):
                    {
                        source.anchorMin = new Vector2(0.5f, 0.5f);
                        source.anchorMax = new Vector2(0.5f, 0.5f);
                        break;
                    }
                case (EUIAnchorPresets.MiddleRight):
                    {
                        source.anchorMin = new Vector2(1, 0.5f);
                        source.anchorMax = new Vector2(1, 0.5f);
                        break;
                    }

                case (EUIAnchorPresets.BottomLeft):
                    {
                        source.anchorMin = new Vector2(0, 0);
                        source.anchorMax = new Vector2(0, 0);
                        break;
                    }
                case (EUIAnchorPresets.BottomCenter):
                    {
                        source.anchorMin = new Vector2(0.5f, 0);
                        source.anchorMax = new Vector2(0.5f, 0);
                        break;
                    }
                case (EUIAnchorPresets.BottomRight):
                    {
                        source.anchorMin = new Vector2(1, 0);
                        source.anchorMax = new Vector2(1, 0);
                        break;
                    }

                case (EUIAnchorPresets.HorStretchTop):
                    {
                        source.anchorMin = new Vector2(0, 1);
                        source.anchorMax = new Vector2(1, 1);
                        break;
                    }
                case (EUIAnchorPresets.HorStretchMiddle):
                    {
                        source.anchorMin = new Vector2(0, 0.5f);
                        source.anchorMax = new Vector2(1, 0.5f);
                        break;
                    }
                case (EUIAnchorPresets.HorStretchBottom):
                    {
                        source.anchorMin = new Vector2(0, 0);
                        source.anchorMax = new Vector2(1, 0);
                        break;
                    }

                case (EUIAnchorPresets.VerStretchLeft):
                    {
                        source.anchorMin = new Vector2(0, 0);
                        source.anchorMax = new Vector2(0, 1);
                        break;
                    }
                case (EUIAnchorPresets.VerStretchCenter):
                    {
                        source.anchorMin = new Vector2(0.5f, 0);
                        source.anchorMax = new Vector2(0.5f, 1);
                        break;
                    }
                case (EUIAnchorPresets.VerStretchRight):
                    {
                        source.anchorMin = new Vector2(1, 0);
                        source.anchorMax = new Vector2(1, 1);
                        break;
                    }

                case (EUIAnchorPresets.StretchAll):
                    {
                        source.anchorMin = new Vector2(0, 0);
                        source.anchorMax = new Vector2(1, 1);
                        break;
                    }
            }

            source.anchoredPosition = new Vector2(offsetX, offsetY);

        }

        /// <summary>
        /// 设置轴心
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pivot"></param>
        public static void SetPivot(RectTransform source, EUIPivotPresets pivot)
        {
            switch (pivot)
            {
                case EUIPivotPresets.TopLeft:
                    source.pivot = new Vector2(0, 1);
                    break;
                case EUIPivotPresets.TopCenter:
                    source.pivot = new Vector2(0.5f, 1);
                    break;
                case EUIPivotPresets.TopRight:
                    source.pivot = new Vector2(1, 1);
                    break;
                case EUIPivotPresets.MiddleLeft:
                    source.pivot = new Vector2(0, 0.5f);
                    break;
                case EUIPivotPresets.MiddleCenter:
                    source.pivot = new Vector2(0.5f, 0.5f);
                    break;
                case EUIPivotPresets.MiddleRight:
                    source.pivot = new Vector2(1, 0.5f);
                    break;
                case EUIPivotPresets.BottomLeft:
                    source.pivot = new Vector2(0, 0);
                    break;
                case EUIPivotPresets.BottomCenter:
                    source.pivot = new Vector2(0.5f, 0);
                    break;
                case EUIPivotPresets.BottomRight:
                    source.pivot = new Vector2(1, 0);
                    break;
            }
        }
    }
}