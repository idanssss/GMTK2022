using System;
using UnityEngine;

public class BG : MonoBehaviour
{
    [SerializeField] private Vector2 speed;
    private Vector2 offset;
    private Material met;

    private void Awake()
    {
        met = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        offset.x += speed.x * Time.deltaTime;
        offset.y += speed.y * Time.deltaTime;
        
        met.SetTextureOffset("_MainTex", offset);
    }
}