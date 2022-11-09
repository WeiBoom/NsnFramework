using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NeverSayNever
{
    public interface IUIMdl : IModule
    {
        void OpenView(int viewID, params object[] userDatas);

        void CloseView(int viewID);

        void GetView(int viewID);

        void CloseAll();
    }

}
