using UnityEngine;

public class ShootableSpawnData : IEventData
{
    public GameObject shootable;
    public ShootableSpawnData(GameObject shootable)
    {
        this.shootable = shootable;
    }
}
