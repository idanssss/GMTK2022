using System.Linq;
using TMPro;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private TileManager tm;

    public int Value { get; private set; }
    
    public void Roll()
    {
        if (tm.dropped[5] != 0)
            tm.ResetTiles();
        
        int random = Random.Range(1, 7);
        while (tm.dropped.Contains(random))
            random = Random.Range(1, 7);
        
        Value = random;
        text.text = Value.ToString();
        tm.Drop(Value);
        // transform.Rotate(0, 0, random * (360 / 6));
    }
}
