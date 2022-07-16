public interface IDamageable
{
    public bool Dead { get; protected set; }
    public float Health { get; protected set; }
    void Hit(float damage);
}