using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializedDictionary<T1, T2>
{
    public SerializedDictionary()
    {
        _pairs = new();
    }

    [SerializeField]
    private List<SerializedDictionaryObject<T1, T2>> _pairs;

    public SerializedDictionaryObject<T1, T2> GetPairByKey(T1 key)
    {
        return _pairs.Find(pair => pair.Key.Equals(key));
    }

    public SerializedDictionaryObject<T1, T2> GetPairByValue(T2 value)
    {
        return _pairs.Find(pair => pair.Value.Equals(value));
    }

    public T1 GetKey(T2 value)
    {
        SerializedDictionaryObject<T1, T2> pair =
            _pairs.Find(pair => pair.Value.Equals(value));
        return pair.Key;
    }

    public T2 GetValue(T1 key)
    {
        SerializedDictionaryObject<T1, T2> pair =
            _pairs.Find(pair => pair.Key.Equals(key));
        return pair.Value;
    }

    public void Clear()
    {
        _pairs.Clear();
    }

    public void Add(T1 key, T2 value)
    {
        _pairs.Add(new(key, value));
    }

    [Serializable]
    public class SerializedDictionaryObject<T3, T4>
    {
        public SerializedDictionaryObject(T3 key, T4 value)
        {
            Key = key;
            Value = value;
        }

        [field: SerializeField]
        public T3 Key { get; private set; }

        [field: SerializeField]
        public T4 Value { get; private set; }
    }
}
