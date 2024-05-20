using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    public float speed;

    public static AsteroidSpawner Instance
    {
        get
        {
            return _Instance;
        }
    }

    private static AsteroidSpawner _Instance;

    public Asteroid asteroidPrefab;

    public ObjectPool<Asteroid> pool;

    private void Start()
    {
        _Instance = this;
        pool = new ObjectPool<Asteroid>(AsteroidFactory, Asteroid.TurnOn, Asteroid.TurnOff, 6, true);
    }
    Asteroid AsteroidFactory()
    {
        return Instantiate(asteroidPrefab);
    }

    public void ReturnAsteroid(Asteroid a)
    {
        pool.ReturnObject(a);
    }

    public void InstanceParticlesPO(PlatformObstacle o)
    {
        PartycleAsteroidExplosionSpawner.Instance.pool.GetObject().transform.position = o.transform.position;
    }

    public void InstanceParticlesBH(BlackHole b)
    {
        PartycleAsteroidExplosionSpawner.Instance.pool.GetObject().transform.position = b.transform.position;
    }

    public void InstanceParticles(Asteroid a)
    {
        PartycleAsteroidExplosionSpawner.Instance.pool.GetObject().transform.position = a.transform.position;
    }
}
