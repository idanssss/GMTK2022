using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject CurrentAimedObject;
    CharacterMovement cm;

    void Awake() => cm = GetComponent<CharacterMovement>();

    void Update()
    {
        UpdateTarget();
        if(Random.Range(0,10) > 6)
            Move();
    }

    void UpdateTarget()
    {
        if(CurrentAimedObject == null)
        {
            if(Random.Range(0, 10) == 6)
            {
                CurrentAimedObject = Entity.AllEntities[Random.Range(0,Entity.AllEntities.Count)].gameObject;
            }
            else
            {
                CurrentAimedObject = GameObject.Find("Player");
            } 
        }
        transform.GetChild(0).GetComponent<Gun>().SetTarget(CurrentAimedObject.transform.position);
    }

    void Move()
    {
        var val = Random.Range(0,4);
        cm.Move(val == 0 ? transform.right : val == 1 ? -transform.right : val == 2 ? transform.up : -transform.up);
    }
}
