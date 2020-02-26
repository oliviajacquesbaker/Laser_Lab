using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObjectBlock : BoardObject
{
    public BoardObjectBlock()
    {
        Orientation = 0;
    }

    public BoardObjectBlock(int rotation)
    {
        if (rotation >= 0 && rotation <= 3)
            Orientation = rotation;
    }

   
    public override Laser[] OnLaserHit(Laser laser)
    {
        return new Laser[0];
    }
}
