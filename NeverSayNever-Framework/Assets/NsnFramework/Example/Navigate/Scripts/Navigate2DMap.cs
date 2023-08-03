using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Nsn;
namespace Nsn.Example
{
    // Demo 的地图生成类
    public class Navigate2DMap : MonoBehaviour
    {
        // 核心寻路逻辑
        private NavigateAStar AStar;
        // 地图Item 预制体
        public Navigate2DMapGridItem GridItemPrefab;
        public Transform GridItemRoot;
        
        public Button setPlayerButton;
        public Button setDestinationButton;
        public Button setObstacleButton;
        public Button resetMapButton;
        public Button aStarButton;
        public Text hintText;
        private Text SetObstacleButtonText;
        public NavigateEvaluationType navigateEvaluationType;
        public bool IsShowGridHint;
        public bool IsStepOnByOne;
        public int GridMapSize;
        // 地图网格
        private Navigate2DMapGridItem[,] m_Map;
        // 地图大小
        private NavigateInt2 m_MapSize;
        private NavigateGridItemState m_SettingState;
        // 玩家所处的位置
        private Navigate2DMapGridItem m_PlayerItem;
        // 目标点的位置
        private Navigate2DMapGridItem m_DestinationItem;
        // 障碍Item
        private Dictionary<NavigateInt2, Navigate2DMapGridItem> m_ObstacleDic = new Dictionary<NavigateInt2, Navigate2DMapGridItem>();

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
            
            InitEvents();
            InitMap();
        }

        private void OnDestroy()
        {
            ClearMap();
        }

        private void InitEvents()
        {
            setPlayerButton.onClick.AddListener(OnSetPlayerButtonClicked);
            setDestinationButton.onClick.AddListener(OnSetDestinationButtonClicked);
            setObstacleButton.onClick.AddListener(OnSetObstacleButtonClicked);
            resetMapButton.onClick.AddListener(OnResetMapButtonClicked);
            aStarButton.onClick.AddListener(OnAStarButtonClicked);
        }

        private void InitMap()
        {
            if (m_Map != null) return;
            
            NavigateInt2 offset = new NavigateInt2(50 + GridMapSize / 2, 50 + GridMapSize / 2);
            m_MapSize = new NavigateInt2((Screen.width - 100) / GridMapSize, (Screen.height - 100) / GridMapSize);

            m_Map = new Navigate2DMapGridItem[m_MapSize.x, m_MapSize.y];
            Vector2 itemSize = new Vector2(GridMapSize, GridMapSize);
            for (int i = 0; i < m_MapSize.x; i++)
            {
                for (int j = 0; j < m_MapSize.y; j++)
                {
                    Navigate2DMapGridItem item = Instantiate(GridItemPrefab, GridItemRoot);
                    item.rectTransform.sizeDelta = itemSize;
                    item.rectTransform.anchoredPosition = new Vector2(GridMapSize * i + offset.x, GridMapSize * j + offset.y);
                    item.rectTransform.localScale = Vector3.one;
                    item.gameObject.SetActive(true);
                    item.Init(new NavigateInt2(i, j), IsShowGridHint, OnMapItemClicked);
                    m_Map[i, j] = item;
                }
            }
        }

        private void ResetMap()
        {
            if(AStar.Initialized) {
                AStar.Clear();
                return;
            }

            if(m_PlayerItem != null)
                m_PlayerItem.ItemState = NavigateGridItemState.Default;//  = NavigateGridItemState.Default;
            m_PlayerItem = null;

            if(m_DestinationItem != null)
                m_DestinationItem.ItemState = NavigateGridItemState.Default;
            m_DestinationItem = null;

            foreach(var grid in m_ObstacleDic.Values)
                grid.ItemState = NavigateGridItemState.Default;
            m_ObstacleDic.Clear();
        }
        
        private void ClearMap()
        {
            if (m_Map == null) return;
            for(int i = 0; i < m_MapSize.x; i++) {
                for(int j = 0; j < m_MapSize.y; j++) {
                    m_Map[i, j].Clear();
                    m_Map[i, j] = null;
                }
            }
            m_Map = null;
        }


        void OnMapItemClicked(Navigate2DMapGridItem item)
        {
            switch (m_SettingState)
            {
                case NavigateGridItemState.Player:
                {
                    //SetTargetItemState(m_PlayerItem, item, NavigateGridItemState.Player);
                    if (m_PlayerItem != null)
                        m_PlayerItem.ItemState = NavigateGridItemState.Default;
                    m_PlayerItem = item;
                    item.ItemState = NavigateGridItemState.Player;

                    SetHintInfo();
                    m_SettingState = NavigateGridItemState.Default;
                    break;
                }
                case NavigateGridItemState.Destination:
                {
                   // SetTargetItemState(m_DestinationItem, item, NavigateGridItemState.Destination);
                    if (m_DestinationItem != null)
                        m_DestinationItem.ItemState = NavigateGridItemState.Default;
                    m_DestinationItem = item;
                    item.ItemState = NavigateGridItemState.Destination;

                    SetHintInfo();
                    m_SettingState = NavigateGridItemState.Default;
                    break;
                }
                case NavigateGridItemState.Obstacle:
                {
                    if (item.ItemState == NavigateGridItemState.Default)
                    {
                        item.ItemState = NavigateGridItemState.Obstacle;
                        m_ObstacleDic[item.Position] = item;
                    }
                    else if (item.ItemState == NavigateGridItemState.Obstacle)
                    {
                        item.ItemState = NavigateGridItemState.Default;
                        m_ObstacleDic.Remove(item.Position);
                    }
                    break;
                }
            }
        }
        
        private void SetTargetItemState(Navigate2DMapGridItem target, Navigate2DMapGridItem item,NavigateGridItemState state)
        {
            item.ItemState = state;
            if (target != null)
                target.ItemState = NavigateGridItemState.Default;
            target = item;
            SetHintInfo();
            m_SettingState = NavigateGridItemState.Default;
        }

        private void SetHintInfo(string hint = null)
        {
            hintText.text = hint;
        }
        
        
        void OnSetPlayerButtonClicked() {
            if(m_SettingState == NavigateGridItemState.Obstacle)
                return;
            m_SettingState = NavigateGridItemState.Player;
            SetHintInfo("请设置玩家位置");
        }

        void OnSetDestinationButtonClicked() {
            if(m_SettingState == NavigateGridItemState.Obstacle)
                return;
            m_SettingState = NavigateGridItemState.Destination;
            SetHintInfo("请设置目的地位置");
        }

        void OnSetObstacleButtonClicked() {
            if(SetObstacleButtonText == null)
                SetObstacleButtonText = setObstacleButton.GetComponentInChildren<Text>();

            if(m_SettingState == NavigateGridItemState.Obstacle) {
                m_SettingState = NavigateGridItemState.Default;
                SetObstacleButtonText.text = "设置障碍";
                SetHintInfo();
            } else {
                m_SettingState = NavigateGridItemState.Obstacle;
                SetObstacleButtonText.text = "完成障碍设置";
                SetHintInfo("请设置障碍物");
            }
        }

        void OnResetMapButtonClicked() {
            if(m_SettingState == NavigateGridItemState.Obstacle)
                return;
            ResetMap();
        }
        
        //开始寻路，分一步一步寻路和一次性完成寻路
        void OnAStarButtonClicked() {
            if(!AStar.Initialized) {
                AStar.Init(m_Map, m_MapSize, m_PlayerItem.Position,m_DestinationItem.Position, navigateEvaluationType);
                m_AStarProcess = AStar.Execute();
            }
            if(IsStepOnByOne) {
                if(!m_AStarProcess.MoveNext()) {
                    SetHintInfo("寻路完成");
                }
            }
            else {
                while(m_AStarProcess.MoveNext())
                {
                }
                SetHintInfo("寻路完成");
            }
        }
    }
}