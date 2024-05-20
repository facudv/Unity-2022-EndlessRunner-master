using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [HideInInspector] public int actualLevel; //con este int tengo que avisarle al PlatformManager que haga lo que tenga que hacer segun el nivelActual;
    public PlatformManager pm;
    private float _timer = 60f;
    private bool _stopReduceLevel;
    private int _maxReduction = 17;

    //feedbuck levelUp
    public GameObject levelUpImage;
    bool _levelUpImageEnabled;
    float _timerOffLevelUpImage = 3;

  
    void Update()
    {
        TimerToLevelUp();
        ActiveImageLevelUp();
    }

    void TimerToLevelUp()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            if (pm.blockRowWalkeableTiles >= _maxReduction)
            {
                pm.blockRowWalkeableTiles-= 1;
                _timer = 60f;
                Debug.Log("SUBI DE NIVEL");
                levelUpImage.SetActive(true);
                _levelUpImageEnabled = true;
                CheckLevel();
                //le digo al platform manager actualLevel++;
            }
        }
    }

    void ActiveImageLevelUp()
    {
        if (_levelUpImageEnabled)
        {
            _timerOffLevelUpImage -= Time.deltaTime;
            if (_timerOffLevelUpImage <= 0)
            {
                levelUpImage.SetActive(false);
                _levelUpImageEnabled = false;
                _timerOffLevelUpImage = 5f;
            }
        }
    }

    void CheckLevel()
    {
        actualLevel++;
        pm.CurrentLevel(actualLevel);
        if (actualLevel == 1)
        {
            LevelOne();
        }
        if (actualLevel == 2)
        {
            LevelTwo();
        }
    }

    void LevelOne()
    {
        AsteroidSpawner.Instance.speed += 30;
        //en platformManager se aumenta la dificultad al modificar "actualLevel".
    }

    void LevelTwo()
    {
        pm.levelThree = true;
        PlatformObstacleSpawner.Instance.isLevelThree = true;
    }
}
