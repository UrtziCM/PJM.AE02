using UnityEngine;

public class ScreenTappedData : IEventData
{
    public Vector2 pos;

    public ScreenTappedData(Vector2 pos)
    {
        this.pos = pos;
    }
}