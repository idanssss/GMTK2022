using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterHealth))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Gun gun;
    [SerializeField] private float hitCooldown = 0.2f;
    [SerializeField] private float shootCooldown = 0.5f;
    
    private CharacterMovement _movement;
    private CharacterHealth _health;

    private LayerMask tileMask;


    private void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
        _health = GetComponent<CharacterHealth>();
        
        _health.OnGetHit += OnGetHit;
        _health.OnDeath += OnDeath;
        
        tileMask = LayerMask.GetMask("Tile");
    }

    private void OnDeath() => Destroy(gameObject);

    private float lastHit = 0f;
    private void FixedUpdate()
    {
        UpdateTarget();
        if (lastHit < hitCooldown) { lastHit += Time.fixedTime; return; }
    }

    
    private void Update()
    {
        HandleShooting();
        Move();
    }

    private float shootTimer;

    private void HandleShooting()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer < shootCooldown) return;
        
        shootTimer = 0f;
        gun.Shoot();
    }

    private void OnGetHit(GameObject go)
    {
        Bullet bullet;
        if (!(bullet = go.GetComponent<Bullet>())) return;
        
        _movement.Move(bullet.Dir * bullet.Properties.BulletSpeed);

        lastHit = 0f;
    }

    private void UpdateTarget()
    {
        if (PlayerInput.Player == null) return;
        gun.SetTarget(PlayerInput.Player.transform.position);
    }

    private void Move()
    {
        var hits = Physics2D.CircleCastAll(transform.position, .5f,
            Vector2.zero, 0f, tileMask);

        _movement.Move(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        if (hits.Length == 0 && lastHit > hitCooldown)
        {
            
            return;
        }
        
        foreach (var hit in hits)
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (!tile || !tile.Shaking || tile.Exists) continue;
            
            _movement.Move(transform.position - tile.transform.position);
        }

        if (!PlayerInput.Player) return;
        var distToPlayer = Vector2.Distance(transform.position, PlayerInput.Player.transform.position);
        if (distToPlayer < 2f)
        {
            shootCooldown = 0.5f;
        }
        else
        {
            shootCooldown = 1.5f;
        }
    }
}
