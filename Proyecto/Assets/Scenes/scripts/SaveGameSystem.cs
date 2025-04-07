using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEditor;
using System;

public static class SaveGameSystem
{
    [System.Serializable]
    public class Data
    {
        public enum DataType
        {
            Int,
            Float,
            String,
            Bool
        }

        public DataType dataType;

        public int intData;
        public float floatData;
        public string stringData;
        public bool boolData;
    }

    static Dictionary<string, Data> savedData = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnRuntimeMethodLoad()
    {
        Load();
    }

    public static int GetInt(string key, int defaultValue = 0)
    {
        int retVal = defaultValue;
        if (savedData.ContainsKey(key))
        {
            // CheckDataIsInt();
            retVal = savedData[key].intData;
        }
        return retVal;
    }

    public static float GetFloat(string key, float defaultValue = 0f)
    {
        float retVal = defaultValue;
        if (savedData.ContainsKey(key))
        {
            // CheckDataIsInt();
            retVal = savedData[key].floatData;
        }
        return retVal;
    }

    public static string GetString(string key, string defaultValue = "")
    {
        string retVal = defaultValue;
        if (savedData.ContainsKey(key))
        {
            // CheckDataIsInt();
            retVal = savedData[key].stringData;
        }
        return retVal;
    }

    public static bool GetBool(string key, bool defaultValue = false)
    {
        bool retVal = defaultValue;
        if (savedData.ContainsKey(key))
        {
            // CheckDataIsInt();
            retVal = savedData[key].boolData;
        }
        return retVal;
    }



    public static void SetInt(string key, int value)
    {
        Data data;

        if (!savedData.TryGetValue(key, out data))
        {
            data = new Data();
            data.dataType = Data.DataType.Int;
            savedData.Add(key, data);
        }

        data.intData = value;
    }

    public static void SetFloat(string key, float value)
    {
        Data data;

        if (!savedData.TryGetValue(key, out data))
        {
            data = new Data();
            data.dataType = Data.DataType.Float;
            savedData.Add(key, data);
        }

        data.floatData = value;
    }
    public static void SetString(string key, string value)
    {
        Data data;

        if (!savedData.TryGetValue(key, out data))
        {
            data = new Data();
            data.dataType = Data.DataType.String;
            savedData.Add(key, data);
        }

        data.stringData = value;
    }
    public static void SetBool(string key, bool value)
    {
        Data data;

        if (!savedData.TryGetValue(key, out data))
        {
            data = new Data();
            data.dataType = Data.DataType.Bool;
            savedData.Add(key, data);
        }

        data.boolData = value;
    }

    [System.Serializable] class SerializableData
    {
        public List<string> keys = new();
        public List<Data> data = new();
    }

    public static void Save(string saveGameName = "SaveGame")
    {
        SerializableData serializableData = new();

        foreach (KeyValuePair<string, Data> item in savedData)
        {
            serializableData.keys.Add(item.Key);
            serializableData.data.Add(item.Value);
        }

        string stringToSave = JsonUtility.ToJson(serializableData);
        Debug.Log(stringToSave);

        PlayerPrefs.SetString(saveGameName, stringToSave);
        PlayerPrefs.Save();
    }

    public static void Load(string saveGameName = "SaveGame")
    {
        string stringToLoad = PlayerPrefs.GetString(saveGameName);

        SerializableData serializableData = new();
        JsonUtility.FromJsonOverwrite(stringToLoad, serializableData);

        savedData = new();
        for (int i = 0; i < serializableData.keys.Count; i++)
        {
            savedData.Add(
                serializableData.keys[i],
                serializableData.data[i]
                );
        }
    }

#if UNITY_EDITOR

    [MenuItem("SaveGameSystem/Save")]
    public static void SaveToPlayerPrefs()
    {
        SaveGameSystem.Save();
    }

    [MenuItem("SaveGameSystem/Load")]
    public static void LoadFromPlayerPrefs()
    {
        SaveGameSystem.Load();
    }

    static string testKey = "RandomInt";
    [MenuItem("SaveGameSystem/Save int to 5")]
    public static void SaveIntTo5()
    {
        SaveGameSystem.SetInt(testKey, 5);
    }

    [MenuItem("SaveGameSystem/Save int to 7")]
    public static void SaveIntTo7()
    {
        SaveGameSystem.SetInt(testKey, 7);
    }

    [MenuItem("SaveGameSystem/Debug log int")]
    public static void DebugLogInt()
    {
        Debug.Log(SaveGameSystem.GetInt(testKey));
    }
#endif

}
