using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardObject : MonoBehaviour, ILaserTarget
{
   
    public int Rotation { get; protected set; }
    public abstract Laser[] OnLaserHit(Laser laser);
}
