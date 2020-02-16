public interface ILaserTarget
{
    //returns any resulting lasers
    Laser[] OnLaserHit(Laser laser);
}
