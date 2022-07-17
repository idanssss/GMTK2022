using UnityEngine;
using Action = System.Action;
using Assert = UnityEngine.Assertions.Assert;

public class CharacterHealth : MonoBehaviour
{
    private float _health;
    public float Health
    {
        get => _health;
        private set
        {
            value = value < 0 ? 0 : value;
            if (Mathf.Abs(_health - value) < 0.0001f) return;
            
            _health = value;

            if (value == 0)
                OnDeath?.Invoke();
        }
    }

    [SerializeField] private float startHealth = 100f;

    private void Awake()
    {
        Assert.IsTrue(startHealth > 0, "Start health must be greater than 0");
        _health = startHealth;
    }

    public event Action OnDeath;
    
    public delegate void GetHitDel(GameObject go);
    public event GetHitDel OnGetHit;

    public void Hit(float damage, GameObject go)
    {
        Health -= damage;
        OnGetHit?.Invoke(go);
    }
}