using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    public GameObject enemyPrefab;

    public Stopwatch sw;
    public TileManager tm;

    public int enemiesPerRound = 2;
    public int roundNumber = 0;

    public List<GameObject> enemies = new List<GameObject>();

    private void FixedUpdate()
    {
        foreach (var e in enemies)
        {
            if (e != null) return;
        }

        roundNumber++;
        if (roundNumber % 10 == 0 && enemiesPerRound < 7)
        {
            enemiesPerRound++;
            sw.time -= 0.5f;
        }

        SpawnWave();
    }

    private void SpawnWave()
    {
        enemies.Clear();
        
        // loop on nuber of enemies
        for (int i = 0; i < enemiesPerRound; i++)
        {
            // spawn enemy
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, tm.GetRandomTile(), Quaternion.identity);
        enemies.Add(enemy);
    }
}
