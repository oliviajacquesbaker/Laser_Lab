using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObjectBlank1 : WallObject
{
    public float testfloat;

    public WallObjectBlank1()
    {

    }

    public override Laser[] OnLaserHit(Laser laser)
    {
        return new Laser[0];
    }
}
