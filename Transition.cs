using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    SpriteRenderer sr;

    void Start () => sr = GetComponent<SpriteRenderer>();

    void Update() => sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - .05f);
}
