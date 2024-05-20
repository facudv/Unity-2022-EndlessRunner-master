using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffSpawner : MonoBehaviour
{
    public static DebuffSpawner Instance
    {
        get
        {
            return _Instance;
        }
    }

    private static DebuffSpawner _Instance;

    public List<Debuff> DebuffList;

    public ObjectPool<Debuff> pool;

    private void Start()
    {
        _Instance = this;
        pool = new ObjectPool<Debuff>(DebuffFactory, Debuff.TurnOn, Debuff.TurnOff, 15, true);
    }

    public Debuff DebuffFactory()
    {
        int _randomNum = Random.Range(0, 2);

        return Instantiate(DebuffList[_randomNum]);
    }

    public void ReturnDebuff(Debuff d)
    {
        pool.ReturnObject(d);
    }


}
