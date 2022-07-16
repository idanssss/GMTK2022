using System.Collections;
using System.Collections.Generic;
//using System.ServiceModel.Configuration;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileManager))]
public class TileEditor : Editor
{
    private TileManager tm;
    private int fallNumber = 1;

    void OnEnable()
    {
        tm = (TileManager) target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Generate!"))
            tm.Generate();
        
        GUILayout.Space(10f);

        fallNumber = EditorGUILayout.IntField("Die Number", fallNumber);
        fallNumber = fallNumber < 1 ? 1 : fallNumber > 6 ? 6 : fallNumber;
        
        if (GUILayout.Button("Drop"))
            tm.Drop(fallNumber);

        DrawDefaultInspector();

        if (GUILayout.Button("Destroy All"))
            tm.DestroyAll();
    }
}
