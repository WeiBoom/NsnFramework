using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nsn
{
    public interface IUIMgr : IManager
    {
        Camera UICamera2D { get; }
        
        Vector2 DesignResolution { get; }

        void Register(int viewID);

        void Open(int viewID, params object[] args);

        void Close(int viewID);

        bool IsOpened(int viewID);
    }
}

