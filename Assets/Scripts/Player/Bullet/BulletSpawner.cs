using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner Instance
    {
        get
        {
            return _Instance;
        }
    }

    private static BulletSpawner _Instance;

    public Bullet bulletPrefab;

    public ObjectPool<Bullet> pool;

    private void Start()
    {
        _Instance = this;
        pool = new ObjectPool<Bullet>(BulletFactory, Bullet.TurnOn, Bullet.TurnOff, 15, true);
    }

    public Bullet BulletFactory()
    {
        return Instantiate(bulletPrefab);
    }

    public void ReturnPlatform(Bullet p)
    {
        pool.ReturnObject(p);
    }
    

}

