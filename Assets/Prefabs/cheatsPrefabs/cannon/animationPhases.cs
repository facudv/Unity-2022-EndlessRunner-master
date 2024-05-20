using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationPhases : MonoBehaviour
{
    public GameObject cannon;
    public GameObject connection;

    public void StepOneCannon()
    {
        cannon.SetActive(true);
    }

    public void FinalStep()
    {
        connection.SetActive(true);
    }
}
