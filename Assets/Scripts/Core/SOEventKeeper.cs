using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOEventKeeper : MonoBehaviour
{
    public static SOEventKeeper Instance;

    [SerializeField] private List<SOEventWithKeyForUI> eventList;
    private Dictionary<string, SOEvent> eventDictionary;

    private void Awake() {
        Instance = this;

        eventDictionary = new Dictionary<string, SOEvent>();

        foreach(var e in eventList)
        {
            eventDictionary.Add(e.key, e.sOEvent);
        }
    }

    public SOEvent GetEvent(string key)
    {
        if(eventDictionary.ContainsKey(key))
        {
            return eventDictionary[key];
        }
        Debug.LogError($"Event with key {key} not found");
        return null;
    }
}

[Serializable]
public class SOEventWithKeyForUI
{
    public string key;
    public SOEvent sOEvent;
}