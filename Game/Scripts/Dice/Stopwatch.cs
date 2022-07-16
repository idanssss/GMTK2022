using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    [SerializeField]  private TextMeshPro timerText;
    [SerializeField] private Dice dice;
    [SerializeField]  private float time;
    [SerializeField]  private float timeToStart;
    
    private void Start()
    {
        time = timeToStart;    
    }
    
    private void Update()
    {
        time -= Time.deltaTime;
        timerText.text = time.ToString("0.00");
        
        if (time <= 0)
        {
            time = timeToStart;
            dice.Roll();
        }
    }
}
