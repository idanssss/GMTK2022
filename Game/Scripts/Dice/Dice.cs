using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private float rollTime = 2f;
    [SerializeField] private float rollsPerSecond = 4f;

    public TileManager tm;
    
    public int Value { get; private set; }
    
    public void Roll(Stopwatch stopwatch)
    {
        StartCoroutine(RollCoroutine(stopwatch));
    }

    private IEnumerator RollCoroutine(Stopwatch stopwatch)
    {
        float timeBetween = rollTime / 2;
        int random = Random.Range(1, 7);
        for (int i = 0; i < rollTime * rollsPerSecond; i++)
        {
            int newRand = Random.Range(1, 7);
            while (newRand == random)
                newRand = Random.Range(1, 7);
            
            random = newRand;
            rend.sprite = DiceExtensions.GetDice(random);

            timeBetween = Mathf.Lerp(timeBetween, 0.1f, 0.5f);
            yield return new WaitForSeconds(timeBetween);
        }
        
        while (tm.dropped.Contains(random))
            random = Random.Range(1, 7);
        
        Value = random;
        rend.sprite = DiceExtensions.GetDice(random);   
        tm.Drop(Value);

        stopwatch.ResetTime();
        stopwatch.count = true;
        
        if (tm.dropped[5] != 0)
            tm.ResetTiles();
    }
}

public static class DiceExtensions
{
    public const string root = "Assets/GMTK2022/Game/Sprites/Dice/Dice/Dice Prefab/Dice_";
    
    public static Sprite GetDice(int digit)
    {
        Assert.IsFalse(digit < 1 || digit > 6, "Digit out of bounds");
        digit--;
        
        string name = root + digit + ".prefab";
        var p = AssetDatabase.LoadAssetAtPath<GameObject>(name);
        var sprite = p.GetComponent<SpriteRenderer>().sprite;
        
        return sprite;
    }
}
