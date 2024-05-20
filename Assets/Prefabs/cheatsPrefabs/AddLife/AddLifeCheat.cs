using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLifeCheat : IActivate
{
    Model mdl;

    public AddLifeCheat(Model _mdl)
    {
        mdl = _mdl;
    }

    public void Activate()
    {
        if (mdl.aditionalLife <= 0)
        {
            mdl.reespawn = false;
            mdl.additionalLifeState = false;
            mdl.canDie = true;
            mdl.OffCheat();
        }
    }
}
