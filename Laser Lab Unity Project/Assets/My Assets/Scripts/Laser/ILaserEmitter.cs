using UnityEngine;

public interface ILaserEmitter : ILaserTarget
{
    //returns any resulting lasers
    Laser[] OnLaserEmit();
}