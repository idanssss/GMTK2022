using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerInput : MonoBehaviour
{
    private Camera cam;
    
    private CharacterMovement _movement;
    
    [SerializeField]
    private Transform gunObj;

    private Gun gun;

    private void Awake()
    {
        cam = Camera.main;
        
        _movement = GetComponent<CharacterMovement>();
        gun = gunObj.GetComponent<Gun>();

        if (!gun) Debug.LogError("No gun found on gun object");
    }

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        
        _movement.Move(horizontal, vertical);

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        gun.LookAt(mousePos);
    }
}