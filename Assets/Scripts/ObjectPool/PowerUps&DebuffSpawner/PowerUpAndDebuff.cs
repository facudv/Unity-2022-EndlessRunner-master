using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAndDebuff : MonoBehaviour
{
    private void Reset()
    {

    }

    void Update()
    {

    }

    public static void TurnOn(PowerUpAndDebuff p)
    {
        //p.Reset();
        p.gameObject.SetActive(true);
    }

    public static void TurnOff(PowerUpAndDebuff p)
    {
        p.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        //devolveme al pool
        if (other.gameObject.tag == "restart")
        {
            PowerUpAndDebuffSpawner.Instance.ReturnPowerUpAndDebuff(this);
        }

    }
}
