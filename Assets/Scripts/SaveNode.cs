using PreviewLabs;
using System;
using UnityEngine;

public class SaveNode : MonoBehaviour
{
    public object data;

    public SaveNode()
    {
    }

    public bool GetBool(string inKey, bool inDefaultValue)
    {
        return PreviewLabs.PlayerPrefs.GetBool(inKey, inDefaultValue);
    }

    public float GetFloat(string inKey, float inDefaultValue)
    {
        return PreviewLabs.PlayerPrefs.GetFloat(inKey, inDefaultValue);
    }

    public int GetInt(string inKey, int inDefaultValue)
    {
        return PreviewLabs.PlayerPrefs.GetInt(inKey, inDefaultValue);
    }

    public string GetString(string inKey, string inDefaultValue)
    {
        return PreviewLabs.PlayerPrefs.GetString(inKey, inDefaultValue);
    }

    public bool HasKey(string inKey)
    {
        return PreviewLabs.PlayerPrefs.HasKey(inKey);
    }

    public virtual void Load(SaveNode.DataType inDataType)
    {
    }

    public virtual void PostLoad()
    {
    }

    public virtual void Reset()
    {
    }

    public virtual void Save()
    {
    }

    public enum DataType
    {
        New,
        Old
    }
}