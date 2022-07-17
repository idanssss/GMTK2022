using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public static List<Entity> AllEntities = new List<Entity>();

    public bool dead;
    void Start()
    {
        AllEntities.Add(this);
    }

    void Update() 
    {
        if(dead)
        {
            Instantiate(Resources.Load("Enemy") as GameObject, new Vector2(Random.Range(-6, 6),  Random.Range(-3, 3)), Quaternion.identity);
            GameObject.Find("Player").GetComponent<PlayerInput>().Score += GlobalManager.ScorePerEnemy;
            AllEntities.Remove(this);
            Destroy(gameObject);
        }
    }
}
