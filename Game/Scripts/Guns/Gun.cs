using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

public class Gun : MonoBehaviour
{
    public Vector2 Target { get; private set; }
    [SerializeField] private GunProperties gunProps;

    public int nBulletsLoaded;

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
        Vector2 position = transform.position;
        Vector2 dir = (Target - position).normalized;;
        
        Vector2 origin = position + dir.normalized * transform.localScale.x * 0.5f;
        
        var hits = Physics2D.RaycastAll(origin, dir);

        foreach (RaycastHit2D hit in hits)
        {
            var hitColl = hit.collider;

            if (hitColl == null || transform.IsChildOf(hitColl.transform)) continue;
            
            Debug.Log("Hit Object " + hitColl.transform.name);
            break;
        }
    }

    public void SetTarget(Vector2 targetPos) => Target = targetPos;
}