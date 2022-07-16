using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public Dictionary<int, Color> dieColors = new Dictionary<int, Color>() {
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

            if (Rend)
                Rend.enabled = value;
            
            if (Text)
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
    public void Drop(float duration, float strength)
    {
        // StartCoroutine(FlashCoroutine(times));
        transform.Shake(duration, strength, () => Exists = false);
    }

    private IEnumerator FlashCoroutine(int times)
    {
        const float minAlpha = 0.2f;
        const float maxAlpha = 1f;
        const float maxDelta = 0.05f;

        for (int i = 0; i < times; i++)
        {
            while (Rend.color.a > minAlpha)
            {
                Color c = Rend.color;
                float newAlpha = Mathf.MoveTowards(c.a, minAlpha, maxDelta);

                Rend.color = new Color(c.r, c.g, c.b, newAlpha);
                yield return new WaitForFixedUpdate();
            }
            
            while (Rend.color.a < maxAlpha)
            {
                Color c = Rend.color;
                float newAlpha = Mathf.MoveTowards(c.a, maxAlpha, maxDelta);

                Rend.color = new Color(c.r, c.g, c.b, newAlpha);
                yield return new WaitForFixedUpdate();
            }
        }
        
        Exists = false;
    }

    public void ResetTile() => Exists = true;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Exists) return; 
        other.gameObject.SetActive(false);
    }
}
