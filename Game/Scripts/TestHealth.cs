using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterHealth))]
[RequireComponent(typeof(SpriteRenderer))]
public class TestHealth : MonoBehaviour
{
    private CharacterHealth health;
    private SpriteRenderer rend;

    private void Awake()
    {
        health = GetComponent<CharacterHealth>();

        health.OnGetHit += OnGetHit;
        health.OnDeath += OnDie;
        
        
        rend = GetComponent<SpriteRenderer>();
        rend.color = Color.white;
    }

    private void OnGetHit()
    {
        StartCoroutine(Hit());
    }

    private void OnDie()
    {
        rend.enabled = false;
    }

    private IEnumerator Hit()
    {
        rend.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rend.color = Color.white;
    }
}