using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnObjectPool : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * FlyWeightPointer.flyWightStatsPlayer.playerSpeed * Time.deltaTime;
    }
}
