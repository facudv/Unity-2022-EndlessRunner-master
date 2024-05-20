using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAndDebuffSpawner : MonoBehaviour
{
    public static PowerUpAndDebuffSpawner Instance
    {
        get
        {
            return _Instance;
        }
    }

    private static PowerUpAndDebuffSpawner _Instance;

    public List<PowerUpAndDebuff> powerUpAndDebuffList;

    public ObjectPool<PowerUpAndDebuff> pool;

    private void Start()
    {
        _Instance = this;
        pool = new ObjectPool<PowerUpAndDebuff>(PowerUpAndDebuffFactory, PowerUpAndDebuff.TurnOn, PowerUpAndDebuff.TurnOff, 15, true);
    }

    public PowerUpAndDebuff PowerUpAndDebuffFactory()
    {
        //var _instanceBuffOrDebuff = Instantiate(platformPrefab);

        int _randomNum = Random.Range(0, 4);

        return Instantiate(powerUpAndDebuffList[_randomNum]);
    }

    public void ReturnPowerUpAndDebuff(PowerUpAndDebuff p)
    {
        pool.ReturnObject(p);
    }


}
