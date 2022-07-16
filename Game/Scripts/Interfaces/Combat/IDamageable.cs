public interface IDamageable
{
    public float Health { get; protected set; }
    void Hit(float damage);
}