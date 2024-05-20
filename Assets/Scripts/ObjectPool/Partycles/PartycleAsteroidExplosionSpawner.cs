using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartycleAsteroidExplosionSpawner : MonoBehaviour
{
    public static PartycleAsteroidExplosionSpawner Instance
    {
        get
        {
            return _Instance;
        }
    }

    private static PartycleAsteroidExplosionSpawner _Instance;

    public PartycleAsteroidExplosion partycleBulletExplosionPrefab;

    public ObjectPool<PartycleAsteroidExplosion> pool;

    private void Start()
    {
        _Instance = this;
        pool = new ObjectPool<PartycleAsteroidExplosion>(PartycleBulletExplosionFactory, PartycleAsteroidExplosion.TurnOn, PartycleAsteroidExplosion.TurnOff, 10, true);
    }

    PartycleAsteroidExplosion PartycleBulletExplosionFactory()
    {
        return Instantiate(partycleBulletExplosionPrefab);
    }

    public void ReturnPartycleAsteroidExplosion(PartycleAsteroidExplosion p)
    {
        pool.ReturnObject(p);
    }
}
