using Nsn;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JsonConvert = Unity.Plastic.Newtonsoft.Json.JsonConvert;

namespace Nsn
{
    public class ConfigMgr : IConfigMgr
    {
        private Dictionary<string, string> _configs;


        public void OnInitialized(params object[] args)
        {
            _configs = new Dictionary<string, string>(100);
        }

        public void OnDisposed() 
        { 
            _configs.Clear();
        }

        public void OnUpdate(float deltaTime)
        {
        }


        public void Load(string config)
        {
           
        }

        public void LoadAsync(string config)
        {
        }


        public T GetTable<T>()
        {
            return default(T);
        }

        public bool TryGetTable<T>(int key, out T table)
        {
            table = default(T);
            return false;
        }

        public bool TryGetTable<T>(string key, out T table)
        {
            table = default(T);
            return false;
        }





        private void OnLoadCompleteds(byte[] bytes)
        {
            JsonConvert.DeserializeObject(bytes.ToString());
        }
    }
}