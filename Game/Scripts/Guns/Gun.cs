using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    [SerializeField]
    
    
    public void LookAt(Vector2 pos)
    {
        Vector2 direction = (pos - (Vector2) transform.position).normalized;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // TODO: Implement Bullet Shooting
    public void Shoot(Vector2 pos)
    {
        
    }
}