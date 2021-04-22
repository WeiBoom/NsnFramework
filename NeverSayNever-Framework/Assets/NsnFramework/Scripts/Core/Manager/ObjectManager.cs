using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using NeverSayNever.Utilities;
using NeverSayNever.Core.ObjectPool;

namespace NeverSayNever.Core
{
    public class ObjectManager : Singleton<ObjectManager>
    {
        private Transform _poolRootObject;
        private readonly Dictionary<string, object> _objectPools = new Dictionary<string, object>();
        private readonly Dictionary<string, UGameObjectPool> _gameObjectPools = new Dictionary<string, UGameObjectPool>();

        Transform PoolRootObject
        {
            get
            {
                if (_poolRootObject == null)
                {
                    var objectPool = new GameObject("ObjectPool");
                    objectPool.transform.SetParent(Framework.BridgeObject.transform);
                    objectPool.transform.localScale = Vector3.one;
                    objectPool.transform.localPosition = Vector3.zero;
                    _poolRootObject = objectPool.transform;
                }
                return _poolRootObject;
            }
        }

        public override void OnInitialize(params object[] args)
        {
            base.OnInitialize(args);
        }

        public UGameObjectPool CreatePool(string poolName, int initSize, int maxSize, GameObject prefab, bool selfGrowing = false)
        {
            var pool = new UGameObjectPool(poolName, prefab, initSize, maxSize, PoolRootObject, selfGrowing);
            _gameObjectPools[poolName] = pool;
            return pool;
        }

        public UGameObjectPool GetPool(string poolName)
        {
            return _gameObjectPools.ContainsKey(poolName) ? _gameObjectPools[poolName] : null;
        }

        public GameObject Get(string poolName)
        {
            GameObject result = null;
            if (_gameObjectPools.ContainsKey(poolName))
            {
                var pool = _gameObjectPools[poolName];
                var poolObj = pool.NextAvailableObject();
                if (poolObj == null)
                {
                    Debug.LogWarning("No object available in pool. Consider setting fixedSize to false.: " + poolName);
                }
                else
                {
                    result = poolObj.gameObject;
                }
            }
            else
            {
                Debug.LogError("Invalid pool name specified: " + poolName);
            }
            return result;
        }

        public void Release(GameObject gameObj)
        {
            if (gameObj == null)
            {
                return;
            }
            var poolObject = gameObj.GetComponent<PoolObject>();
            if (poolObject == null) return;
            var poolName = poolObject.poolName;
            if (_gameObjectPools.ContainsKey(poolName))
            {
                var pool = _gameObjectPools[poolName];
                pool.ReturnObjectToPool(poolObject);
            }
            else
            {
                Debug.LogWarning("No pool available with name: " + poolName);
            }
        }

        ///-----------------------------------------------------------------------------------------------
        public UObjectPool<T> CreatePool<T>(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease) where T : class
        {
            var type = typeof(T);
            var pool = new UObjectPool<T>(actionOnGet, actionOnRelease);
            _objectPools[type.Name] = pool;
            return pool;
        }

        public UObjectPool<T> GetPool<T>() where T : class
        {
            var type = typeof(T);
            UObjectPool<T> pool = null;
            if (_objectPools.ContainsKey(type.Name))
            {
                pool = _objectPools[type.Name] as UObjectPool<T>;
            }
            return pool;
        }

        public bool Exist<T>()
        {
            var type = typeof(T);
            return _objectPools.ContainsKey(type.Name);
        }

        public T Get<T>() where T : class
        {
            var pool = GetPool<T>();
            return pool?.Get();
        }

        public void Release<T>(T obj) where T : class
        {
            var pool = GetPool<T>();
            pool?.Release(obj);
        }
    }
}