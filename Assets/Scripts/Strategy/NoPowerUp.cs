using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPowerUp : IActivate
{
    Model mdl;

    public NoPowerUp(Model _mdl)
    {
        mdl = _mdl;
    }

    public void Activate()
    {
        mdl.bigGuy = false;
        mdl.canDie = true;
        mdl.canShoot = true;
        mdl.timerStrategy = 5f;
        mdl.view.DisarmOff();
    }
}
