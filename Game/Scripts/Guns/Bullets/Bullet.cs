using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private GunProperties gun;
    public bool Shot { get; private set; }
    public bool visible;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void OnEnable() => GetComponent<SpriteRenderer>().enabled = visible; 

    public void Shoot(Vector2 dir, GunProperties gun)
    {
        if (Shot) return;
        Shot = true;

        this.gun = gun;
        rb.velocity = dir.normalized * gun.BulletSpeed;
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        CharacterHealth health;
        if (!(health = col.gameObject.GetComponent<CharacterHealth>())) return;
        
        health.Hit(gun.Damage);
        Destroy(gameObject);
    }
}