using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    private int m_width;
    [SerializeField]
    [HideInInspector]
    private int m_height;

    public int Width { get { return m_width; } }
    public int Height { get { return m_height; } }

    [HideInInspector]
    public TileSet tileSet;
    TileSet.EnvironmentPiece floorTile { get { return tileSet.environment.Floor; } }
    GameObject[] floorTiles;

#if UNITY_EDITOR
    public void UpdateTiles()
    {
        if (!tileSet)
        {
            Debug.LogError("No tileset selected");
            return;
        }

        if (!floorTile.Tile)
        {
            Debug.LogError("No floor tile in tileset");
            return;
        }

        if (floorTiles != null)
            for (int i = 0; i < floorTiles.Length; i++)
            {
                DestroyImmediate(floorTiles[i]);
            }
        floorTiles = new GameObject[Width * Height];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                GameObject tile = Instantiate(floorTile.Tile);
                tile.transform.position = new Vector3(i, 0, j);
                tile.transform.Rotate(0, -90 * (int)floorTile.modelOrientation, 0);
                floorTiles[i + Width * j] = tile;
                tile.transform.SetParent(transform);
                tile.name = "Floor Tile (" + i + "," + j + ")";
            }
        }
    }

    public void Resize(Vector2Int newSize)
    {
        m_width = newSize.x;
        m_height = newSize.y;
        UpdateTiles();
    }
#endif
}
