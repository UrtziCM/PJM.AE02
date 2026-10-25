using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Bomb : MonoBehaviour, IShootable
{
    public void Shoot()
    {
        EventBus.Call(new ShotData(gameObject));
        EventBus.Call(new BombExplodeData(this));
    }
}
