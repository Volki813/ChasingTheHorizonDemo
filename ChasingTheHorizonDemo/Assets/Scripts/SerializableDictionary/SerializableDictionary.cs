using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    public List<TKey> keys = new();
    public List<TValue> values = new();
    public void OnAfterDeserialize()
    {
        SynchronizeToSerializedData();
    }

    public void OnBeforeSerialize() { }

    public void SynchronizeToSerializedData()
    {
        Clear();

        // build the dictionary if valid data, otherwise re-initialize keys and values
        if (keys != null && values != null)
        {
            int numElements = Mathf.Min(keys.Count, values.Count);
            for (int i = 0; i < numElements; ++i)
            {
                this[keys[i]] = values[i];
            }
        }
        else
        {
            keys = new();
            values = new();
        }

        // rebuild if keys and values are out of sync
        if(keys.Count != values.Count)
        {
            keys = new(keys);
            values = new(values);
        }
    }

#if UNITY_EDITOR
    public void EditorOnlyAdd(TKey key, TValue value)
    {
        keys.Add(key);
        values.Add(value);
    }
#endif
}
