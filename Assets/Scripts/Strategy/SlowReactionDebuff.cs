using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowReactionDebuff : IActivate
{
    /// hay que crear una variable en model que sea public float timerSidesSave; 
    /// timerSidesSave = timerSides;

    Model mdl;

    public SlowReactionDebuff(Model _mdl)
    {
        mdl = _mdl;
    }

    public void Activate()
    {
        mdl.timerStrategy -= Time.deltaTime;
        if (mdl.timerStrategy >= 0)
        {
            mdl.timerSides = mdl.timerSidesSave + 0.5f;
            
        }
        else
        {
            mdl.timerSides = mdl.timerSidesSave;
            mdl.ResetPower();
            mdl.timerStrategy = 5f;
            mdl.view.SlowReactionOff();
            mdl.view.StopAllCoroutines();
        }
    }
}
