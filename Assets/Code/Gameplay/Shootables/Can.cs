using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Can : MonoBehaviour, IShootable, IScorable
{
    private Rigidbody _rigidbody;
    [SerializeField]
    private float bounceStrength = 10;
    private const byte SCORE = 50;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Score()
    {
        EventBus.Call(new ScoreData(SCORE, gameObject));
    }

    public void Shoot()
    {
        EventBus.Call(new ShotData(gameObject));
        Score();
        Bounce();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Bounce()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.AddForce(Vector3.up * bounceStrength, ForceMode.VelocityChange);
        _rigidbody.AddTorque(Random.onUnitSphere);
    }
}
