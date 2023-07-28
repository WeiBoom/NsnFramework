using System;
using UnityEngine;
using UnityEngine.UI;

namespace  Nsn.Example
{
    public enum NavigateGridItemState {
        Default,
        /// <summary>
        /// 玩家所在位置(当前位置)
        /// </summary>
        Player,
        /// <summary>
        /// 障碍
        /// </summary>
        Obstacle,
        /// <summary>
        /// 目标点
        /// </summary>
        Destination,
        /// <summary>
        /// 路过
        /// </summary>
        Path,
        /// <summary>
        /// 在open列中 
        /// </summary>
        InOpen,
        /// <summary>
        /// 在close列中
        /// </summary>
        InClose
    }
    
    public class Navigate2DMapGridItem : MonoBehaviour
    {
        private static Color[] GRID_ITEM_COLORS = new Color[7]
        {
            Color.white, Color.green, Color.gray, Color.red, Color.yellow, new Color(0, 0.5f, 1), new Color(0, 1, 1)
        };

        public Image image;
        public Button button;

        [Header("A.STAR")]
        public Text gTxt;
        public Text hTxt;
        public Text fTxt;
        public GameObject arrow;

        private RectTransform m_RectTrans;
        public RectTransform rectTransform => m_RectTrans;

        private NavigateInt2 m_Pos;
        private NavigateInt2 Position => m_Pos;

        private Action<Navigate2DMapGridItem> m_OnClickCallback;
        private bool m_IsCanShowHint;
        
        private NavigateGridItemState _mItemItemState;
        public NavigateGridItemState ItemState
        {
            get => _mItemItemState;
            set
            {
                _mItemItemState = value;
                image.color = GRID_ITEM_COLORS[(int)_mItemItemState];
            }
        }
            
        private void Awake()
        {
            m_RectTrans = GetComponent<RectTransform>();
        }

        // 初始化Item，设置Pos，是否显示AStar信息等
        public void Init(NavigateInt2 pos, bool isShowHint, Action<Navigate2DMapGridItem> callback)
        {
            m_Pos = pos;
            m_IsCanShowHint = isShowHint;
            m_OnClickCallback = callback;
            ItemState = NavigateGridItemState.Default;
            button.onClick.AddListener(() =>
            {
                m_OnClickCallback?.Invoke(this);
            });
        }
        
        // 刷新AStar查询的信息
        public void RefreshAStarHint(int g, int h, int f, Vector2 forward) {
            if(ItemState == NavigateGridItemState.Default || ItemState == NavigateGridItemState.InOpen) {
                ItemState = NavigateGridItemState.InOpen;
                if(m_IsCanShowHint) {
                    gTxt.text = $"G:\n{g.ToString()}";
                    hTxt.text = $"H:\n{h.ToString()}";
                    fTxt.text = $"F:\n{f.ToString()}";
                    arrow.SetActive(true);
                    arrow.transform.up = -forward;
                }
            }
        }
        
        // 设置为Close状态
        public void ChangeInOpenStateToInClose() {
            if(ItemState == NavigateGridItemState.InOpen)
                ItemState = NavigateGridItemState.InClose;
        }
        
        // 设置为Path状态
        public void ChangeToPathState() {
            if(ItemState == NavigateGridItemState.InOpen || ItemState == NavigateGridItemState.InClose)
                ItemState = NavigateGridItemState.Path;
        }

        // 清理AStar的路径信息
        public void ClearAStarHint() {
            gTxt.text = "";
            hTxt.text = "";
            fTxt.text = "";
            arrow.SetActive(false);

            if(ItemState == NavigateGridItemState.InOpen || ItemState == NavigateGridItemState.InClose || ItemState == NavigateGridItemState.Path)
                ItemState = NavigateGridItemState.Default;
        }
        
        public void Clear()
        {
            Destroy(this.gameObject);
        }
    }

}