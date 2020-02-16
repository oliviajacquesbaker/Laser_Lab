public interface ILaserTarget
{
    //returns any resulting lasers
    public abstract Laser[] OnLaserHit(Laser laser);
}
