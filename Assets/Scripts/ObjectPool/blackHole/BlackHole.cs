using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private void Reset()
    {

    }

    public static void TurnOn(BlackHole b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(BlackHole b)
    {
        b.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "restart")
        {
            BlackHoleSpawner.Instance.ReturnBlackHole(this);
        }
    }
}
