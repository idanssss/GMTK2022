using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] private Dice dice;
    [SerializeField] public float time;
    [SerializeField] public float timeToStart;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private List<Sprite> states = new List<Sprite>();

    public bool count = true;

    public void ResetTime() => time = timeToStart;

    private void Start() => ResetTime();

    private void Update()
    {
        if (!count) return;
        
        time -= Time.deltaTime;
        // timerText.text = time.ToString("0.00");

        float eighth = timeToStart / 8;
        int state = (int)(time / eighth);
        
        rend.sprite = states[state];

        if (time <= 0)
        {
            // timerText.text = "";
            rend.sprite = states[7];
            dice.Roll(this);
            count = false;
        }
    }
}
