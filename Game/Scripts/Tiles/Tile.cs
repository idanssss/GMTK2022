using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    public int AssociatedNumber;
    bool enabled;

    void Start() => transform.GetChild(0).GetComponent<TextMeshPro>().text = string.Concat(AssociatedNumber);

    public void fall() => enabled = false;
    public void unfall() => enabled = true;

    public void OnTriggerEnter2D(Collider2D other) => other.gameObject.SetActive(enabled);
}
