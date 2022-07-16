using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileManager))]
public class TileEditor : Editor
{
    TileManager tm;

    void OnEnable()
    {
        tm = (TileManager) target;
    }

    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Generate!"))
        {
            tm.Generate();
        }
        DrawDefaultInspector();
    }
}
