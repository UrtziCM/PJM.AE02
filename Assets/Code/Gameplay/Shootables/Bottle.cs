using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Bottle : MonoBehaviour, IScorable, IShootable
{
    private const byte SCORE = 100; // ahorramos 24 bits por instancia :)
    public void Score()
    {
        EventBus.Call(new ScoreData(SCORE, gameObject));
    }

    public void Shoot()
    {
        EventBus.Call(new ShotData(gameObject));
        Score();
    }

}
