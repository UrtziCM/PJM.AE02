using System;
using System.Collections.Generic;
using System.Linq;

public static class EventBus
{
    private static readonly Dictionary<Type, List<object>> _listeners = new(); 

    public static void Subscribe<T>(object listener) where T : IEventData
    {
        if (!_listeners.TryGetValue(typeof(T), out var list))
        {
            list = new List<object>();
            _listeners[typeof(T)] = list;
        }
        if (!list.Contains(listener))
        {
            list.Add(listener);
        }
    }

    public static void Unsubscribe<T>(object listener) where T : IEventData
    {
        if (_listeners.TryGetValue(typeof(T), out var list))
        {
            list.Remove(listener);
        }
    }

    public static void Call<T>(T eventData) where T : IEventData
    {
        if (!_listeners.TryGetValue(typeof(T), out var list)) return;

        var snapshot = list.ToArray(); // Por si se añaden durante una iteracion
        foreach (var listener in snapshot)
        {
            if (listener is IEventListener<T> typedListener)
            {
                typedListener.OnEvent(eventData);
            }
        }
    }


}
