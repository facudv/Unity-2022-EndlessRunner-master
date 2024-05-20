using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Asteroid : MonoBehaviour
{
    public float timer;
    private float _timer;
    public float speed = 30f;

    private void Reset()
    {
        speed = AsteroidSpawner.Instance.speed;
        _timer = timer;
    }

    public void Update()
    {
        transform.position += -transform.right * speed * Time.deltaTime;
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            AsteroidSpawner.Instance.ReturnAsteroid(this);
            _timer = timer;
        }
    }

    public static void TurnOn(Asteroid p)
    {
        p.Reset();
        p.gameObject.SetActive(true);
    }

    public static void TurnOff(Asteroid p)
    {
        //var partycleExplosion = PartycleAsteroidExplosionSpawner.Instance.pool.GetObject();
        //partycleExplosion.transform.position = p.transform.position;
        p.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlatformObstacle>())
        {
            AsteroidSpawner.Instance.ReturnAsteroid(this);
            AsteroidSpawner.Instance.InstanceParticlesPO(other.gameObject.GetComponent<PlatformObstacle>());
        }
        if (other.gameObject.GetComponent<BlackHole>())
        {
            AsteroidSpawner.Instance.ReturnAsteroid(this);
            AsteroidSpawner.Instance.InstanceParticlesBH(other.gameObject.GetComponent<BlackHole>());
        }
    }
}
