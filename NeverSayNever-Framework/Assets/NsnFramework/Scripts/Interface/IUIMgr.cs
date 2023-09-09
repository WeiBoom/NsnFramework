using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nsn
{
    public interface IUIMgr : IManager
    {
        Camera UICamera2D { get; }
        
        Vector2 DesignResolution { get; }
        
        void Open(string viewName, params object[] args);

        void Close(string viewName);

        bool IsOpened(string viewName);
    }
}

