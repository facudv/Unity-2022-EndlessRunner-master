using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PartycleBullet : MonoBehaviour
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
            PartycleBulletSpawner.Instance.ReturnPartycleBullet(this);
            _timer = timer;
        }
    }

    public static void TurnOn(PartycleBullet p)
    {
        p.Reset();
        p.gameObject.SetActive(true);
    }

    public static void TurnOff(PartycleBullet p)
    {
        p.gameObject.SetActive(false);
    }
}
