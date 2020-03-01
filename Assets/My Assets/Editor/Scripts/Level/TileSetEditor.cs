using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileSet))]
public class TileSetEditor : Editor
{
    bool foldout = false;
    bool[] pieceFoldouts;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        TileSet tileSet = target as TileSet;

        EditorGUILayout.BeginVertical();
        {
            SerializedProperty environment = serializedObject.FindProperty("environment");
            EditorGUILayout.PropertyField(environment);
        }
        {
            SerializedProperty pieces = serializedObject.FindProperty("pieces");
            {
                foldout = EditorGUILayout.Foldout(foldout, "Pieces");

                if (foldout)
                {
                    EditorGUI.indentLevel++;

                    pieces.NextVisible(true);

                    EditorGUILayout.PropertyField(pieces);
                    serializedObject.ApplyModifiedProperties();
                    int pieceCount = pieces.intValue;

                    if (pieceFoldouts == null)
                    {
                        pieceFoldouts = new bool[pieceCount];
                    } else if (pieceFoldouts.Length < pieceCount)
                    {
                        ArrayUtility.AddRange(ref pieceFoldouts, new bool[pieceCount-pieceFoldouts.Length]);
                    }

                    for (int i = 0; i < pieceCount; i++)
                    {
                        pieceFoldouts[i] = EditorGUILayout.Foldout(pieceFoldouts[i], "Element " + i);
                        pieces.NextVisible(true);
                        pieces.NextVisible(true);
                        EditorGUI.indentLevel++;
                        MonoScript current = new MonoScript();
                        if (tileSet.pieces[i].objectType != null )
                            current.name = tileSet.pieces[i].objectType.ToString();
                        else
                            current.name = "none";
                        MonoScript script = (MonoScript)EditorGUILayout.ObjectField("Type", current, typeof(MonoScript), false);
                        if (script != current) {
                            if (script == null)
                                tileSet.pieces[i].objectType = null;
                            else
                                tileSet.pieces[i].objectType = script.GetClass().ToString();
                        }
                        EditorGUILayout.PropertyField(pieces);
                        pieces.NextVisible(true);
                        EditorGUILayout.PropertyField(pieces);
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                }
            }
        }
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}
