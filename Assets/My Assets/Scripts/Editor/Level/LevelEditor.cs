using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    Level level;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        level = (Level)target;
        EditorGUILayout.BeginVertical();
        DrawGridView();
        EditorGUILayout.EndVertical();
    }

    private void DrawGridView()
    {
        EditorGUILayout.BeginVertical(gridContainerStyle);

        for (int j = level.board.Tiles.GetLength(1); j >= -1; j--)
        {
            EditorGUILayout.BeginHorizontal(gridRowStyle);
            for (int i = -1; i < level.board.Tiles.GetLength(0) + 1; i++)
            {
                Vector2Int pos = new Vector2Int(i, j);

                if (level.board.IsWithinBoard(pos))
                {
                    BoardObject obj = level.board.GetBoardObject(pos);
                    if (GUILayout.Button(BoardObjectEditor.FindPreview(obj), gridObjectStyle))
                    {
                        BoardObjectEditor.Open(obj);
                    }
                } else if (level.board.IsWallTile(pos))
                {
                    WallObject obj = level.board.GetWallObject(pos);
                    if (GUILayout.Button(WallObjectEditor.FindPreview(obj), gridObjectStyle))
                    {
                        WallObjectEditor.Open(obj);
                    }
                } else
                {
                    GUILayout.Box("", gridObjectStyleEmpty);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

    }

    [MenuItem("Assets/Create/Game/Level")]
    public static void Create()
    {
        Level asset = CreateInstance<Level>();

        if (!System.IO.File.Exists(Application.dataPath.Remove(Application.dataPath.LastIndexOf('/') + 1, 6) + AssetDatabase.GetAssetPath(Selection.activeObject) + "/New Level.asset"))
            AssetDatabase.CreateAsset(asset, AssetDatabase.GetAssetPath(Selection.activeObject) + "/New Level.asset");
        else
            asset = CreateNew(1);

        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    private static Level CreateNew(int index)
    {
        Level asset = CreateInstance<Level>();

        if (!System.IO.File.Exists(Application.dataPath.Remove(Application.dataPath.LastIndexOf('/') + 1, 6) + AssetDatabase.GetAssetPath(Selection.activeObject) + "/New Level " + index + ".asset"))
        {
            AssetDatabase.CreateAsset(asset, AssetDatabase.GetAssetPath(Selection.activeObject) + "/New Level " + index + ".asset");
        }
        else return CreateNew(index + 1);

        return asset;
    }

    private void OnEnable()
    {
        gridContainerStyle = new GUIStyle()
        {
            padding = new RectOffset(),
            margin = new RectOffset(20, 20, 20, 20)
        };

        gridRowStyle = new GUIStyle()
        {
            margin = new RectOffset(),
            padding = new RectOffset()
        };

        gridObjectStyle = new GUIStyle(EditorStyles.miniButton)
        {
            fixedWidth = 25,
            fixedHeight = 25,
            padding = new RectOffset(),
            margin = new RectOffset(2, 2, 2, 2)
        };

        gridObjectStyleEmpty = new GUIStyle()
        {
            fixedWidth = 25,
            fixedHeight = 25,
            padding = new RectOffset(),
            margin = new RectOffset(2, 2, 2, 2)
        };
    }

    public static GUIStyle gridContainerStyle;

    public static GUIStyle gridRowStyle;

    public static GUIStyle gridObjectStyle;

    public static GUIStyle gridObjectStyleEmpty;

}
