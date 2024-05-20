using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float timerShoot;
    private float _timerShoot;


    // Start is called before the first frame update
    void Awake()
    {
        _timerShoot = timerShoot;
    }

    // Update is called once per frame
    void Update()
    {
        TimerShoot();
    }

    void TimerShoot()
    {
        if (_timerShoot > 0)
        {
            _timerShoot -= Time.deltaTime;
        }
        else
            Shoot();
    }

    public virtual void Shoot()
    {
        var bullet = BulletSpawner.Instance.pool.GetObject();
        var changeColorBullet = bullet.GetComponentInChildren<ParticleSystem>();
        changeColorBullet.startColor = Color.red;
        bullet.transform.position = this.transform.position + FlyWeightPointer.flyWeightStateEnemy.offSetBulletEnemy;
        bullet.ChangeColor();
        bullet.speed = -bullet.speed;
        _timerShoot = timerShoot;
    }

    //protected abstract void Move();
    
}
