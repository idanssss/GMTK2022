using System;
using System.Collections;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class DiceMenu : MonoBehaviour
{
    private SpriteRenderer rend;
    [SerializeField] private float spinSpeed = 10;
    [SerializeField] private Sprite[] sprites;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(Change());
        rend.sprite = sprites[0];
    }

    private float z = 0;
    private void Update()
    {
        // Spin
        transform.localRotation = Quaternion.Euler(0, 0, z);
        z = Mathf.MoveTowards(z, 360, spinSpeed * Time.deltaTime);

        if (z >= 360)
            z = 0;
    }

    private IEnumerator Change()
    {
        while (true) {
            for (int i = 0; i < 6; i++)
            {
                rend.sprite = sprites[i];
                yield return new WaitForSeconds(2f);
            }
        }
    }
}