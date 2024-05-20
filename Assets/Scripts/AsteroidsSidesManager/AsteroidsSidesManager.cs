using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSidesManager : MonoBehaviour
{
    private float _timer;
    private Vector3 spawnPos;
    Model mdl;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 5;
        mdl = FindObjectOfType<Model>();
    }

    // Update is called once per frame
    void Update()
    {
        TimerToGenerateAsteroids();
        GenerateSidesAstroids();
    }

    public void TimerToGenerateAsteroids()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
    }

    public void GenerateSidesAstroids()
    {
        if (_timer <= 0)
        {
            var randomNum = Random.Range(0, 3);
            RandomPositionSpawn(randomNum);
            var instanceAsteroid = AsteroidSpawner.Instance.pool.GetObject();
            instanceAsteroid.transform.position = spawnPos;
            _timer = 2;
        }
    }

    public void RandomPositionSpawn(int randomNum)
    {
        if (mdl.actualRail == 0)
        {
            if (randomNum == 0)
            {
                spawnPos = transform.position + new Vector3(200, 0, 0);
            }
            if (randomNum == 1)
            {
                spawnPos = transform.position + new Vector3(200, 0, 10);
            }
            if (randomNum == 2)
            {
                spawnPos = transform.position + new Vector3(200, 0, -10);
            }
        }
        else spawnPos = transform.position + new Vector3(200, 0, 0);
    }
}
