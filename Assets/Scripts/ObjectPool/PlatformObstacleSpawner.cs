using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformObstacleSpawner : MonoBehaviour
{

    public bool isLevelThree;

    public static PlatformObstacleSpawner Instance
    {
        get
        {
            return _Instance;
        }
    }

    private static PlatformObstacleSpawner _Instance;

    public List<PlatformObstacle> platformPrefab;

    public ObjectPool<PlatformObstacle> pool;

    private void Start()
    {
        _Instance = this;
        pool = new ObjectPool<PlatformObstacle>(PlatformFactory, PlatformObstacle.TurnOn, PlatformObstacle.TurnOff, 15, true);
    }

    public PlatformObstacle PlatformFactory()
    {
        int _randomNum = Random.Range(1, 4);
        return Instantiate(platformPrefab[_randomNum - 1]);
    }

    public void ReturnPlatform(PlatformObstacle p)
    {
        pool.ReturnObject(p);
    }

    public void InstanceParticles(PlatformObstacle p)
    {
        PartycleAsteroidExplosionSpawner.Instance.pool.GetObject().transform.position = p.transform.position;
    }

}

