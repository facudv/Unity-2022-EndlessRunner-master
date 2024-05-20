using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOtherTileBackground : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Model>())
        {
            FindObjectOfType<BackgroundTileManager>().CreateBackgroundTile();
        }
    }
}
