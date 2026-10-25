using UnityEngine;

public class BombExplodeData : IEventData
{
    public Bomb bomb;

    public BombExplodeData(Bomb bomb)
    {
        this.bomb = bomb;
    }
}
