using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocaleMaster : MonoBehaviour
{
    public static LocaleMaster Instance { get; private set; }

    [SerializeField] private List<LocaleStringDataForUnityUI> strings = new List<LocaleStringDataForUnityUI>();

    private Dictionary<string, LocaleString> localeData = new Dictionary<string, LocaleString>();

    public GameLocale currentLocale;

    private void Awake()
    {
        Instance = this;

        foreach(var d in strings)
        { 
            localeData.Add(d.key, d.data);
        }
    }

    public string GetString(string key)
    {
        if(localeData.ContainsKey(key))
        {
            return localeData[key].GetValue();
        }
        else
        {
            Debug.LogError($"LocaleMaster does not have string with a key: '{key}'");
            return string.Empty;
        }
    }
}

public enum GameLocale
{
    Russian,
    English
}

[Serializable]
public class LocaleStringDataForUnityUI
{
    public string key;
    public LocaleString data;
}