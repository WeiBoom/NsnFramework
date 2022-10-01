using UnityEngine;
using System.Collections.Generic;

namespace NeverSayNever
{
    public class NsnGoPool
    {
        private int _maxSize;
        private int _poolSize;
        private readonly string _poolName;
        private readonly Transform _poolRoot;
        private readonly GameObject _poolObjectPrefab;
        private readonly bool _selfGrowing;
        private readonly Stack<NsnMonoPoolObj> _availableObjStack = new Stack<NsnMonoPoolObj>();

        public NsnGoPool(string poolName, GameObject poolObjectPrefab, int initCount, int maxSize, Transform pool, bool selfGrowing = false)
        {
            _poolName = poolName;
            _poolSize = initCount;
            _maxSize = maxSize;
            _poolRoot = pool;
            _poolObjectPrefab = poolObjectPrefab;
            _selfGrowing = selfGrowing;

            for (int index = 0; index < initCount; index++)
            {
                AddObjectToPool(NewObjectInstance());
            }
        }

        //add to pool
        private void AddObjectToPool(NsnMonoPoolObj po)
        {
            po.name = _poolName;
            po.gameObject.SetActive(false);
            _availableObjStack.Push(po);
            po.isPooled = true;
            po.transform.SetParent(_poolRoot, false);
        }

        private NsnMonoPoolObj NewObjectInstance()
        {
            var go = Object.Instantiate(_poolObjectPrefab);
            go.name = _poolName;
            var po = go.GetComponent<NsnMonoPoolObj>();
            if (po == null)
            {
                po = go.AddComponent<NsnMonoPoolObj>();
            }
            po.poolName = _poolName;
            return po;
        }

        public NsnMonoPoolObj NextAvailableObject()
        {
            NsnMonoPoolObj po = null;
            if (_availableObjStack.Count > 0)
            {
                po = _availableObjStack.Pop();
            }
            else if (_poolSize < _maxSize)
            {
                _poolSize++;
                po = NewObjectInstance();
                Debug.Log($"Growing pool {_poolName}. New size: {_poolSize}");
            }
            else if (_selfGrowing)
            {
                _poolSize++;
                _maxSize++;
                po = NewObjectInstance();
                Debug.LogWarning($"Growing pool {_poolName}. New size: {_poolSize}");
            }
            else
            {
                Debug.LogError("No object available & cannot grow pool: " + _poolName);
            }
            return po;
        }

        public void ReturnObjectToPool(NsnMonoPoolObj obj)
        {
            if (_poolName.Equals(obj.poolName))
            {
                AddObjectToPool(obj);
            }
            else
            {
                Debug.LogError($"Trying to add object to incorrect pool {_poolName} ");
            }
        }
    }
}