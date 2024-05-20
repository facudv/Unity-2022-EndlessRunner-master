using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

public class OnHoldButton : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    public Model player;
    public bool canShoot;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        canShoot = !canShoot;
    }

    public void Update()
    {
        if (canShoot)
        player.Shoot(player.offsetBullet,player.transform.position);
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        canShoot = !canShoot;
    }
}
