using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private GunProperties gun;
    
    public Vector2 Dir { get; private set; }
    public GunProperties Properties { get; private set; }

    [SerializeField] private float rotationOffset;

    public bool Shot { get; private set; }
    public bool visible;
    
    [HideInInspector]
    public GameObject shotBy;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void OnEnable() => GetComponent<SpriteRenderer>().enabled = visible; 

    public void Shoot(Vector2 dir, GunProperties gun)
    {
        if (Shot) return;
        dir.Normalize();
        
        Shot = true;
        
        Dir = dir;
        Properties = gun;

        this.gun = gun;
        rb.velocity = dir * gun.BulletSpeed;
        
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));
        
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject != shotBy)
        {
            CharacterHealth health;
            if (!(health = col.gameObject.GetComponent<CharacterHealth>())) return;
            
            health.Hit(gun.Damage, gameObject);
            Destroy(gameObject);
        }
    }
}