using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public static void TurnOn(Platform p)
    {
        //p.Reset();
        p.gameObject.SetActive(true);
    }

    public static void TurnOff(Platform p)
    {
        p.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        //para de hacer
        if (other.gameObject.tag == "cctile")
        {
            FlyWeightPointer.flywightState1.timeToGenerateMoreTiles = 1f;
        }

        //devolveme al pool
        if (other.gameObject.tag == "restart")
        {
            PlatformSpawner.Instance.ReturnPlatform(this);
        }
    }
}
