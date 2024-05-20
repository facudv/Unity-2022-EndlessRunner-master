using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyWeightPointer : MonoBehaviour
{
    public static FlyWeight flyweightState = new FlyWeight
    {
        player = FindObjectOfType<PlayerController>(),
    };

    public static readonly FlyWeight flywightState1 = new FlyWeight
    {
        timeToGenerateMoreTiles = 0f,
        spawnBuffOrDebuff = 10,
    };

    public static readonly FlyWeight flyWeightStateEnemy = new FlyWeight
    {
        offSetBulletEnemy = new Vector3(-9.5f, 0, 0),
    };

    public static readonly FlyWeight flyWightStateBullet = new FlyWeight
    {
        timerBullet = 1f,
    };

    public static readonly FlyWeight flyWightStatsPlayer = new FlyWeight
    {
        playerSpeed = 70f,
    };

    public static readonly FlyWeight flyWightBackgroundTile = new FlyWeight
    {
        offSetTileBackground = new Vector3(900, 0, 0),
    };
}
