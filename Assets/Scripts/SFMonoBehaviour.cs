using System;
using Nekki.Events;
using UnityEngine;

public class SFMonoBehaviour<T> : MonoBehaviour, IEventDispatcher<T>
{
    EventDispatcher<T> _eventDispatcher = new EventDispatcher<T>();

    public int AddEventListener(int name, Action<T> dlg)
    {
        return _eventDispatcher.AddEventListener(name, dlg);
    }

    public int CallEvent(int name, T arg)
    {
        return _eventDispatcher.CallEvent(name, arg);
    }

    public int RemoveAllEventListener()
    {
        return _eventDispatcher.RemoveAllEventListener();
    }

    public int RemoveEvent(int name)
    {
        return _eventDispatcher.RemoveEvent(name);
    }

    public int RemoveEventListener(int name, Action<T> dlg)
    {
        return _eventDispatcher.RemoveEventListener(name, dlg);
    }
}