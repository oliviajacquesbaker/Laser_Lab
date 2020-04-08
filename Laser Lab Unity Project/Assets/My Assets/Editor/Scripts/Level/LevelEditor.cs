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

    private void SetSceneDirty()
    {
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    private void RecordUndo(string name)
    {
        Undo.RegisterFullObjectHierarchyUndo(level.gameObject, name);
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
                RecordUndo("Change Level Tile Set");
                level.SetTileset(tileSet.objectReferenceValue as TileSet);
                ReloadBoard();
            }
        }

        {
            Vector2Int tmpSize = EditorGUILayout.Vector2IntField("Size", level.size);
            tmpSize.x = Mathf.Clamp(tmpSize.x, 1, 10);
            tmpSize.y = Mathf.Clamp(tmpSize.y, 1, 10);
            if (!level.size.Equals(tmpSize))
            {
                RecordUndo("Resize Level");
                ResizeBoard(tmpSize);
            }
        }

        {
            bool sandbox = EditorGUILayout.Toggle("Sandbox Level", level.sandboxLevel);
            if (sandbox != level.sandboxLevel)
            {
                RecordUndo("Toggle Sandbox Mode");
                level.sandboxLevel = sandbox;
            }
        }

        if (GUILayout.Button("Refresh"))
        {
            RecordUndo("Refresh Level");
            ReloadBoard();
        }

        DrawGridView();

        DrawSelectedTileProperties();

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
        SetSceneDirty();
    }

    private void ResizeBoard(Vector2Int newSize)
    {
        level.Resize(newSize);
        SetSceneDirty();
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

    private void DrawSelectedTileProperties()
    {
        if (!level.board.IsWithinWalls(selectedTile))
            selectedTile = new Vector2Int(-1, -1);
        if (!selectedTile.Equals(new Vector2Int(-1, -1)))
        {
            EditorGUILayout.LabelField("Selected Tile: " + selectedTile.ToString());
            EditorGUI.indentLevel++;
            if (level.board.IsWallTile(selectedTile))
            {
                WallObject wallObject = level.board.GetWallObject(selectedTile);
                int newIndex;
                int index = findIndex(wallObjectTypes, wallObject.GetType());
                if (index != -1)
                {
                    newIndex = EditorGUILayout.Popup("Object Type", index, wallObjectTypeNames);
                    DrawProperties(wallObject);
                }
                else
                {
                    newIndex = EditorGUILayout.Popup("Object Type", 0, wallObjectTypeNames);
                }

                if (newIndex != index)
                {
                    ReplaceWall(selectedTile, wallObjectTypes[newIndex]);
                }
            }
            else
            {
                BoardObject boardObject = level.board.GetBoardObject(selectedTile);
                int newIndex;
                int index = 0;
                if (boardObject)
                {
                    index = findIndex(boardObjectTypes, boardObject.GetType());
                    if (index != -1)
                    {
                        newIndex = EditorGUILayout.Popup("Object Type", index, boardObjectTypeNames);
                        DrawProperties(boardObject);
                    }
                    else
                    {
                        newIndex = EditorGUILayout.Popup("Object Type", 0, wallObjectTypeNames);
                    }
                }
                else
                {
                    newIndex = EditorGUILayout.Popup("Object Type", 0, boardObjectTypeNames);
                }

                if (newIndex != index)
                {
                    ReplaceTile(selectedTile, boardObjectTypes[newIndex]);
                }
            }
            EditorGUI.indentLevel--;
        }
    }

    private void ReplaceWall(Vector2Int pos, Type type)
    {
        WallObject obj = level.board.GetWallObject(pos);
        WallObject newObj = null;
        if (type != null)
            newObj = level.board.CreateWallAt(pos, type);
        if (obj && (newObj != null || type == null))
        {
            DestroyImmediate(obj.gameObject);
        }
        if (type != null && newObj == null)
            return;
        level.board.SetWallObject(pos, newObj);
        SetSceneDirty();
    }

    private void ReplaceTile(Vector2Int pos, Type type)
    {
        BoardObject obj = level.board.GetBoardObject(pos);
        BoardObject newObj = null;
        if (type != null)
            newObj = level.board.CreateBoardObjectAt(pos, type);
        if (obj && (newObj != null || type == null))
        {
            DestroyImmediate(obj.gameObject);
        }
        if (type != null && newObj == null)
            return;
        level.board.SetBoardObject(pos, newObj);
        SetSceneDirty();
    }

    private void DrawProperties(LaserLabObject obj)
    {
        SerializedObject serialObj = new SerializedObject(obj);
        SerializedProperty prop = serialObj.GetIterator();
        GUI.enabled = false;
        prop.NextVisible(true);
        EditorGUILayout.PropertyField(prop);
        GUI.enabled = true;
        while (prop.NextVisible(true))
        {
            bool isPrefabData = System.Attribute.GetCustomAttribute(obj.GetType().GetField(prop.name), typeof(PrefabDataAttribute)) != null;
            if (isPrefabData)
                GUI.enabled = false;
            EditorGUILayout.PropertyField(prop);
            GUI.enabled = true;
        }
        if(serialObj.ApplyModifiedProperties())
        {
            if (obj is IRefreshable)
                (obj as IRefreshable).Refresh();
        }
    }

    private SerializedProperty GetItterator(UnityEngine.Object obj)
    {
        SerializedObject serialTile = new SerializedObject(obj);
        SerializedProperty prop = serialTile.GetIterator();
        return prop;
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
        boardObjectTypes = getTypes(typeof(BoardObject));
        ArrayUtility.Insert(ref boardObjectTypes, 0, null);

        wallObjectTypeNames = new string[wallObjectTypes.Length];
        boardObjectTypeNames = new string[boardObjectTypes.Length];

        wallObjectIcons = new Texture[wallObjectTypes.Length];
        boardObjectIcons = new Texture[boardObjectTypes.Length];

        boardObjectTypeNames[0] = "Empty";

        string[] iconPaths;
        {
            string[] guids = AssetDatabase.FindAssets(" Icon");
            iconPaths = new string[guids.Length];
            for (int i = 0; i < guids.Length; i++) {
                iconPaths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
            }
        }

        for (int i = 0; i < wallObjectTypes.Length; i++)
        {
            wallObjectTypeNames[i] = wallObjectTypes[i].Name;
            for (int j = 0; j < iconPaths.Length; j++)
            {
                if (iconPaths[j].Contains(wallObjectTypeNames[i]))
                {
                    wallObjectIcons[i] = AssetDatabase.LoadAssetAtPath<Sprite>(iconPaths[j]).texture;
                }
            }
            wallObjectTypeNames[i] = wallObjectTypeNames[i].Remove(0, 10);
        }

        for (int i = 1; i < boardObjectTypes.Length; i++)
        {
            boardObjectTypeNames[i] = boardObjectTypes[i].Name;
            for (int j = 0; j < iconPaths.Length; j++)
            {
                if (iconPaths[j].Contains(boardObjectTypeNames[i]))
                {
                    boardObjectIcons[i] = AssetDatabase.LoadAssetAtPath<Texture>(iconPaths[j]);
                }
            }
            boardObjectTypeNames[i] = boardObjectTypeNames[i].Remove(0, 11);
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