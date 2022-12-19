using System.Collections;
using UnityEngine;

namespace Nsn
{
    public interface IConfigMgr : IManager
    {

        void Load(string config);

        void LoadAsync(string config);

        T GetTable<T>();

        bool TryGetTable<T>(int key, out T table);

        bool TryGetTable<T>(string key, out T table);
    }
}