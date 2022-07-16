using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    private int associatedNumber;

    private TextMeshPro _text;
    private TextMeshPro Text
    {
        get
        {
            _text ??= GetComponentInChildren<TextMeshPro>();
            return _text;
        }
    }

    private bool exists;

    public void UpdateUI() => Text.text = associatedNumber.ToString();

    public void SetAssociatedNumber(int number) => associatedNumber = number;
    public void Fall() => exists = false;
    public void ResetTile() => exists = true;

    public void OnTriggerEnter2D(Collider2D other) => other.gameObject.SetActive(exists);
}
