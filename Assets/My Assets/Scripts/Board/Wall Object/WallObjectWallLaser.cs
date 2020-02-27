using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObjectWallLaser : WallObject, ILaserEmitter
{
    private float redOut;
    private float greenOut;
    private float blueOut;
    WallObjectWallLaser()
    {
        redOut = greenOut = blueOut = 1;
    }

    WallObjectWallLaser(float red, float green, float blue)
    {
        redOut = red;
        greenOut = green;
        blueOut = blue;
    }
    public override Laser[] OnLaserHit(Laser laser)
    {
        return new Laser[0];
    }

    public Laser[] OnLaserEmit(Laser laser)
    {
        Laser newLaser = new Laser(redOut, greenOut, blueOut);
        return new Laser[] { newLaser };
    }
}
