using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private float rollTime = 2f;
    [SerializeField] private float rollsPerSecond = 4f;
    
    public TileManager tm;
    
    public int Value { get; private set; }
    
    public void Roll(Stopwatch stopwatch)
    {
        if (tm.dropped[5] != 0)
            tm.ResetTiles();

        StartCoroutine(RollCoroutine(stopwatch));
    }

    private IEnumerator RollCoroutine(Stopwatch stopwatch)
    {
        for (int i = 0; i < rollTime * rollsPerSecond; i++)
        {
            Value = Random.Range(1, 7);
            text.text = Value.ToString();
            
            yield return new WaitForSeconds(1 / rollsPerSecond);
        }
        
        int random = Random.Range(1, 7);
        while (tm.dropped.Contains(random))
            random = Random.Range(1, 7);
        
        Value = random;
        text.text = Value.ToString();
        tm.Drop(Value);

        stopwatch.ResetTime();
        stopwatch.count = true;
    }
}
