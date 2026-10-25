using System.Collections;
using UnityEngine;

public class ShootableSpawnManager : MonoBehaviour , IEventListener<GameStartData>
{
    [SerializeField]
    private float timeBetweenSpawns;
    [SerializeField]
    private float padding;
    [SerializeField]
    private float leftLimit;
    [SerializeField]
    private float rightLimit;
    [SerializeField]
    private float shootableSpawnForce;

    [SerializeField]
    private float spawnRelativeHeight;
    [SerializeField]
    private float depthInScene;

    private Vector3 leftBounds => Vector3.right * padding;
    private Vector3 rightBounds => Vector3.left * padding;
    private Vector3 LeftMostLimit => Camera.main.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMin, transform.position.y, transform.position.z + depthInScene)) + leftBounds + (Vector3.up * spawnRelativeHeight);
    private Vector3 RightMostLimit => Camera.main.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMax, transform.position.y, transform.position.z + depthInScene)) + rightBounds + (Vector3.up * spawnRelativeHeight);
    private int spawnCount = 0;

    private void Awake()
    {
        EventBus.Subscribe<GameStartData>(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(LeftMostLimit, .125f );
        Gizmos.DrawSphere(RightMostLimit, .125f);
        Gizmos.DrawLine(LeftMostLimit, RightMostLimit);
    }

    private IEnumerator SpawnTimer()
    {
        WaitForSeconds wfs = new(timeBetweenSpawns);
        while (isActiveAndEnabled)
        {
            yield return wfs;
            spawnCount++; // Todo este caos estaba chulo en teoría, en práctica son demasiados objetos :(
            #region Spawn chaos
            //if (spawnCount % 7 == 0)
            //    if (Random.value > .5f)
            //        SpawnBombInCenter();
            //    else
            //        SpawnRandomInCenter();
            //if (spawnCount % 3 == 0 && spawnCount % 21 != 0)
            //    SpawnSafeInCenter();
            //if (Random.value > .5f)
            //    SpawnRandom();
            //else
            #endregion

            EventBus.Call(new ShootableSpawnData(SpawnRandom()));
        }
    }

    private void SpawnBombInCenter()
    {
        GameObject bomb = ShootableFactory.CreateBomb();
        SetupShootableCenter(bomb);
    }
    private void SpawnRandomInCenter()
    {
        GameObject random = ShootableFactory.CreateRandom();
        SetupShootableCenter(random);
    }

    private void SpawnSafeInCenter()
    {
        GameObject safe = ShootableFactory.CreateSafe();
        SetupShootableCenter(safe);
    }
    private GameObject SpawnRandom()
    {
        GameObject random = ShootableFactory.CreateRandom();
        SetupShootableRandom(random);
        return random;
    }
    private void SpawnSafe()
    {
        GameObject safe = ShootableFactory.CreateSafe();
        SetupShootableRandom(safe);
    }
    private void SetupShootableCenter(GameObject shootable)
    {
        Vector3 spawnPos = transform.position + Vector3.up * spawnRelativeHeight;
        Rigidbody rigidbody = shootable.GetComponent<Rigidbody>();
        rigidbody.Move(spawnPos, Quaternion.identity);
        rigidbody.AddForce(Vector3.up * shootableSpawnForce);
    }

    private void SetupShootableRandom(GameObject shootable)
    {

        Vector3 possibleSpawnVector = (RightMostLimit - LeftMostLimit) * Random.value;//transform.worldToLocalMatrix * (RightMostLimit - LeftMostLimit);
        Vector3 spawnPos = LeftMostLimit + possibleSpawnVector;
        Rigidbody rigidbody = shootable.GetComponent<Rigidbody>();

        Vector3 screenCenterInWorld = Camera.main.ScreenToWorldPoint(Screen.safeArea.center);
        Vector3 fromSpawnToCenter = screenCenterInWorld - spawnPos;
        fromSpawnToCenter.z = 0; // Aplanamos el vector porque solo queremos le factor X para dirigirlo hacia la zona central
        fromSpawnToCenter.y = 0;

        rigidbody.Move(spawnPos, Quaternion.identity);
        rigidbody.AddForce(Vector3.up * shootableSpawnForce + fromSpawnToCenter, ForceMode.VelocityChange);
        rigidbody.AddTorque(Random.onUnitCircle * 20);
    }

    public void OnEvent(GameStartData eventData)
    {
        Debug.Log("started spawning");
        StartCoroutine(SpawnTimer());
    }
}
