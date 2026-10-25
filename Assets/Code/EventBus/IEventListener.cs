using UnityEngine;

public interface IEventListener<T> where T : IEventData
{
    void OnEvent(T eventData);
}
