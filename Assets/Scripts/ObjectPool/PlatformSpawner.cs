using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    private int _offSetPlatform = 10;
    private int _actualIndex = 1;
    private int _num = 1;
    private int _nextRowPlatforms;

    public static PlatformSpawner Instance
    {
        get
        {
            return _Instance;
        }
    }

    private static PlatformSpawner _Instance;

    public Platform platformPrefab;

    public ObjectPool<Platform> pool;

    private void Start()
    {
        _Instance = this;
        pool = new ObjectPool<Platform>(PlatformFactory, Platform.TurnOn, Platform.TurnOff, 10, true);
    }

    public Platform PlatformFactory()
    {
        var _instancePlatform = Instantiate(platformPrefab);

        return _instancePlatform;
    }

    public void ReturnPlatform(Platform p)
    {
        pool.ReturnObject(p);
    }
}




