using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisarmDebuff : IActivate
{
    Model mdl;
    float _timer = 5f;

    public DisarmDebuff(Model _mdl)
    {
        mdl = _mdl;
    }

    public void Activate()
    {
        _timer -= Time.deltaTime;
        if (_timer >= 0)
        {
            mdl.canShoot = false;
            
        }
        else
        {
            mdl.canShoot = true;
            mdl.ResetPower();
            _timer = 5f;
        }
    }
}
