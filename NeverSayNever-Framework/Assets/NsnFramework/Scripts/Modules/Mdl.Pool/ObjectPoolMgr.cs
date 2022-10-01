using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace NeverSayNever
{
    public class ObjectManager : IManager
    {
        private Transform _poolRootObject;
        private readonly Dictionary<string, object> _objectPools = new Dictionary<string, object>();
        private readonly Dictionary<string, NsnGoPool> _gameObjectPools = new Dictionary<string, NsnGoPool>();

        Transform PoolRoot
        {
            get
            {
                if (_poolRootObject == null)
                {
                    var objectPool = new GameObject("ObjectPoolRoot");
                    objectPool.transform.SetParent(Framework.BridgeObject.transform);
                    objectPool.transform.localScale = Vector3.one;
                    objectPool.transform.localPosition = Vector3.zero;
                    _poolRootObject = objectPool.transform;
                }
                return _poolRootObject;
            }
        }

        public void OnInitialize(params object[] args)
        {
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void OnDispose()
        {
            _objectPools?.Clear();
            _gameObjectPools.Clear();
        }

        public NsnGoPool CreatePool(string poolName, int initSize, int maxSize, GameObject prefab, bool selfGrowing = false)
        {
            var pool = new NsnGoPool(poolName, prefab, initSize, maxSize, PoolRoot, selfGrowing);
            _gameObjectPools[poolName] = pool;
            return pool;
        }

        public NsnGoPool GetPool(string poolName)
        {
            return _gameObjectPools.ContainsKey(poolName) ? _gameObjectPools[poolName] : null;
        }

        public GameObject GetObject(string poolName)
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

        public void ReleaseObj(GameObject gameObj)
        {
            if (gameObj == null)
            {
                return;
            }
            var poolObject = gameObj.GetComponent<NsnMonoPoolObj>();
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
        public NsnPool<T> CreatePool<T>(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease) where T : class
        {
            var type = typeof(T);
            var pool = new NsnPool<T>(actionOnGet, actionOnRelease);
            _objectPools[type.Name] = pool;
            return pool;
        }

        public NsnPool<T> GetPool<T>() where T : class
        {
            var type = typeof(T);
            NsnPool<T> pool = null;
            if (_objectPools.ContainsKey(type.Name))
            {
                pool = _objectPools[type.Name] as NsnPool<T>;
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