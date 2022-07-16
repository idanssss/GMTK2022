using UnityEngine;

[CreateAssetMenu(fileName = "Default Gun Properties", menuName = "Gun/Gun Properties", order = 1)]
public class GunProperties : ScriptableObject
{
    [SerializeField] private float damage = 8f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float reloadTime = 0.5f;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private int maxAmmo = 50;
        
    public string Name => name;
    public float Damage => damage;
    public float FireRate => fireRate;
    public float ReloadTime => reloadTime;
    public float BulletSpeed => bulletSpeed;
    public int MaxAmmo => maxAmmo;
}