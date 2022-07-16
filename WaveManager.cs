using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    AnimationCurve enemyByPhase;
    public GameObject EnemyPrefab;

    int phase;

    public void SummonNextWave()
    {
        for(int i = 0; i < Mathf.RoundToInt(enemyByPhase.Evaluate(phase+1)); i++)
        {
            Instantiate(EnemyPrefab, new Vector2(Random.Range(-7, 7), Random.Range(-2, 3)), Quaternion.identity);
        }
        phase++;
    }
}
