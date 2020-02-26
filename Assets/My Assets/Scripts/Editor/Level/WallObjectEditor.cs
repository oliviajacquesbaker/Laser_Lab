using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class WallObjectEditor : EditorWindow
{
    WallObject Target;
    public static void Open(WallObject target)
    {
        WallObjectEditor editor = (WallObjectEditor)GetWindow(typeof(WallObjectEditor));
        editor.Target = target;
    }

    private void OnGUI()
    {
        
    }

    public static Texture FindPreview(WallObject obj)
    {
        if (obj == null)
            return null;
        return null;
        throw new NotImplementedException();
    }
}
