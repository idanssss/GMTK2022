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


    private void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
        _health = GetComponent<CharacterHealth>();
        
        _health.OnGetHit += OnGetHit;
        _health.OnDeath += OnDeath;
    }

    private void OnDeath() => Destroy(gameObject);

    private float lastHit = 0f;
    private void FixedUpdate()
    {
        UpdateTarget();
        if (lastHit < hitCooldown) { lastHit += Time.fixedTime; return; }
        
        _movement.Move(0, 0);
    }

    
    private void Update() => HandleShooting();

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
        
        Debug.Log("Enemy got hit by " + bullet.shotBy.name);
        _movement.Move(bullet.Dir * bullet.Properties.BulletSpeed);

        lastHit = 0f;
    }

    private void UpdateTarget() => gun.SetTarget(PlayerInput.Player.transform.position);
}
