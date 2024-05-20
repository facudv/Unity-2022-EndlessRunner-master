using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartycleExplosionEnemy : MonoBehaviour
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
            PartycleExplosionEnemySpawner.Instance.ReturnPartycleExplosionEnemy(this);
            _timer = timer;
        }
    }

    public static void TurnOn(PartycleExplosionEnemy p)
    {
        p.Reset();
        p.gameObject.SetActive(true);
    }

    public static void TurnOff(PartycleExplosionEnemy p)
    {
        p.gameObject.SetActive(false);
    }
}
