using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalCannon : MonoBehaviour
{
    public OnHoldButton buttonShoot;
    public Model player;
    public Vector3 offset;

    //// Update is called once per frame
    //void Update()
    //{
    //    if (buttonShoot.canShoot)
    //    {
    //        Shoot();
    //    }
    //}

    //void Shoot()
    //{
    //    if (player.canShoot)
    //    {
    //        if (player._timerShoot <= 0)
    //        {
    //            var bullet = BulletSpawner.Instance.pool.GetObject();
    //            bullet.transform.position = this.transform.position + offset;
    //            player._timerShoot = player.timerShoot;
    //        }
    //    }
    //}
}
