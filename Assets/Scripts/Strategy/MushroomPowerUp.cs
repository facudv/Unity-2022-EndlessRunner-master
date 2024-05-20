using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPowerUp : IActivate
{
    Model mdl;
    
    public MushroomPowerUp(Model _mdl)
    {
        Debug.Log("entre");
        mdl = _mdl;        
    }

    public void Activate()
    {
        mdl.bigGuy = true;
    }
}
