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

    public Direction GetDirection(Board board)
    {
        Vector2Int pos = GetPosition();

        if (pos.x == -1)
            return Direction.RIGHT;
        if (pos.y == -1)
            return Direction.UP;
        if (pos.x == board.Width)
            return Direction.LEFT;
        if (pos.y == board.Height)
            return Direction.DOWN;

        return Direction.UP;
    }
}
