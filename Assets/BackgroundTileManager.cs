using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTileManager : MonoBehaviour
{
    public List<BackgroundTile> backgroundTile;
    private Vector3 _offset;

    void Start()
    {
        StartCoroutine(FirstBackgroundTile());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBackgroundTile()
    {
        _offset += FlyWeightPointer.flyWightBackgroundTile.offSetTileBackground;
        var backgroundtile = BackgroundTileSpawner.Instance.pool.GetObject();   
        backgroundtile.transform.position = _offset;
        backgroundTile.Add(backgroundtile);
    }

    public void ReturnTile()
    {
        BackgroundTileSpawner.Instance.ReturnBackgroundTile(backgroundTile[0]);
        backgroundTile.RemoveAt(0);
    }

    IEnumerator FirstBackgroundTile()
    {
        yield return new WaitForSeconds(0.5f);
        var backgroundtile = BackgroundTileSpawner.Instance.pool.GetObject();
        backgroundtile.transform.position = Vector3.zero;
        backgroundTile.Add(backgroundtile);
    }
}
