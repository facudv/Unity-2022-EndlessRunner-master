using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : Enemy
{
    public List<ParticleSystem> fly;
    bool up = true;
    bool down;
    float _timerIdle;

    public void Update()
    {
        Move();
    }

    public void Move()
    {
        if (up)
        {
            if (transform.position.y < 4)
            {
                transform.position += transform.up * Time.deltaTime;
                Fly();
            }
            else
            {
                up = false;
                _timerIdle -= Time.deltaTime;
                if(_timerIdle <= 0)
                {
                    down = true;
                    Shoot();
                    Fly();
                    _timerIdle = 0.5f;
                }
            }
        }
        if (down)
        {
            if (transform.position.y > 0)
            {
                transform.position -= transform.up * Time.deltaTime;
            }
            else
            {
                down = false;
                _timerIdle -= Time.deltaTime;
                if (_timerIdle <= 0)
                {
                    _timerIdle = 0.5f;
                    up = true;
                    Shoot();
                }
            }
        }
    }

    public override void Shoot()
    {
        base.Shoot();
    }

    public void Fly()
    {
        foreach (var item in fly)
        {
            item.Play();
        }
    }

    // ----- OBJECT POOL -----//

    private void Reset()
    {

    }

    public static void TurnOn(EnemyMove e)
    {
        e.Reset();
        e.gameObject.SetActive(true);
    }

    public static void TurnOff(EnemyMove e)
    {
        e.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "restart" || other.gameObject.GetComponent<Bullet>() || other.gameObject.GetComponent<Model>())
        {
            if (other.gameObject.GetComponent<Model>())
            {
                PartycleExplosionEnemySpawner.Instance.InstanceParticlesEM(this);
            }
            EnemyMoveSpawner.Instance.ReturnEnemyMove(this);
        }
    }
}
