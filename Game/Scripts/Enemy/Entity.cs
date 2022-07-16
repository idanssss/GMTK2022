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
            AllEntities.Remove(this);
            Destroy(gameObject);
        }
    }
}
