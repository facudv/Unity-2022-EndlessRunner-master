using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    void Update()
    {
        transform.position += -transform.forward * FlyWeightPointer.flyWightStatsPlayer.playerSpeed * Time.deltaTime;
    }
}
