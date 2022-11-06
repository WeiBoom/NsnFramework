using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{

    public class UIViewInfo
    {
        // ����������Ϣ
        public UIConfigInfo Config;
        // Ԥ���������Ϣ
        public UIPanelInfo Panel;
        // UI�����㼶
        public EPanelLayer Layer;

        // ˢ�»ص�
        private System.Action<UIViewInfo> _prepareCallback;
        // �򿪽���ʱ��Ӧ�Ĳ���
        private System.Object[] _userDatas;
        // UI�����Ƿ��Ѿ��������
        private bool _isAssetLoaded = false;

        private bool _isPrepared = false;

        internal void TryInvoke(System.Action<UIViewInfo> prepareCallback, System.Object[] userDatas)
        {
            _userDatas = userDatas;
            if (_isPrepared)
                prepareCallback?.Invoke(this);
            else
                _prepareCallback = prepareCallback;
        }
    }

    public class UIMdl : IUIMdl
    {
        private readonly List<UIViewInfo> _viewStack;

        private IResMdl mResMdl;

        public void OnCreate(params object[] args)
        {
            mResMdl = Framework.GetModule<IResMdl>();
            if (mResMdl == null)
                throw new System.Exception("IResMdl is Null , it need create before UIMdl Create");
        }

        public void OnDispose()
        {
            _viewStack.Clear();
            mResMdl = null;
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void OpenView(int viewID, params object[] userDatas)
        {
            UIViewInfo viewInfo = GetView(viewID);
            if(viewInfo != null)
            {
                Pop(viewInfo);  // ��ջ�е���
                Push(viewInfo); // ����ѹջ

                viewInfo.TryInvoke(OnUIViewPrepare, userDatas);
            }
            else
            {
                viewInfo = CreateInstance(viewID);
            }
        }

        public void CloseView(int viewID)
        {
        }
        public void CloseAll()
        {
        }

        private void Push(UIViewInfo info) { }

        private void Pop(UIViewInfo info) { }

        private bool IsContains(int viewID)
        {
            for(int i = 0; i <_viewStack.Count;i++)
            {
                UIViewInfo info = _viewStack[i];
                if(info.Config.ID == viewID)
                    return true;
            }
            return false;
        }

        private UIViewInfo GetView(int viewID)
        {
            for (int i = 0; i < _viewStack.Count; i++)
            {
                UIViewInfo info = _viewStack[i];
                if (info.Config.ID == viewID)
                    return info;
            }
            return null;
        }

        private UIViewInfo CreateInstance(int viewID)
        {
            UIViewInfo info = null;



            return info;
        }

        private void OnUIViewPrepare(UIViewInfo view)
        {
           
        }

        void IUIMdl.GetView(int viewID)
        {
            throw new System.NotImplementedException();
        }
    }
}

