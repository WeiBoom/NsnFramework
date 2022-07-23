using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NeverSayNever
{
    public interface IFSMMgr : IManager
    {

        IFSM Add(int id);

        void Remove(int id);

        void Clear();
    }
}


