using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entity : MonoBehaviour
{
    public static List<Entity> AllEntities = new List<Entity>();

    public bool dead;
    void Start()
    {
        AllEntities.Add(this);
    }
}
