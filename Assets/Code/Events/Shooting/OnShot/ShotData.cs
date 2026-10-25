using UnityEngine;

public class ShotData : IEventData
{
    public GameObject shotObject;

    public ShotData(GameObject gameObject)
    {
        shotObject = gameObject;
    }

}
