using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

interface IEventAction
{

}
class EventAction<T> : IEventAction
{
    public UnityAction<T> action;
    public EventAction(UnityAction<T> Action) 
    {
        action += Action;
    }
}
class EventAction : IEventAction
{
    public UnityAction action;
    public EventAction(UnityAction Action)
    {
        action += Action;
    }
}

public class EventCenter : SingleBase<EventCenter>
{
    private Dictionary<string, IEventAction> EventDic = new Dictionary<string, IEventAction>();
    public void AddEventListener<T>(string name,UnityAction<T> action)
    {
        if (EventDic.ContainsKey(name))
        {
            (EventDic[name] as EventAction<T>).action += action;
        }
        else
        {
            EventDic.Add(name, new EventAction<T>(action));
        }
    }
    public void AddEventListener(string name, UnityAction action)
    {
        if (EventDic.ContainsKey(name))
        {
            (EventDic[name] as EventAction).action += action;
        }
        else
        {
            EventDic.Add(name, new EventAction(action));
        }
    }
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (EventDic.ContainsKey(name))
        {
            (EventDic[name] as EventAction<T>).action -= action;
        }
    }
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (EventDic.ContainsKey(name))
        {
            (EventDic[name] as EventAction).action -= action;
        }
    }
    public void EventTrigger<T>(string name,T info)
    {
        if (EventDic.ContainsKey(name))
        {
            if ((EventDic[name] as EventAction<T>).action != null)
                (EventDic[name] as EventAction<T>).action(info);
        }
    }
    public void EventTrigger(string name)
    {
        if (EventDic.ContainsKey(name))
        {
            if ((EventDic[name] as EventAction).action != null)
                (EventDic[name] as EventAction).action();
        }
    }
    public void Clear()
    {
        EventDic.Clear();
    }
}
