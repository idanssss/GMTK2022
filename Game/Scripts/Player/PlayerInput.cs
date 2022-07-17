using UnityEngine;
using TMPro;
using Assert = UnityEngine.Assertions.Assert;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterHealth))]
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Player;
    
    private Camera cam;

    [HideInInspector]
    public int Score;

    [SerializeField]
    private TextMeshPro scoreCounterText;

    private CharacterMovement _movement;
    private CharacterHealth _health;
    
    [SerializeField]
    private Transform gunObj;

    private Gun gun;

    // TODO : Add custom key bindings for shooting
    // TODO: Add custom key bindings for reloading

    private void Awake()
    {
        if (Player) Destroy(gameObject);
        else Player = this;
        
        cam = Camera.main;
        _movement = GetComponent<CharacterMovement>();
        
        // Assign gun
        Assert.IsNotNull(gunObj, "No gun object assigned to PlayerInput");
        
        gun = gunObj.GetComponent<Gun>();
        Assert.IsNotNull(gun, "No gun component attached to gun object");
        
        _movement.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        gun.enabled = false;
        canMove = false;
    }

    private bool canMove = true;
    private void Update()
    {
        scoreCounterText.text = "" + Score;
        var horizontal = canMove ? Input.GetAxisRaw("Horizontal") : 0;
        var vertical = canMove ? Input.GetAxisRaw("Vertical") : 0;
        
        _movement.Move(horizontal, vertical);

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        gun.SetTarget(mousePos);

        if (Input.GetMouseButtonDown(0) && canMove)
            gun.Shoot();
        
        if (Input.GetKeyDown(KeyCode.R) && canMove)
            gun.Reload();
    }
}