using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CharacterMovement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Gun gun;
    
    private CharacterMovement _movement;
    private LayerMask tileMask;

    private void Awake() => _movement = GetComponent<CharacterMovement>();

    private void Start()
    {
        tileMask = LayerMask.GetMask("Tile");
    }

    private void Update()
    {
        UpdateTarget();
    }

    private void UpdateTarget() => gun.SetTarget(PlayerInput.Player.transform.position);
}
