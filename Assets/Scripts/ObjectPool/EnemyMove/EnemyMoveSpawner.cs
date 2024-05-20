using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSpawner : MonoBehaviour
{
    public static EnemyMoveSpawner Instance
    {
        get
        {
            return _Instance;
        }
    }

    private static EnemyMoveSpawner _Instance;

    public EnemyMove enemyMovePrefab;

    public ObjectPool<EnemyMove> pool;

    private void Start()
    {
        _Instance = this;
        pool = new ObjectPool<EnemyMove>(EnemyMoveFactory, EnemyMove.TurnOn, EnemyMove.TurnOff, 7, true);
    }

    public EnemyMove EnemyMoveFactory()
    {
        return Instantiate(enemyMovePrefab);
    }

    public void ReturnEnemyMove(EnemyMove e)
    {
        pool.ReturnObject(e);
    }

    //public void InstanceParticles(BlackHole b)
    //{
    //    PartycleAsteroidExplosionSpawner.Instance.pool.GetObject().transform.position = p.transform.position;
    //}
}
