using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser
{
    public Vector2Int origin;
    public Direction direction;
    public float red, green, blue;

    public Laser(Vector2Int origin, Direction direction, float red, float green, float blue)
    {
        this.origin = origin;
        this.direction = direction;
        this.red = red;
        this.green = green;
        this.blue = blue;
    }

    public Laser(Vector2Int origin, Direction direction) : this(origin, direction, 1, 1, 1) { }
    public Laser(Direction direction) : this(new Vector2Int(), direction, 1, 1, 1) { }
    public Laser(Direction direction, float red, float green, float blue) : this(new Vector2Int(), direction, red, green, blue) { }

    public Laser(float red, float green, float blue) : this(new Vector2Int(), Direction.UP, red, green, blue) { }
}
