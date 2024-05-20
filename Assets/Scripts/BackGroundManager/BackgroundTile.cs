using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    public static void TurnOn(BackgroundTile p)
    {
        p.gameObject.SetActive(true);
    }

    public static void TurnOff(BackgroundTile p)
    {
        p.gameObject.SetActive(false);
    }
}
