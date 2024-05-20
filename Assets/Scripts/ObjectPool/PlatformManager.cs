using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public int currentLevel;

    public bool menu;
    public AsteroidsSidesManager am;
    private bool createAsteroid;

    private int _distanceBetweenTiles;
    private bool _RowWithWalkableTiles;
    public int blockRowWalkeableTiles;
    private int _blockRowWalkeableTiles;

    public Platform[] row1 = new Platform[4];
    public PlatformObstacle[] row2 = new PlatformObstacle[0];
    public PowerUpAndDebuff[] row3 = new PowerUpAndDebuff[0];

    void Start()
    {
        _startFreeTiles = true;
        _blockRowWalkeableTiles = blockRowWalkeableTiles;
    }

    void Update()
    {
        FlyWeightPointer.flywightState1.timeToGenerateMoreTiles -= Time.deltaTime;
        if (FlyWeightPointer.flywightState1.timeToGenerateMoreTiles <= 0)
        {
            if (!menu)
                TileGenerator();
        }
    }

    public void TileGenerator()
    {
        if (_startFreeTiles)
        {
            FirstTilesFree();
        }
        else
        {
            if (!_RowWithWalkableTiles)
            {
                if (_blockRowWalkeableTiles >= 0)
                {
                    for (int i = 0; i < row1.Length; i++)
                    {
                        if (i == 0)
                        {
                            var randomNum = Random.Range(0, 80);
                            if (randomNum == FlyWeightPointer.flywightState1.spawnBuffOrDebuff)
                            {
                                row3[i] = PowerUpAndDebuffSpawner.Instance.pool.GetObject();
                                row3[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 0);
                            }
                            row1[i] = PlatformSpawner.Instance.pool.GetObject();
                            row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 0);
                            if (currentLevel >= 1) CheckLevel(i);
                        }
                        if (i == 1)
                        {
                            var randomNum = Random.Range(0, 80);
                            if (randomNum == FlyWeightPointer.flywightState1.spawnBuffOrDebuff)
                            {
                                row3[i] = PowerUpAndDebuffSpawner.Instance.pool.GetObject();
                                row3[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 10);
                            }
                            row1[i] = PlatformSpawner.Instance.pool.GetObject();
                            row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 10);
                            if (currentLevel >= 1) CheckLevel(i);
                        }
                        if (i == 2)
                        {
                            var randomNum = Random.Range(0, 80);
                            if (randomNum == FlyWeightPointer.flywightState1.spawnBuffOrDebuff)
                            {
                                row3[i] = PowerUpAndDebuffSpawner.Instance.pool.GetObject();
                                row3[i].transform.position = new Vector3(_distanceBetweenTiles, 0, -10);
                            }
                            row1[i] = PlatformSpawner.Instance.pool.GetObject();
                            row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, -10);
                            if (currentLevel >= 1) CheckLevel(i);
                        }
                        if (i == 3)
                        {
                            _distanceBetweenTiles += 10;
                            _blockRowWalkeableTiles--;
                        }
                    }
                }
                else
                    _RowWithWalkableTiles = !_RowWithWalkableTiles;
            }
            else
            {
                int freeTile = Random.Range(0, 3);
                for (int i = 0; i < row2.Length; i++)
                {
                    RowWithObstacleGenerator(i, freeTile);
                }
                _distanceBetweenTiles += 10;
                _blockRowWalkeableTiles = blockRowWalkeableTiles;
                _RowWithWalkableTiles = !_RowWithWalkableTiles;
            }
        }
    }

    [HideInInspector] public bool levelThree;
    bool _spawnEnemyMove = true;
    int _indexSpawnEnemyMove;


    public void RowWithObstacleGenerator(int i, int freeTile)
    {
        if (_spawnEnemyMove)
        {
            var _indexSpawnEnemyMove = Random.Range(0, 3);
            _spawnEnemyMove = false;
        }
        if (i == 0 && i != freeTile)
        {
            if (!levelThree)
            {
                row2[i] = PlatformObstacleSpawner.Instance.pool.GetObject();
                row2[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 0);
                row1[i] = PlatformSpawner.Instance.pool.GetObject();
                row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 0);
            }
            else LevelThree(i, freeTile);
        }
        if (i == 1 && i != freeTile)
        {
            if (!levelThree)
            {
                row2[i] = PlatformObstacleSpawner.Instance.pool.GetObject();
                row2[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 10);
                row1[i] = PlatformSpawner.Instance.pool.GetObject();
                row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 10);
            }
            else LevelThree(i, freeTile);
        }
        if (i == 2 && i != freeTile)
        {
            if (!levelThree)
            {
                row2[i] = PlatformObstacleSpawner.Instance.pool.GetObject();
                row2[i].transform.position = new Vector3(_distanceBetweenTiles, 0, -10);
                row1[i] = PlatformSpawner.Instance.pool.GetObject();
                row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, -10);
            }
            else LevelThree(i, freeTile);
        }

        //create walkeable tile in free space

        if (i == 0 && i == freeTile)
        {
            row1[i] = PlatformSpawner.Instance.pool.GetObject();
            row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 0);
        }
        if (i == 1 && i == freeTile)
        {
            row1[i] = PlatformSpawner.Instance.pool.GetObject();
            row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 10);
        }
        if (i == 2 && i == freeTile)
        {
            row1[i] = PlatformSpawner.Instance.pool.GetObject();
            row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, -10);
        }
    }


    //creo tiles libres al comienzo del nivel
    bool _startFreeTiles;
    int _freeTiles;

    void FirstTilesFree()
    {
        _freeTiles++;
        for (int i = 0; i < row1.Length; i++)
        {
            if (i == 0)
            {
                row1[i] = PlatformSpawner.Instance.pool.GetObject();
                row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 0);
            }
            if (i == 1)
            {
                row1[i] = PlatformSpawner.Instance.pool.GetObject();
                row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 10);
            }
            if (i == 2)
            {
                row1[i] = PlatformSpawner.Instance.pool.GetObject();
                row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, -10);
            }
            if (i == 3)
            {
                _distanceBetweenTiles += 10;
                _blockRowWalkeableTiles--;
            }
        }
        if (_freeTiles > 40) _startFreeTiles = !_startFreeTiles;
    }

    public void CurrentLevel(int indexLevel)
    {
        currentLevel = indexLevel;
        Debug.Log("current level" + currentLevel);
    }

    bool _randomIndex;
    int _randomPosBlackHole;

    public void CheckLevel(int i)
    {
        // NIVEL 2
        if (currentLevel >= 1)
        {
            var mitad = Mathf.RoundToInt(blockRowWalkeableTiles / 2);
            if (_blockRowWalkeableTiles == mitad)
            {
                if (_randomIndex)
                {
                    _randomPosBlackHole = Random.Range(0, 3);
                    _randomIndex = false;
                }
                if (i == 0 && i == _randomPosBlackHole)
                {

                    var blackHole = BlackHoleSpawner.Instance.pool.GetObject();
                    blackHole.transform.position = new Vector3(_distanceBetweenTiles, 0, 0);

                }
                if (i == 1 && i == _randomPosBlackHole)
                {

                    var blackHole = BlackHoleSpawner.Instance.pool.GetObject();
                    blackHole.transform.position = new Vector3(_distanceBetweenTiles, 0, 10);

                }
                if (i == 2 && i == _randomPosBlackHole)
                {

                    var blackHole = BlackHoleSpawner.Instance.pool.GetObject();
                    blackHole.transform.position = new Vector3(_distanceBetweenTiles, 0, -10);
                }
                if (i == 2) _randomIndex = true;
            }
        }
    }

    public void LevelThree(int i, int freeTile)
    {
        if (i == 0 && i != freeTile)
        {
            if (i == _indexSpawnEnemyMove)
            {
                EnemyMoveSpawner.Instance.pool.GetObject().transform.position = new Vector3(_distanceBetweenTiles, 0, 0);
            }
            else
            {
                row2[i] = PlatformObstacleSpawner.Instance.pool.GetObject();
                row2[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 0);
            }
            row1[i] = PlatformSpawner.Instance.pool.GetObject();
            row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 0);
        }
        if (i == 1 && i != freeTile)
        {
            if (i == _indexSpawnEnemyMove)
            {
                EnemyMoveSpawner.Instance.pool.GetObject().transform.position = new Vector3(_distanceBetweenTiles, 0, 10);
            }
            else
            {
                row2[i] = PlatformObstacleSpawner.Instance.pool.GetObject();
                row2[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 10);
            }
            row1[i] = PlatformSpawner.Instance.pool.GetObject();
            row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, 10);
        }
        if (i == 2 && i != freeTile)
        {
            if (i == _indexSpawnEnemyMove)
            {
                EnemyMoveSpawner.Instance.pool.GetObject().transform.position = new Vector3(_distanceBetweenTiles, 0, -10);
            }
            else
            {
                row2[i] = PlatformObstacleSpawner.Instance.pool.GetObject();
                row2[i].transform.position = new Vector3(_distanceBetweenTiles, 0, -10);
            }
            row1[i] = PlatformSpawner.Instance.pool.GetObject();
            row1[i].transform.position = new Vector3(_distanceBetweenTiles, 0, -10);
        }
    }
}
