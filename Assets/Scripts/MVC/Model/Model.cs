using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Model : MonoBehaviour
{


    public Vector3 offsetBullet;
    public float timerShoot;
    [HideInInspector] public float _timerShoot;

    [HideInInspector] public int actualRail;

    public float timerJump;
    private float _timerJump;
    public float timerSides;
    private float _timerSides;

    public Vector3 sideVector;

    private bool _cancelSideMoveInJump;

    public int sidesMoveForce;
    private int _sidesMoveForce;
    public int jumpForce;
    private int _jumpForce;
    public float speed;
    private float _speed;

    [SerializeField] private float limitForceDown;

    public Rigidbody rb;
    private bool _forceDown;

    // Controller & View

    IController myController;

    public View view;

    #region Strategys Parameters

    #region Strategy Cheats Parameters
    [HideInInspector] public bool cheatAdditionalCannon;
    [HideInInspector] public float timeInvulnerability;
    [HideInInspector] public int aditionalLife;

    IActivate myCurrentStrategyCheat;
    IActivate myCurrentStrategyOffCheat;
    IActivate myCurrentStrategyCheatAddCannon;
    IActivate myCurrentStrategyCheatInvulnerability;
    IActivate myCurrentStrategyCheatOffNearObstacle;
    IActivate myCurrentStrategyCheatAddLife;
    IActivate myCurrentStrategyCheatDeactivateObstacles;

    public List<LayerMask> layerMaskObstacles;
    [HideInInspector] public int obstacleType;
    [HideInInspector] public Quaternion startRotationCannonOffNearObstacle;
    public GameObject cannonRight;
    public GameObject cannonLeft;
    #endregion

    #region Strategy PowerUps&Debuffs Parameters
    [HideInInspector] public bool bigGuy;
    [HideInInspector] public float timerStrategy;
    [HideInInspector] public bool canDie;
    [HideInInspector] public float timerSidesSave;
    [HideInInspector] public bool canShoot;

    IActivate myCurrentStrategy;
    IActivate myCurrentStrategyNoPowerUp;
    IActivate myCurrentStrategyMushroomPowerUp;
    IActivate myCurrentStrategyStarPowerUp;
    IActivate myCurrentStrategySlowReactionDebuff;
    IActivate myCurrentStrategyDisarmDebuff;
    #endregion

    #endregion

    public void Awake()
    {
        timerStrategy = 5f;
        canShoot = true;
        _timerShoot = timerShoot;
        timerSidesSave = timerSides;
        actualRail = 0;
        _timerJump = timerJump;
        _timerSides = timerSides;
        _sidesMoveForce = sidesMoveForce;
        _jumpForce = jumpForce;
        _speed = speed;
        bigGuy = false;
        canDie = true;
        //buff and debuff strategy
        myCurrentStrategyNoPowerUp = new NoPowerUp(this);
        myCurrentStrategyMushroomPowerUp = new MushroomPowerUp(this);
        myCurrentStrategyStarPowerUp = new StarPowerUp(this);
        myCurrentStrategySlowReactionDebuff = new SlowReactionDebuff(this);
        myCurrentStrategyDisarmDebuff = new DisarmDebuff(this);
        //cheat strategy
        myCurrentStrategyOffCheat = new EndCheat(this);
        myCurrentStrategyCheatInvulnerability = new InvulnerabilityCheat(this);
        myCurrentStrategyCheatAddLife = new AddLifeCheat(this);
        myCurrentStrategyCheatOffNearObstacle = new OffNearObstacleCheat(this);
    }

    private void Update()
    {
        if (myCurrentStrategy != null)
            myCurrentStrategy.Activate();

        if (myCurrentStrategyCheat != null)
            myCurrentStrategyCheat.Activate();

        //myController.OnUpdate();
        Move();
        JumpTimer();
        ShootTimer();
        SidesMoveTimer();
        Reespawn();
        Inputs();
    }

    private void FixedUpdate()
    {
        ForceDown();
    }

    private void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.W)) Jump();
        if (Input.GetKeyDown(KeyCode.A)) Left();
        if (Input.GetKeyDown(KeyCode.D)) Right();
    }

    public void OffCheat()
    {
        myCurrentStrategyCheat = myCurrentStrategyOffCheat;
    }

    public void ResetPower()
    {
        myCurrentStrategy = myCurrentStrategyNoPowerUp;
    }

    public void ResetParams()
    {
        view.meshModel.enabled = true;
        view.fillAreaStar.SetActive(false);
        view.sliderStar.value = 5f;
        timerStrategy = 5f;
        canDie = true;
        canShoot = true;
        bigGuy = false;
        view.ShieldDown();
        view.DisarmOff();
        view.StarDown();
        view.SlowReactionOff();
        view.ResetColourButton(view.buttonImagesList);
        view.ResetColourButton(view.buttonShootList);
    }

    public void JumpTimer()
    {
        if (_timerJump > 0)
        {
            _timerJump -= Time.deltaTime;
        }
    }

    public void ShootTimer()
    {
        if (_timerShoot > 0)
        {
            _timerShoot -= Time.deltaTime;
        }
    }

    public void SidesMoveTimer()
    {
        if (_timerSides > 0)
        {
            _timerSides -= Time.deltaTime;
        }
    }

    public void Jump()
    {
        view.JumpPartycles();
        if (!_cancelSideMoveInJump)
        {
            if (_timerJump < 0)
            {
                rb.AddForce(0, _jumpForce, 0, ForceMode.Impulse);
                _timerJump = timerJump;
                _cancelSideMoveInJump = true;
            }
        }
    }

    public void Right()
    {
        if (!_cancelSideMoveInJump)
        {
            if (_timerSides < 0)
            {
                if (actualRail < 1)
                {
                    transform.position += -sideVector;
                    _timerSides = timerSides;
                    _timerJump = timerJump;
                    actualRail++;
                }
            }
        }
    }

    public void Left()
    {
        if (!_cancelSideMoveInJump)
        {
            if (_timerSides < 0)
            {
                if (actualRail > -1)
                {
                    transform.position += sideVector;
                    _timerSides = timerSides;
                    _timerJump = timerJump;
                    actualRail--;
                }
            }
        }
    }

    public void Shoot(Vector3 offset, Vector3 startPos)
    {
        if (canShoot)
        {
            if (_timerShoot <= 0)
            {
                if (cheatAdditionalCannon) SpawnAdditionalBullet(); //Si tengo activo el cheat de cañones adicionales spawn mas balas
                var bullet = BulletSpawner.Instance.pool.GetObject();
                bullet.transform.position = startPos + offset;
                view.FeedBuckShoot();
                _timerShoot = timerShoot;
            }
        }
    }

    [HideInInspector] public bool stop;

    public void Move()
    {
        if (!stop)
            transform.position += transform.forward * FlyWeightPointer.flyWightStatsPlayer.playerSpeed * Time.deltaTime;
    }

    public void ForceDown()
    {
        if (transform.position.y >= limitForceDown)
        {
            _forceDown = true;
            _cancelSideMoveInJump = true;
        }
        if (_forceDown)
        {
            rb.AddForce(0, -_jumpForce * 3f, 0, ForceMode.Force);
        }
    }

    [HideInInspector] public float reespawnTimer;
    [HideInInspector] public bool reespawn;

    public void Reespawn()
    {
        if (reespawn)
        {
            reespawnTimer += Time.deltaTime;
            if (reespawnTimer < 3f)
            {
                canDie = false;
                additionalLifeState = false;
            }
            else
            {
                canDie = true;
                reespawn = false;
                reespawnTimer = 0;
                additionalLifeState = true;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //layer "0" == ground
        if (collision.gameObject.GetComponent<Platform>())
        {
            _forceDown = false;
            _cancelSideMoveInJump = false;
        }
    }

    [HideInInspector] public bool additionalLifeState;

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.tag != "restart")
        {
            if (other.gameObject.GetComponent<PlatformObstacle>() || other.gameObject.GetComponent<Asteroid>() || other.gameObject.GetComponent<Bullet>() || other.gameObject.GetComponent<BlackHole>())
            {
                if (!additionalLifeState)
                {
                    if (canDie)
                    {
                        if (!bigGuy)
                        {
                            Defeat();
                            ReturnToPoolObstacleTrigger(other);
                        }
                        else
                        {
                            if (other.gameObject.GetComponent<PlatformObstacle>())
                            {
                                PlatformObstacleSpawner.Instance.ReturnPlatform(other.gameObject.GetComponent<PlatformObstacle>());
                                if (other.gameObject.GetComponent<Enemy>())
                                {
                                    PartycleExplosionEnemySpawner.Instance.InstanceParticles(other.gameObject.GetComponent<PlatformObstacle>());
                                }
                                else PlatformObstacleSpawner.Instance.InstanceParticles(other.gameObject.GetComponent<PlatformObstacle>());
                                myCurrentStrategy = myCurrentStrategyNoPowerUp;
                                view.ShieldDown();
                            }
                            if (other.gameObject.GetComponent<Asteroid>())
                            {
                                AsteroidSpawner.Instance.ReturnAsteroid(other.gameObject.GetComponent<Asteroid>());
                                AsteroidSpawner.Instance.InstanceParticles(other.gameObject.GetComponent<Asteroid>());
                                myCurrentStrategy = myCurrentStrategyNoPowerUp;
                                view.ShieldDown();
                            }
                            if (other.gameObject.GetComponent<Bullet>())
                            {
                                BulletSpawner.Instance.ReturnPlatform(other.gameObject.GetComponent<Bullet>());
                                myCurrentStrategy = myCurrentStrategyNoPowerUp;
                                view.ShieldDown();
                            }
                            if (other.gameObject.GetComponent<BlackHole>())
                            {
                                BlackHoleSpawner.Instance.ReturnBlackHole(other.gameObject.GetComponent<BlackHole>());
                                myCurrentStrategy = myCurrentStrategyNoPowerUp;
                                view.ShieldDown();
                            }
                        }
                    }
                    else
                    {
                        ReturnToPoolObstacleTrigger(other);
                    }
                }
                else if (aditionalLife > 0)
                {
                    CheatLostAdditionalLife();
                    ReturnToPoolObstacleTrigger(other);
                }
            }
        }

        if (other.gameObject.tag == "mushroom")
        {
            ResetParams();
            Debug.Log("hongo");
            myCurrentStrategy = myCurrentStrategyMushroomPowerUp;
            view.ShieldUp();
        }
        if (other.gameObject.tag == "star")
        {
            ResetParams();
            Debug.Log("star");
            myCurrentStrategy = myCurrentStrategyStarPowerUp;
            view.StarUp();
        }
        if (other.gameObject.tag == "slowreaction")
        {
            ResetParams();
            Debug.Log("SLOWMO");
            view.SlowReactionOn();
            myCurrentStrategy = myCurrentStrategySlowReactionDebuff;
        }
        if (other.gameObject.tag == "disarm")
        {
            ResetParams();
            view.DisarmOn();
            Debug.Log("SINARMA");
            myCurrentStrategy = myCurrentStrategyDisarmDebuff;
        }
    }

    public void ReturnToPoolObstacleTrigger(Collider other)
    {
        if (other.gameObject.GetComponent<PlatformObstacle>())
        {
            PlatformObstacleSpawner.Instance.ReturnPlatform(other.gameObject.GetComponent<PlatformObstacle>());
            if (other.gameObject.GetComponent<Enemy>())
            {
                PartycleExplosionEnemySpawner.Instance.InstanceParticles(other.gameObject.GetComponent<PlatformObstacle>());
            }
            else PlatformObstacleSpawner.Instance.InstanceParticles(other.gameObject.GetComponent<PlatformObstacle>());
        }
        if (other.gameObject.GetComponent<Asteroid>())
        {
            AsteroidSpawner.Instance.ReturnAsteroid(other.gameObject.GetComponent<Asteroid>());
            AsteroidSpawner.Instance.InstanceParticles(other.gameObject.GetComponent<Asteroid>());
        }
        if (other.gameObject.GetComponent<Bullet>())
        {
            BulletSpawner.Instance.ReturnPlatform(other.gameObject.GetComponent<Bullet>());
        }
        if (other.gameObject.GetComponent<BlackHole>())
        {
            BlackHoleSpawner.Instance.ReturnBlackHole(other.gameObject.GetComponent<BlackHole>());
        }
    }

    public void Defeat()
    {
        view.Die();
        FindObjectOfType<Menu>().StartCoroutineDie();
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<SphereCollider>().enabled = false;
        stop = true;
    }

    #region Cheats  
    //AddCannon
    public void SpawnAdditionalBullet()
    {
        if (cannonRight.activeSelf == true)
        {
            var bullet = BulletSpawner.Instance.pool.GetObject();
            bullet.transform.position = this.transform.position + new Vector3(7, 1.5f, -6);
        }

        if (cannonLeft.activeSelf == true)
        {
            var bullet = BulletSpawner.Instance.pool.GetObject();
            bullet.transform.position = this.transform.position + new Vector3(7, 1.5f, 6);
        }
    }

    //Invulnerability
    public void InvulnerabilityStartegyState()
    {
        myCurrentStrategyCheat = myCurrentStrategyCheatInvulnerability;
    }


    //AddLife
    public void AddLifeStrategyState()
    {
        myCurrentStrategyCheat = myCurrentStrategyCheatAddLife;
    }

    public void CheatLostAdditionalLife()
    {
        view.EffectLostAdditionalLifeCanvas();
        aditionalLife--;
        reespawn = true;
        view.EffectLostAdditionalLifeCoroutine();
    }

    //OffNearObstacle
    public void OffNearObstacleStrategyState()
    {
        startRotationCannonOffNearObstacle = view.offNearObsacleCannon.transform.rotation;
        myCurrentStrategyCheat = myCurrentStrategyCheatOffNearObstacle;
    }

    #endregion

}



