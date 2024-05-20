using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartycleBulletSpawner : MonoBehaviour
{
    public static PartycleBulletSpawner Instance
    {
        get
        {
            return _Instance;
        }
    }

    private static PartycleBulletSpawner _Instance;

    public PartycleBullet partycleBulletPrefab;

    public ObjectPool<PartycleBullet> pool;

    private void Start()
    {
        _Instance = this;
        pool = new ObjectPool<PartycleBullet>(PartycleBulletFactory, PartycleBullet.TurnOn, PartycleBullet.TurnOff, 10, true);
    }

    PartycleBullet PartycleBulletFactory()
    {
        return Instantiate(partycleBulletPrefab);
    }

    public void ReturnPartycleBullet(PartycleBullet p)
    {
        pool.ReturnObject(p);
    }
}
