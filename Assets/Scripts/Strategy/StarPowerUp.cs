using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPowerUp : IActivate
{
    Model mdl;

    public StarPowerUp(Model _mdl)
    {
        mdl = _mdl;
    }

    public void Activate()
    {
        mdl.view.sliderStar.value -= Time.deltaTime;
        if (mdl.view.sliderStar.value >= 0.1)
        {
            mdl.canDie = false;
        }
        else
        {
            mdl.canDie = true;
            mdl.ResetPower();
            mdl.view.sliderStar.value = 5f;
            mdl.view.DisarmOff();
            mdl.view.StarDown();
        }
    }
}
