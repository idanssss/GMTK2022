using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public int AssociatedNumber { get; private set; }

    [SerializeField]
    private SpriteRenderer numberRend;

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
    private bool _shaking;

    public bool Shaking
    {
        get => _shaking;
        private set => _shaking = value;
    }

    public bool Exists
    {
        get => _exists;
        private set
        {
            if (_exists == value) return;

            if (Rend)
                Rend.enabled = value;
            
            if (numberRend)
                numberRend.enabled = value;
            
            
            
            _exists = value;
        }
    }

    private void Awake() {  Rend.color = Color.white; }

    public void UpdateUI() => numberRend.sprite = Numbers.GetSprite(AssociatedNumber);

    public void SetAssociatedNumber(int number) => AssociatedNumber = number;
    public void Drop(float duration, float strength)
    {
        Shaking = true;
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
}
