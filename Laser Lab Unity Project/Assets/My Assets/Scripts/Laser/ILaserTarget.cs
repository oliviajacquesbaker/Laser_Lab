using UnityEngine;

public interface ILaserTarget
{
    //returns any resulting lasers
    Laser[] OnLaserHit(Laser laser);
    Vector2Int GetPosition();
}
