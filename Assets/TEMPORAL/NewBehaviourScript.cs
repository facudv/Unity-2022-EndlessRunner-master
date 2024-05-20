using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private int NUM = 20;
    // Start is called before the first frame update
    void Start()
    {
        NUM -= 2;
        Debug.Log(NUM);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
