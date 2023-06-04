using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Min(0)]
    [SerializeField] int includedMilliseconds = 1;
    private int totalTime;
    private TextMeshProUGUI timeText;
    private StringBuilder stringBuilder = new StringBuilder();
    private char separator = ':';

    private void Start()
    {
        timeText = GetComponentInChildren<TextMeshProUGUI>();
    }


    public void SetTime(float remainingTime)
    {
        if (remainingTime <= 0) {
            timeText.text = "0:00:0";
        
        }
        int minutes = (int)(remainingTime / 60);
        int seconds = (int)remainingTime - minutes * 60;
        int milliseconds = (int)((remainingTime - (int)remainingTime) * Mathf.Pow(10, includedMilliseconds));
        stringBuilder.Clear();
        stringBuilder.Append(minutes);
        stringBuilder.Append(separator);
        stringBuilder.Append(seconds);
        stringBuilder.Append(separator);
        stringBuilder.Append(milliseconds);

        timeText.text = stringBuilder.ToString();
    }

}
