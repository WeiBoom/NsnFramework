using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NeverSayNever
{
    public interface IUIMdl : IModule
    {
        public void OpenView(int viewID, params object[] userDatas);

        public void CloseView(int viewID);

        public void GetView(int viewID);

        public void CloseAll();
    }

}
