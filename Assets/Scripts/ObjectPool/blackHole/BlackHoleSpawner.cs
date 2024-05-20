using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSpawner : MonoBehaviour
{
    public static BlackHoleSpawner Instance
    {
        get
        {
            return _Instance;
        }
    }

    private static BlackHoleSpawner _Instance;

    public BlackHole blackHolePrefab;

    public ObjectPool<BlackHole> pool;

    private void Start()
    {
        _Instance = this;
        pool = new ObjectPool<BlackHole>(BlackHoleFactory, BlackHole.TurnOn, BlackHole.TurnOff, 10, true);
    }

    public BlackHole BlackHoleFactory()
    {
        return Instantiate(blackHolePrefab);
    }

    public void ReturnBlackHole(BlackHole b)
    {
        pool.ReturnObject(b);
    }

    //public void InstanceParticles(BlackHole b)
    //{
    //    PartycleAsteroidExplosionSpawner.Instance.pool.GetObject().transform.position = p.transform.position;
    //}
}
