using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : BoardObject
{
    Block()
    {
        Rotation = 0;
    }

    Block(int rotation)
    {
        if (rotation >= 0 && rotation <= 3)
            Rotation = rotation;
    }

   
    public override Laser[] OnLaserHit(Laser laser)
    {
        return new Laser[0];
    }
}
