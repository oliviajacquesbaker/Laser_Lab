using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallObject : LaserLabObject, ILaserTarget
{
    public abstract Laser[] OnLaserHit(Laser laser);

}
