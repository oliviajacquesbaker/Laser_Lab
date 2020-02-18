using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallObject : MonoBehaviour, ILaserTarget
{
    public abstract Laser[] OnLaserHit(Laser laser);
}
