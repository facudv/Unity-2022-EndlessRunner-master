using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public Vector3 sideToRotation;

    void Update()
    {
        transform.Rotate(sideToRotation * Time.deltaTime);
    }
}
