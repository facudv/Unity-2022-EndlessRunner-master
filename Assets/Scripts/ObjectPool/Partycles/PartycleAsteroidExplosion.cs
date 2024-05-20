using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartycleAsteroidExplosion : MonoBehaviour
{
    private float timer = 1f;
    private float _timer;
    public ParticleSystem ps;

    private void Reset()
    {
        ps.Play();
        _timer = timer;
    }

    public void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            PartycleAsteroidExplosionSpawner.Instance.ReturnPartycleAsteroidExplosion(this);
            _timer = timer;
        }
    }

    public static void TurnOn(PartycleAsteroidExplosion p)
    {
        p.Reset();
        p.gameObject.SetActive(true);
    }

    public static void TurnOff(PartycleAsteroidExplosion p)
    {
        p.gameObject.SetActive(false);
    }
}
