using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartycleExplosionEnemySpawner : MonoBehaviour
{
    public static PartycleExplosionEnemySpawner Instance
    {
        get
        {
            return _Instance;
        }
    }

    private static PartycleExplosionEnemySpawner _Instance;

    public PartycleExplosionEnemy partycleExplosionEnemyPrefab;

    public ObjectPool<PartycleExplosionEnemy> pool;

    private void Start()
    {
        _Instance = this;
        pool = new ObjectPool<PartycleExplosionEnemy>(PartycleExplosionEnemyFactory, PartycleExplosionEnemy.TurnOn, PartycleExplosionEnemy.TurnOff, 5, true);
    }

    PartycleExplosionEnemy PartycleExplosionEnemyFactory()
    {
        return Instantiate(partycleExplosionEnemyPrefab);
    }

    public void ReturnPartycleExplosionEnemy(PartycleExplosionEnemy a)
    {
        pool.ReturnObject(a);
    }

    public void InstanceParticles(PlatformObstacle p)
    {
        Instance.pool.GetObject().transform.position = p.transform.position;
    }

    public void InstanceParticlesEM(EnemyMove e)
    {
        Instance.pool.GetObject().transform.position = e.transform.position;
    }
}
