using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject GunEnd;
    
    public void LookAt(Vector2 pos)
    {
        Vector2 direction = (pos - (Vector2) transform.position).normalized;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(GunEnd.transform.position, GunEnd.transform.right);

        if(hit.collider != null)
        {
            Debug.Log("Hit Object " + hit.collider.transform.name);
        }
    }
}