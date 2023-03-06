using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nsn
{
    public class SerializableDictionary
    {
    }

    [Serializable]
    public class SerializableDictionary<TKey, TValue> : SerializableDictionary, IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [Serializable]
        public struct SerializableKeyValuePair
        {
            public TKey Key;
            public TValue Value;

            public SerializableKeyValuePair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public void SetValue(TValue value)
            {
                Value = value;
            }
        }

        [SerializeField]
        private List<SerializableKeyValuePair> m_List = new List<SerializableKeyValuePair>();

        private Lazy<Dictionary<TKey, uint>> m_KeyPositionsDic;
        private Dictionary<TKey, uint> KeyPositions => m_KeyPositionsDic.Value;

        public SerializableDictionary()
        {
            ResetKeyPositions();
        }

        public SerializableDictionary(IDictionary<TKey,TValue> dictionary)
        {
            ResetKeyPositions();
            if (dictionary != null)
                throw new ArgumentException("Failed! dictionary is null");

            // copy value
            foreach (var pair in dictionary)
            {
            }
        }

        private Dictionary<TKey, uint> MakeKeyPositions()
        {
            int numEntries = m_List.Count;
            Dictionary<TKey, uint> result = new Dictionary<TKey, uint>(numEntries);

            for (int i = 0; i < numEntries; ++i)
                result[m_List[i].Key] = (uint)i;

            return result;
        }

        private void ResetKeyPositions() => m_KeyPositionsDic = new Lazy<Dictionary<TKey, uint>>(MakeKeyPositions);


        #region IDictionary<TKey, TValue>

        public TValue this[TKey key]
        {
            get => m_List[(int)KeyPositions[key]].Value;
            set
            {
                if(KeyPositions.TryGetValue(key,out uint posIndex))
                {
                    m_List[(int)posIndex].SetValue(value);
                }
                else
                {
                    KeyPositions[key] = (uint)m_List.Count;
                    m_List.Add(new SerializableKeyValuePair(key, value));
                }
            }
        }

        public ICollection<TKey> Keys => m_List.Select(tuple => tuple.Key).ToArray();

        public ICollection<TValue> Values => m_List.Select(tuple => tuple.Value).ToArray();

        public int Count => m_List.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            if (KeyPositions.ContainsKey(key))
            {
                throw new ArgumentException("An element with the same key already exists in the dictionary.");
            }
            else
            {
                // these can replace by "this[key] = value;"

                KeyPositions[key] = (uint)m_List.Count;
                m_List.Add(new SerializableKeyValuePair(key, value));
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        public void Clear()
        {
            m_List.Clear();
            KeyPositions.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return KeyPositions.ContainsKey(item.Key);
        }

        public bool ContainsKey(TKey key)
        {
            return KeyPositions.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            int numKeys = m_List.Count;
            if (array.Length - arrayIndex < numKeys)
                throw new ArgumentException("arrayIndex");

            for (int i = 0; i < numKeys; i++, arrayIndex++)
            {
                var entry = m_List[i];
                array[arrayIndex] = new KeyValuePair<TKey, TValue>(entry.Key, entry.Value);
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return m_List.Select(ToKeyValuePair).GetEnumerator();

            KeyValuePair<TKey, TValue> ToKeyValuePair(SerializableKeyValuePair skvp)
            {
                return new KeyValuePair<TKey, TValue>(skvp.Key, skvp.Value);
            }
        }

        public bool Remove(TKey key)
        {
            if (KeyPositions.TryGetValue(key, out uint index))
            {
                Dictionary<TKey, uint> kp = KeyPositions;

                kp.Remove(key);
                m_List.RemoveAt((int)index);

                int numEntries = m_List.Count;
                for (uint i = index; i < numEntries; i++)
                {
                    kp[m_List[(int)i].Key] = i;
                }
                return true;
            }
            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if(KeyPositions.TryGetValue(key, out uint index))
            {
                value = m_List[(int)index].Value;
                return true;
            }
            value = default;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region ISerializationCallbackReceiver

        public void OnAfterDeserialize()
        {
            ResetKeyPositions();
        }

        public void OnBeforeSerialize()
        {
        }

        #endregion
    }
}

