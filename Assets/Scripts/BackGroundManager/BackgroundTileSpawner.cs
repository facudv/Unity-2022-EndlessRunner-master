using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTileSpawner : MonoBehaviour
{
    public static BackgroundTileSpawner Instance
    {
        get
        {
            return _Instance;
        }
    }

    private static BackgroundTileSpawner _Instance;

    public BackgroundTile platformPrefab;

    public ObjectPool<BackgroundTile> pool;

    private void Start()
    {
        _Instance = this;
        pool = new ObjectPool<BackgroundTile>(BackgroundFactory, BackgroundTile.TurnOn, BackgroundTile.TurnOff, 4, true);
    }

    public BackgroundTile BackgroundFactory()
    {
        return Instantiate(platformPrefab);
    }

    public void ReturnBackgroundTile(BackgroundTile bt)
    {
        pool.ReturnObject(bt);
    }
}
