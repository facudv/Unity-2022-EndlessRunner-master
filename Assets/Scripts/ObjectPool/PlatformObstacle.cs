using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformObstacle : MonoBehaviour
{
    static int _start;

    private void Reset()
    {
        levelThree = PlatformObstacleSpawner.Instance.isLevelThree;
    }

    public static void TurnOn(PlatformObstacle p)
    {
        p.gameObject.SetActive(true);
        p.Reset();
    }

    public static void TurnOff(PlatformObstacle p)
    {
        p.gameObject.SetActive(false);
    }

    public bool levelThree;

    public void OnTriggerEnter(Collider other)
    {
        if (levelThree)
        {
            if (other.gameObject.GetComponent<Model>())
            {
                DebuffSpawner.Instance.pool.GetObject().transform.position = this.transform.position + new Vector3(80, 3, 0);
            }
        }
        if (other.gameObject.tag == "restart")
        {
            PlatformObstacleSpawner.Instance.ReturnPlatform(this);
        }
    }
}
