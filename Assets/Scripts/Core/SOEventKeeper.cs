using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SOEventKeeper : MonoBehaviour
{
    public static SOEventKeeper Instance;

    [SerializeField] private List<SOEventWithKeyForUI> eventList;
    private Dictionary<string, SOEvent> eventDictionary;

    private Queue<SOEvent> eventQueue;

    private void Awake() {
        Instance = this;

        GetAndLoadAllSOEvents();
        eventDictionary = new Dictionary<string, SOEvent>();
        eventQueue = new Queue<SOEvent>();

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

    /// <summary>
    /// Adds an event without argument to the event queue
    /// </summary>
    /// <param name="key">Name of event with prefix "on". Example - "onStartBattle"</param>
    public void AddEventToQueue(string key)
    {
        SOEvent newEventInQueue = GetEvent(key);
        if(newEventInQueue != null)
        {
            eventQueue.Enqueue(newEventInQueue);
        }

        Debug.LogWarning($"Event with key {key} was not added in queue");
    }

    private void GetAndLoadAllSOEvents()
    {
        Debug.Log("=============");
        Debug.Log("Auto event finding");
        Debug.Log("=============");
        string[] guids = AssetDatabase.FindAssets("t:"+typeof(SOEvent).Name);
        for(int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            var obj = (SOEvent)AssetDatabase.LoadAssetAtPath(path, typeof(SOEvent));
            eventList.Add(new SOEventWithKeyForUI() {key = "on" + obj.name, sOEvent = obj});
            Debug.Log($"{path} : File name {obj.name}");
        }
        Debug.Log("=============");
    }

    private void Update()
    {
        ProcessEventQueue();
    }

    private void ProcessEventQueue()
    {
        if (eventQueue.Count > 0)
        {
            foreach (var ev in eventQueue)
            {
                ev.Raise();
            }

            CleanEventQueue();
        }
    }

    private void CleanEventQueue()
    {
        eventQueue.Clear();
    }
}

[Serializable]
public class SOEventWithKeyForUI
{
    public string key;
    public SOEvent sOEvent;
}