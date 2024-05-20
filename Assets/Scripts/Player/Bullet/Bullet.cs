using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    private Renderer mat;
    public float speed;
    private float _speed;
    private float _timer;

    private void Awake()
    {
        mat = GetComponent<Renderer>();
    }

    void Reset()
    {
        mat.material.color = Color.grey;
        GetComponentInChildren<ParticleSystem>().startColor = Color.cyan;
        _timer = FlyWeightPointer.flyWightStateBullet.timerBullet;
        speed = 150;
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            BulletSpawner.Instance.ReturnPlatform(this);
            _timer = FlyWeightPointer.flyWightStateBullet.timerBullet;
        }
    }

    public static void TurnOn(Bullet p)
    {
        p.Reset();
        p.gameObject.SetActive(true);
        //this.transform.position = FlyWeightPointer.flyweightState.player.transform.position;
    }

    public static void TurnOff(Bullet p)
    {
        p.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            var platformObstacle = other.gameObject.GetComponent<PlatformObstacle>();
            PlatformObstacleSpawner.Instance.ReturnPlatform(platformObstacle);
            BulletSpawner.Instance.ReturnPlatform(this);
            _timer = FlyWeightPointer.flyWightStateBullet.timerBullet;
        }
        if (other.gameObject.GetComponent<PlatformObstacle>())
        {
            var partycleBullet = PartycleBulletSpawner.Instance.pool.GetObject();
            partycleBullet.transform.position = other.transform.position + new Vector3(-9, 0, 0);
            BulletSpawner.Instance.ReturnPlatform(this);
            if (other.gameObject.GetComponent<Enemy>())
            {
                PartycleExplosionEnemySpawner.Instance.InstanceParticles(other.gameObject.GetComponent<PlatformObstacle>());
            }
            _timer = FlyWeightPointer.flyWightStateBullet.timerBullet;
        }
        if (other.gameObject.GetComponent<BlackHole>())
        {
            var partycleBullet = PartycleBulletSpawner.Instance.pool.GetObject();
            partycleBullet.transform.position = other.transform.position + new Vector3(0, 0, 0);
            BulletSpawner.Instance.ReturnPlatform(this);
        }
    }

    public void ChangeColor()
    {
        mat.material.color = Color.red;
    }
}
