using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NeverSayNever
{
    public interface IFSMMgr
    {

        void Update(float deltaTime);

        IFSM Add(int id);

        void Remove(int id);

        void Clear();
    }
}


