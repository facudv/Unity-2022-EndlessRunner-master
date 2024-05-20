using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffNearObstacleCheat : IActivate
{
    Model mdl;
    float _timer = 0.5f;
    //Collider[] _nearObstacle;

    float _lerpRange;
    bool _rotationLerp;
    GameObject obstacle;


    public OffNearObstacleCheat(Model _mdl)
    {
        mdl = _mdl;
    }

    public void Activate()
    {
        if (!_rotationLerp)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                Collider[] _nearObstacle = Physics.OverlapSphere(mdl.transform.position, 160, mdl.layerMaskObstacles[mdl.obstacleType]);
                foreach (var item in _nearObstacle)
                {
                    _rotationLerp = !_rotationLerp;
                    obstacle = item.gameObject;
                    mdl.view.StartCoroutineElectric();
                    _timer = 0.3f;
                    break;
                }
                _timer = 0.3f;
            }
        }
        else if (_lerpRange < 1)
        {
            _returnToStartPosition = false;
            _lerpRange += Time.deltaTime * 3f;
            mdl.view.offNearObsacleCannon.transform.forward = Vector3.Lerp(mdl.view.offNearObsacleCannon.transform.position, (obstacle.transform.position - mdl.view.offNearObsacleCannon.transform.position), _lerpRange);
        }
        else
        {
            ReturnObstaclePool(obstacle);
        }
        if (_returnToStartPosition)
        {
            _timerToBackCannon -= Time.deltaTime;
            if (_timerToBackCannon <= 0)
            {
                if (_lerpToStartPos < 1)
                //if (_timerToBackCannon < 1)
                {
                    _lerpToStartPos += Time.deltaTime * 1f;
                    //mdl.view.offNearObsacleCannon.transform.up = Vector3.Lerp(mdl.view.offNearObsacleCannon.transform.position, mdl.view.offNearObsacleCannon.transform , 1);
                    mdl.view.offNearObsacleCannon.transform.rotation = Quaternion.Lerp(mdl.view.offNearObsacleCannon.transform.rotation, mdl.startRotationCannonOffNearObstacle, _lerpToStartPos);
                }
                else _returnToStartPosition = false;
            }
        }
    }

    public void ReturnObstaclePool(GameObject platformObstacle)
    {
        if (mdl.obstacleType == 0 || mdl.obstacleType == 1 || mdl.obstacleType == 2) //si es un asteroide fijo, medio o grande. O un enmigo.
        {
            if (platformObstacle.gameObject.GetComponentInParent<Enemy>())
            {
                PartycleExplosionEnemySpawner.Instance.InstanceParticles(platformObstacle.gameObject.GetComponentInParent<PlatformObstacle>());
            }
            else PlatformObstacleSpawner.Instance.InstanceParticles(platformObstacle.gameObject.GetComponentInParent<PlatformObstacle>());
            PlatformObstacleSpawner.Instance.ReturnPlatform(platformObstacle.GetComponentInParent<PlatformObstacle>());
        }
        if (mdl.obstacleType == 3) //si es un asteroide que se mueve
        {
            AsteroidSpawner.Instance.InstanceParticles(platformObstacle.gameObject.GetComponentInParent<Asteroid>());
            AsteroidSpawner.Instance.ReturnAsteroid(platformObstacle.gameObject.GetComponentInParent<Asteroid>());
        }
        _rotationLerp = !_rotationLerp;
        _lerpRange = 0;
        _timerToBackCannon = 0.5f;
        _returnToStartPosition = true;
        _lerpToStartPos = 0;
    }

    bool _returnToStartPosition;
    float _timerToBackCannon;
    float _lerpToStartPos;
}


