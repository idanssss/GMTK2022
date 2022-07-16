using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaveManager))]
public class EnemyWaveManagerEditor : Editor
{
    private WaveManager wm;
    void OnEnable()
    {
        wm = (WaveManager) target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Summon Next Wave!"))
            wm.SummonNextWave();  
        GUILayout.Space(10f); 
        DrawDefaultInspector();
    }
}
