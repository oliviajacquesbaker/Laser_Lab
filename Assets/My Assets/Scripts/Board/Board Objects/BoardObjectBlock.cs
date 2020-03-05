﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObjectBlock : BoardObject
{
    public BoardObjectBlock()
    {
        Orientation = Direction.UP;
        CanRotate = false;
    }

   
    public override Laser[] OnLaserHit(Laser laser)
    {
        return new Laser[0];
    }
}
