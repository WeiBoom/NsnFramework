using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nsn
{
    public interface IUIMgr : IManager
    {
        void Open(string view);

        void Close(string view);

        bool IsOpened(string view);
    }
}

