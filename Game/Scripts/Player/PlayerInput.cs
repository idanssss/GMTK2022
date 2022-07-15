using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerInput : MonoBehaviour
{
    private CharacterMovement _movement;
    
    private void Awake() => _movement = GetComponent<CharacterMovement>();
    
    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        _movement.Move(horizontal, vertical);
    }
}