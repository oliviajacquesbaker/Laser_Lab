using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallObject : LaserLabObject, ILaserTarget
{
    public abstract Laser[] OnLaserHit(Laser laser);

    public Vector2Int GetPosition()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }
}
