using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterHealth))]
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Player;
    
    private Camera cam;
    
    private CharacterMovement _movement;
    
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
    }

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        
        _movement.Move(horizontal, vertical);

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        gun.SetTarget(mousePos);

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
            gun.Shoot();
        
        if (Input.GetButtonDown("Reload") || Input.GetKeyDown(KeyCode.R))
            gun.Reload();
    }
}