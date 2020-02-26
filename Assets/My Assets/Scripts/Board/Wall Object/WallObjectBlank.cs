using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObjectBlank : WallObject
{
    [SerializeField]
    float testfloat;
    public override Laser[] OnLaserHit(Laser laser)
    {
        return new Laser[0];
    }
}
