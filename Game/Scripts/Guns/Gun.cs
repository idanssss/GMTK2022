using UnityEngine;
using System.Collections;
using Assert = UnityEngine.Assertions.Assert;
using TMPro;

public class Gun : MonoBehaviour
{
    public Vector2 Target { get; private set; }
    [SerializeField] private GunProperties gunProps;

    public TextMeshProUGUI bulletCountText;

    private int nBulletsLoaded;
    private bool canShoot = true;
    private bool reloading;

    private void Awake()
    {
        Assert.IsNotNull(gunProps, "Gun properties not set!");
        nBulletsLoaded = gunProps.MaxAmmo;
    }


    private void FixedUpdate() => LookAtTarget();

    private void LookAtTarget()
    {
        Vector2 position = transform.position;
        Vector2 direction = (Target - position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Shoot()
    {
        if (!canShoot || nBulletsLoaded <= 0 || reloading) return;
        
        nBulletsLoaded--;
        if(transform.parent.name == "Player")
            bulletCountText.text = string.Concat(nBulletsLoaded);
        canShoot = false;
        StartCoroutine(CanShootCooldown());
        
        GameObject bulletGo = Instantiate(gunProps.BulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        bullet.shotBy = transform.parent.gameObject;
        bullet.Shoot(Target - (Vector2)transform.position, gunProps);
    }

    private IEnumerator CanShootCooldown()
    {
        yield return new WaitForSeconds(gunProps.FireRate);
        if(transform.parent.name == "Player")
            bulletCountText.text = string.Concat(nBulletsLoaded);
        canShoot = true;
    }

    public void Reload()
    {
        if (nBulletsLoaded == gunProps.MaxAmmo) return;
        StartCoroutine(ReloadCoroutine());
    }
    
    private IEnumerator ReloadCoroutine()
    {
        reloading = true;
        yield return new WaitForSeconds(gunProps.ReloadTime);
        nBulletsLoaded = gunProps.MaxAmmo;
        if(transform.parent.name == "Player")
            bulletCountText.text = string.Concat(nBulletsLoaded);
        reloading = false;
    }

    public void SetTarget(Vector2 targetPos) => Target = targetPos;
}