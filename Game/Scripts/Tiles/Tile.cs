using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public Dictionary<int, Color> dieColors = new() {
        { 1, Color.red },
        { 2, Color.green },
        { 3, Color.blue },
        { 4, Color.yellow },
        { 5, Color.magenta },
        { 6, Color.cyan }
    }; 

    public int AssociatedNumber { get; private set; }

    private TextMeshPro _text;
    private TextMeshPro Text
    {
        get
        {
            _text ??= GetComponentInChildren<TextMeshPro>();
            return _text;
        }
    }

    private SpriteRenderer _rend;
    private SpriteRenderer Rend
    {
        get
        {
            _rend ??= GetComponent<SpriteRenderer>();
            return _rend;
        }
    }

    private bool _exists = true;

    public bool Exists
    {
        get => _exists;
        private set
        {
            if (_exists == value) return;

            Rend.enabled = value;
            Text.enabled = value;
            
            _exists = value;
        }
    }

    public void UpdateUI()
    {
        Text.text = AssociatedNumber.ToString();
        Rend.color = dieColors[AssociatedNumber];
    }

    public void SetAssociatedNumber(int number) => AssociatedNumber = number;
    public void Drop() => Exists = false;
    public void ResetTile() => Exists = true;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Exists) return; 
        other.gameObject.SetActive(false);
    }
}
