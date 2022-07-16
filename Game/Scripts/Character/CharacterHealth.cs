using UnityEngine;

public class CharacterHealth : MonoBehaviour, IDamageable
{
    private float _health;
    private bool _dead;

    bool IDamageable.Dead
    {
        get => _dead;
        set => _dead = value;
    }
    float IDamageable.Health
    {
        get => _health;
        set => _health = value;
    }
    
    public event System.Action OnDeath;

    public void Hit(float damage)
    {
        float newHealth = _health - damage;
        if (newHealth <= 0)
        {
            _dead = true;
            _health = 0;
            OnDeath?.Invoke();
        }
        else
            _health = newHealth;
    }
}