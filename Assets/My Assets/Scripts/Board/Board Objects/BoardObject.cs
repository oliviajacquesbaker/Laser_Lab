using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public abstract class BoardObject : LaserLabPiece, ILaserTarget, IRefreshable
{
    [PrefabData]
    public HoverIcon HoverIcon;
    [PrefabData]
    public Material previewMaterial;
    public Direction Orientation = Direction.UP;
    public bool CanMove = true;
    public bool CanRotate = true;
    public bool Placed = false;
    public bool RandomizeOrientation = true;

    private new Renderer renderer;
    private new Collider collider;
    private bool m_IsPreviewing;
    private Material originalMaterial;

    public abstract Laser[] OnLaserHit(Laser laser);

    public Vector2Int GetPosition()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
        originalMaterial = renderer.material;
        HoverIcon.gameObject.SetActive(false);

        if (RandomizeOrientation)
            Orientation = (Direction)(Random.value * 4);

        Refresh();
    }

    public void SetTileSetPieceData(TileSet.Piece piece)
    {
        TileSetPiece = piece;
    }

    public void SetPreview(bool enable)
    {
        if (m_IsPreviewing && !enable)
        {
            m_IsPreviewing = false;
            collider.enabled = true;
            renderer.material = originalMaterial;
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            gameObject.SetActive(false);
        } else if (!m_IsPreviewing && enable)
        {
            m_IsPreviewing = true;
            collider.enabled = false;
            renderer.material = previewMaterial;
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            gameObject.SetActive(true);
        }
    }

    public void Refresh()
    {
        transform.rotation = Quaternion.Euler(0, ((int)Orientation - (int)TileSetPiece.modelOrientation) * 90, 0);

        if (Application.isPlaying)
        {
            gameObject.SetActive(Placed);
        }

        HoverIcon.CanRotate = CanRotate;
        HoverIcon.CanMove = CanMove;
        HoverIcon.Refresh();
    }

    public void Rotate()
    {
        Orientation = (Direction)(((int)Orientation + 1) % 4);
        transform.rotation = Quaternion.Euler(0, ((int)Orientation - (int)TileSetPiece.modelOrientation) * 90, 0);
        HoverIcon.Refresh();
    }

    public void RotateTo(Direction newOrientation)
    {
        Orientation = newOrientation;
        transform.rotation = Quaternion.Euler(0, ((int)Orientation - (int)TileSetPiece.modelOrientation) * 90, 0);
    }

    public void Move(Vector2Int pos)
    {
        transform.position = new Vector3(pos.x, 0, pos.y);
    }

    public void Place()
    {
        SetPreview(false);
        Placed = true;
        Refresh();
    }

    public void Pickup()
    {
        Placed = false;
        Refresh();
    }

    public override void OnHoverEnter()
    {
        base.OnHoverEnter();
        HoverIcon.gameObject.SetActive(true);
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        HoverIcon.gameObject.SetActive(false);
    }

    //Returns the new direction a beam would take if reflected off a mirror at angle '\' when rotation = 0,2 or '/' when rotation = 1,3
    public static Direction Reflect(Direction dir, Direction orientation)
    {
        switch ((int)orientation % 2)
        {
            case 0:
                switch ((int)dir % 2)
                {
                    case 0:
                        return (Direction)(((int)dir + 3) % 4);
                    case 1:
                        return (Direction)(((int)dir + 1) % 4);
                    default:
                        return dir;
                }
            case 1:
                switch ((int)dir % 2)
                {
                    case 0:
                        return (Direction)(((int)dir + 1) % 4);
                    case 1:
                        return (Direction)(((int)dir + 3) % 4);
                    default:
                        return dir;
                }
            default:
                return dir;
        }
    }
    
    public Direction Reflect(Direction dir)
    {
        return Reflect(dir, Orientation);
    }

    /* Returns a direction to represent the relative face on the board
     * object that is hit by a laser moving in the direction dir.
     * 
     * If the object is rotated to the "right" (1) and a laser is going "left" (3), the result should
     * be "up" (0) because the face in contact would be the top relative to the original rotation.
     */
    public static int getFace(Direction dir, Direction orientation)
    {
        return ((int)dir + 6 - (int)orientation) % 4;
    }

    public int getFace(Direction dir)
    {
        return getFace(dir, Orientation);
    }
}
