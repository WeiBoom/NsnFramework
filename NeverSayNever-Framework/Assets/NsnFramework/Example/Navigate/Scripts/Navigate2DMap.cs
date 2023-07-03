using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Nsn;
namespace Nsn.Example
{
    public class Navigate2DMap : MonoBehaviour
    {
        private NavigateAStar AStar;
        
        public Navigate2DMapGridItem GridItemPrefab;
        public Transform GridItemRoot;
        public bool IsShowGridHint;
        public bool IsStepOnByOne;
        public int GridMapSize;

        private Navigate2DMapGridItem[,] m_Map;
        private Int2 m_MapSize;
        private NavigateGridItemState m_SettingState;

        private Navigate2DMapGridItem m_StartGridItem;
        private Navigate2DMapGridItem m_TargetGridItem;

        private Dictionary<Int2, Navigate2DMapGridItem> m_ObstacleDic = new Dictionary<Int2, Navigate2DMapGridItem>();

        private IEnumerator m_AStarProcess;

        private void Awake()
        {
            if (GridItemPrefab == null || GridItemRoot == null)
            {
                Debug.LogError("先设置GridItemPrefab 和 GridItemRoot");
                return;
            }
            m_SettingState = NavigateGridItemState.Default;
            AStar = new NavigateAStar();
        
            InitMap();
        }

        private void InitMap()
        {
            if (m_Map != null) return;
            
            Int2 offset = new Int2(50 + GridMapSize / 2, 50 + GridMapSize / 2);
            m_MapSize = new Int2((Screen.width - 100) / GridMapSize, (Screen.height - 100) / GridMapSize);

            m_Map = new Navigate2DMapGridItem[m_MapSize.x, m_MapSize.y];
            Vector2 itemSize = new Vector2(GridMapSize, GridMapSize);
            for (int i = 0; i < m_MapSize.x; i++)
            {
                for (int j = 0; j < m_MapSize.x; j++)
                {
                    Navigate2DMapGridItem item = Instantiate(GridItemPrefab, GridItemRoot);
                    item.rectTransform.sizeDelta = itemSize;
                    item.rectTransform.anchoredPosition = new Vector2(GridMapSize * i + offset.x, GridMapSize * j + offset.y);
                    item.rectTransform.localScale = Vector3.one;
                    item.gameObject.SetActive(true);
                    item.Init(new Int2(i, j), IsShowGridHint, OnMapItemClicked);
                    m_Map[i, j] = item;
                }
            }
        }

        void OnMapItemClicked(Navigate2DMapGridItem item)
        {
            
        }
    }
}