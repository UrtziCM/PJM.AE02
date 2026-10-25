using UnityEngine;

public class LifeLostData : IEventData
{
    public int oldLifes;
    public int currentLifes;

    public LifeLostData(int oldLifes, int currentLifes)
    {
        this.oldLifes = oldLifes;
        this.currentLifes = currentLifes;

    }
}
