using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] private TextMeshPro timerText;
    [SerializeField] private Dice dice;
    [SerializeField] private float time;
    [SerializeField] private float timeToStart;

    public bool count = true;

    public void ResetTime() => time = timeToStart;

    private void Start() => ResetTime();

    private void Update()
    {
        if (!count) return;
        
        time -= Time.deltaTime;
        timerText.text = time.ToString("0.00");

        if (time <= 0)
        {
            timerText.text = "";
            dice.Roll(this);
            count = false;
        }
    }
}
