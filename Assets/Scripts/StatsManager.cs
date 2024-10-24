using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text time;

    void Start()
    {
        TimeManager.timeIsRunning = false;
    }

    void Update()
    {
        scoreText.text = "Final Score: " + Mathf.Round(GameManager.Score);
        time.text = "Time Taken: " + TimeManager.timeRemaining.ToString("F3") + " Seconds";
    }



}

