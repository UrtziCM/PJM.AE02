using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class GameCleanup : MonoBehaviour, IEventListener<ShotData>, IEventListener<ShootableSpawnData>
{
    List<GameObject> MarkedForDestruction = new(16);
    Dictionary<int, ShootableTimePair> MarkedForBelatedDestruction = new(32);

    private const float timeToLive = 5f;


    private class ShootableTimePair
    {
        public GameObject shootable;
        public float time;
        internal ShootableTimePair(GameObject shootable, float time)
        {
            this.shootable = shootable;
            this.time = time;
        }
    }


    public void OnEvent(ShotData eventData)
    {
        if (eventData.shotObject.TryGetComponent(out Bomb bomb))
        {
            MarkedForDestruction.Add(bomb.gameObject);
            bomb.GetComponent<Renderer>().enabled = false;
        }
        else if (eventData.shotObject.TryGetComponent(out Bottle bottle))
        {
            MarkedForDestruction.Add(bottle.gameObject);
            bottle.GetComponent<Renderer>().enabled = false;
        }
        else if (eventData.shotObject.TryGetComponent(out Can can))
        {
            MarkedForBelatedDestruction[can.gameObject.GetInstanceID()] = new(can.gameObject, Time.realtimeSinceStartup + timeToLive);
        }
    }

    public void OnEvent(ShootableSpawnData eventData)
    {
        MarkedForBelatedDestruction.Add(eventData.shootable.GetInstanceID(), new(eventData.shootable, Time.realtimeSinceStartup + timeToLive));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBus.Subscribe<ShotData>(this);
        EventBus.Subscribe<ShootableSpawnData>(this);
        StartCoroutine(DestroyBatch());
        StartCoroutine(DestroyBatchBelated());
    }

    private IEnumerator DestroyBatch()
    {
        WaitForSeconds timer = new(5);

        while (isActiveAndEnabled)
        {
            yield return timer;
            foreach (var item in MarkedForDestruction)
                Destroy(item);
            MarkedForDestruction.Clear();
        }
    }

    private IEnumerator DestroyBatchBelated() // Otra hora perdida por aquí
    {
        WaitForSeconds timer = new(5);

        while (isActiveAndEnabled)
        {
            yield return timer;
            foreach (int item in MarkedForBelatedDestruction.Keys)
            {
                if (MarkedForBelatedDestruction.TryGetValue(item, out ShootableTimePair pair))
                {
                    if (pair.time < Time.realtimeSinceStartup)
                    {
                        Destroy(pair.shootable);
                    }
                }
            }
            int[] kc = MarkedForBelatedDestruction.Keys.ToArray();
            foreach (int key in kc)
            {
                try
                {
                    if (MarkedForBelatedDestruction.TryGetValue(key, out ShootableTimePair pair))
                    {
                        var _ = pair.shootable.transform;
                    }
                }
                catch
                {
                    MarkedForBelatedDestruction.Remove(key);
                }
            }
        }

    }



}
