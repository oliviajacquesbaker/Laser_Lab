using UnityEngine;

public abstract class LaserLabPiece : LaserLabObject, ILaserTarget
{
    [PrefabData]
    [HideInInspector]
    public TileSet.Piece TileSetPiece;

    public abstract Laser[] OnLaserHit(Laser laser);

    public Vector2Int GetPosition()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }

}