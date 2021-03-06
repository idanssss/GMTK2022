// using UnityEngine;
// using Action = System.Action;
// using Assert = UnityEngine.Assertions.Assert;
//
// public class CharacterHealth : MonoBehaviour
// {
//     public GameObject gameOverMenu;
//
//     private float _health;
//     public float Health
//     {
//         get => _health;
//         private set
//         {
//             value = value < 0 ? 0 : value;
//             if (Mathf.Abs(_health - value) < 0.0001f) return;
//             
//             _health = value;
//
//             if (value == 0)
//             {
//                 if(transform.name == "Player")
//                     OnDeath?.Invoke();
//             }
//         }
//     }
//
//     [SerializeField] private float startHealth = 100f;
//
//     private void Awake()
//     {
//         Assert.IsTrue(startHealth > 0, "Start health must be greater than 0");
//         _health = startHealth;
//         OnDeath += GameOver;
//     }
//
//     public event Action OnDeath;
//     
//     public delegate void GetHitDel(GameObject go);
//     public event GetHitDel OnGetHit;
//
//     public void Hit(float damage, GameObject go)
//     {
//         Health -= damage;
//         OnGetHit?.Invoke(go);
//     }
//
//     private void GameOver()
//     {
//         gameOverMenu.SetActive(true);
//     }
// }
using UnityEngine;
using UnityEngine.UI;
using Action = System.Action;
using Assert = UnityEngine.Assertions.Assert;

// public class CharacterHealth : MonoBehaviour
// {
//     
//     // public GameObject gameOverMenu;
//     // public Slider HPslider;
//     //
//     // private float _health;
//     // public float Health
//     // {
//     //     get => _health;
//     //     private set
//     //     {
//     //         value = value < 0 ? 0 : value;
//     //         if (Mathf.Abs(_health - value) < 0.0001f) return;
//     //
//     //         _health = value;
//     //
//     //
//     //
//     //         if (value <= 0)
//     //         {
//     //             if (transform.name == "Player")
//     //             {
//     //                 OnDeath?.Invoke();
//     //                 Destroy(gameObject);
//     //             }
//     //         }
//     //     }
//     // }
//     //
//     // [SerializeField] private float startHealth = 100f;
//     //
//     // private void Awake()
//     // {
//     //     Assert.IsTrue(startHealth > 0, "Start health must be greater than 0");
//     //     _health = startHealth;
//     //     OnDeath += GameOver;
//     // }
//     //
//     // public event Action OnDeath;
//     //
//     // public delegate void GetHitDel(GameObject go);
//     // public event GetHitDel OnGetHit;
//     //
//     // public void Hit(float damage, GameObject go)
//     // {
//     //     Health -= damage;
//     //     OnGetHit?.Invoke(go);
//     //     if(transform.name == "Player")
//     //         // HPslider.value = 100 / (100 / (startHealth / _health));
//     //         HPslider.value = Health;
//     // }
//     //
//     // private void GameOver()
//     // {
//     //     gameOverMenu.SetActive(true);
//     // }
// }

using UnityEngine;
using UnityEngine.UI;
using System;

public class CharacterHealth : MonoBehaviour
{
    public GameObject gameOverMenu;
    public Slider HPslider;

    private float _health;
    public float Health
    {
        get => _health;
        private set
        {
            value = value < 0 ? 0 : value;
            if (Mathf.Abs(_health - value) < 0.0001f) return;

            _health = value;



            if (value <= 0)
            {
                if (transform.name == "Player")
                {
                    OnDeath?.Invoke();
                    Debug.Log("asidsajidjkdasn");
                }
                
                
                Destroy(gameObject);
            }
        }
    }

    [SerializeField] private float startHealth = 100f;

    private void Awake()
    {
        UnityEngine.Assertions.Assert.IsTrue(startHealth > 0, "Start health must be greater than 0");
        _health = startHealth;
        OnDeath += GameOver;
    }

    public event Action OnDeath;

    public delegate void GetHitDel(GameObject go);
    public event GetHitDel OnGetHit;

    public void Hit(float damage, GameObject go)
    {
        Health -= damage;
        OnGetHit?.Invoke(go);
        if(transform.name == "Player")
            // HPslider.value = 100 / (100 / (startHealth / _health));
            HPslider.value = Health;
    }

    private void GameOver()
    {
        gameOverMenu.SetActive(true);
    }
}