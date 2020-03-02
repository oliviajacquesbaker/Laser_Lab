using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    Level level;
    Vector2Int selectedTile = new Vector2Int(-1,-1);

    static Type[] boardObjectTypes;
    static string[] boardObjectTypeNames;
    static Texture[] boardObjectIcons;

    static Type[] wallObjectTypes;
    static string[] wallObjectTypeNames;
    static Texture[] wallObjectIcons;

    static bool initialized = false;

    private void OnSceneGUI()
    {
        
    }

    public override void OnInspectorGUI()
    {
        //if (Event.current.type != EventType.)
        //    return;

        if (!initialized)
            if (EditorStyles.miniButton != null)
                Init();
            else return;

        level = (Level)target;

        EditorGUILayout.BeginVertical();

        {
            SerializedProperty script = serializedObject.FindProperty("m_Script");

            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", script.objectReferenceValue, typeof(MonoBehaviour), false);
            GUI.enabled = true;
        }

        {
            SerializedProperty tileSet = serializedObject.FindProperty("tileSet");
            TileSet old = tileSet.objectReferenceValue as TileSet;
            EditorGUILayout.PropertyField(tileSet, new GUIContent("Tile Set"));
            serializedObject.ApplyModifiedProperties();
            if (tileSet.objectReferenceValue as TileSet != old)
            {
                level.SetTileset(tileSet.objectReferenceValue as TileSet);
                ReloadBoard();
            }
        }

        {
            Vector2Int tmpSize = EditorGUILayout.Vector2IntField("Width", new Vector2Int(level.board.Width, level.board.Height));
            tmpSize.x = Mathf.Clamp(tmpSize.x, 1, 10);
            tmpSize.y = Mathf.Clamp(tmpSize.y, 1, 10);
            ResizeBoard(tmpSize);
        }

        if (GUILayout.Button("Refresh"))
            ReloadBoard();

        DrawGridView();

        EditorGUILayout.EndVertical();

        if (serializedObject.ApplyModifiedProperties())
        {
            level.floor.tileSet = level.tileSet;
        }
    }

    private void ReloadBoard()
    {
        level.Resize(level.size);
        level.board.ReloadTiles();
    }

    private void ResizeBoard(Vector2Int newSize)
    {
        if (level.size.Equals(newSize))
            return;

        level.Resize(newSize);
    }

    private int findIndex<T> (T[] list, T value)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] == null)
            {
                if (value == null)
                    return i;
            }
            else if (list[i].Equals(value))
                return i;
        }
        return -1;
    }

    private void DrawGridView()
    {
        EditorGUILayout.BeginVertical(gridContainerStyle);

        for (int j = level.board.Height; j >= -1; j--)
        {
            EditorGUILayout.BeginHorizontal(gridRowStyle);
            for (int i = -1; i < level.board.Width + 1; i++)
            {
                Vector2Int pos = new Vector2Int(i, j);

                if (level.board.IsWithinWalls(pos))
                {
                    Color oldCol = GUI.backgroundColor;
                    if (pos == selectedTile)
                        GUI.backgroundColor = Color.grey;

                    if (GUILayout.Button(FindIcon(level.board.GetLaserLabObject(pos)), gridObjectStyle))
                    {
                        selectedTile = pos;
                    }
                    GUI.backgroundColor = oldCol;
                }
                else if (pos.x == -1 && pos.y == -1)
                {
                    Color oldCol = GUI.backgroundColor;
                    GUI.backgroundColor = new Color(1,.5f,.5f);
                    if (GUILayout.Button("X", gridObjectStyle))
                    {
                        selectedTile = pos;
                    }
                    GUI.backgroundColor = oldCol;
                }
                else
                {
                    GUILayout.Box("", gridObjectStyleEmpty);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    private Texture FindIcon(LaserLabObject obj)
    {
        if (obj is BoardObject)
        {
            return boardObjectIcons[findIndex(boardObjectTypes, obj.GetType())];
        } else if (obj is WallObject)
        {
            return wallObjectIcons[findIndex(wallObjectTypes, obj.GetType())];
        }

        return null;
    }

    private void Init()
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

        gridObjectStyle = new GUIStyle(EditorStyles.miniButtonMid)
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

        wallObjectTypes = getTypes(typeof(WallObject));
        ArrayUtility.Insert(ref wallObjectTypes, 0, null);
        boardObjectTypes = getTypes(typeof(BoardObject));
        ArrayUtility.Insert(ref boardObjectTypes, 0, null);

        wallObjectTypeNames = new string[wallObjectTypes.Length];
        boardObjectTypeNames = new string[boardObjectTypes.Length];

        wallObjectIcons = new Texture[wallObjectTypes.Length];
        boardObjectIcons = new Texture[boardObjectTypes.Length];

        wallObjectTypeNames[0] = "Empty";
        boardObjectTypeNames[0] = "Empty";

        string[] iconPaths;
        {
            string[] guids = AssetDatabase.FindAssets("EditorIcon");
            iconPaths = new string[guids.Length];
            for (int i = 0; i < guids.Length; i++) {
                iconPaths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
            }
        }

        for (int i = 1; i < wallObjectTypes.Length; i++)
        {
            wallObjectTypeNames[i] = wallObjectTypes[i].Name.Remove(0,10);
            for (int j = 0; j < iconPaths.Length; j++)
            {
                if (iconPaths[j].Contains(wallObjectTypeNames[i]))
                {
                    wallObjectIcons[i] = AssetDatabase.LoadAssetAtPath<Texture>(iconPaths[j]);
                }
            }
        }
        for (int i = 1; i < boardObjectTypes.Length; i++)
        {
            boardObjectTypeNames[i] = boardObjectTypes[i].Name.Remove(0, 11);
            for (int j = 0; j < iconPaths.Length; j++)
            {
                if (iconPaths[j].Contains(boardObjectTypeNames[i]))
                {
                    boardObjectIcons[i] = AssetDatabase.LoadAssetAtPath<Texture>(iconPaths[j]);
                }
            }
        }
    }

    private Type[] getTypes(Type baseClass)
    {
        Type[] allTypes = System.Reflection.Assembly.GetAssembly(baseClass).GetTypes();
        List<Type> result = new List<Type>();
        for (int i = 0; i < allTypes.Length; i++)
        {
            if (allTypes[i].IsSubclassOf(baseClass))
                result.Add(allTypes[i]);
        }
        return result.ToArray();
    }

    public static GUIStyle gridContainerStyle;

    public static GUIStyle gridRowStyle;

    public static GUIStyle gridObjectStyle;

    public static GUIStyle gridObjectStyleEmpty;

}