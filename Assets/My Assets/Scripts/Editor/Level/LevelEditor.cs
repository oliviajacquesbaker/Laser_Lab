using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        Level level = (Level)target;

        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginVertical();
        for (int j = 0; j < level.board.Tiles.GetLength(1); j++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < level.board.Tiles.GetLength(0); i++)
            {
                GUILayout.Button(level.board.Tiles[i, j].GetPreview());
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();
    }

    [MenuItem("Assets/Create/Game/Level")]
    public static void Create()
    {
        Level asset = CreateInstance<Level>();

        AssetDatabase.CreateAsset(asset, "Assets/NewScripableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
