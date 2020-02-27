using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LaserLabObject : ScriptableObject, ILaserTarget
{
    public abstract Laser[] OnLaserHit(Laser laser);
}
