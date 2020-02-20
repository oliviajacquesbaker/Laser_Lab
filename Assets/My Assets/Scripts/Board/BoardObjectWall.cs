using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObjectWall : BoardObject
{
    public override Laser[] OnLaserHit(Laser laser)
    {
        return new Laser[0];
    }
}
