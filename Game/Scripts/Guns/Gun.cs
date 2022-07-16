using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assert = UnityEngine.Assertions.Assert;

public class Gun : MonoBehaviour
{
    public Vector2 Target { get; private set; }
    [SerializeField] private GunProperties gunProps;

    private int nBulletsLoaded;
    private bool canShoot = true;

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
        if (!canShoot || nBulletsLoaded <= 0) return;
        StartCoroutine(HitTarget(GetHit()));
    }

    private GameObject GetHit()
    {
        Vector2 position = transform.position;
        Vector2 dir = (Target - position).normalized;;
        
        Vector2 origin = position + dir.normalized * transform.localScale.x * 0.5f;
        
        var hits = Physics2D.RaycastAll(origin, dir);

        foreach (RaycastHit2D hit in hits)
        {
            var hitColl = hit.collider;

            if (hitColl == null || transform.IsChildOf(hitColl.transform)) continue;
            return hitColl.gameObject;
        }

        return null;
    }

    private IEnumerator HitTarget(GameObject go)
    {
        nBulletsLoaded--;

        canShoot = false;
        StartCoroutine(CanShootCooldown());

        CharacterHealth health;
        if (!go || !(health = go.GetComponent<CharacterHealth>()))
            yield break;

        float distance = (go.transform.position - transform.position).magnitude;
        yield return new WaitForSeconds(distance / gunProps.BulletSpeed);

        health.Hit(gunProps.Damage);
    }

    private IEnumerator CanShootCooldown()
    {
        yield return new WaitForSeconds(gunProps.FireRate);
        canShoot = true;
    }

    public void Reload()
    {
        if (nBulletsLoaded == gunProps.MaxAmmo) return;
        StartCoroutine(ReloadCoroutine());
    }
    
    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(gunProps.ReloadTime);
        nBulletsLoaded = gunProps.MaxAmmo;
    }

    public void SetTarget(Vector2 targetPos) => Target = targetPos;
}