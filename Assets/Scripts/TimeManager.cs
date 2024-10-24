using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static bool timeIsRunning = true;
    public static float timeRemaining = 0;
    public TMP_Text timeText;
  
    
    
    void Start()
    {
        //timeRemaining = GameManager.GameTime;
        timeIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsRunning)
        {
            if(timeRemaining >= 0)
            {
                timeRemaining += Time.deltaTime;
                DisplayTime(timeRemaining);
            }
        }
        
    }

    void DisplayTime (float timeDisplay)
    {
        timeDisplay++;
        float minutes = Mathf.FloorToInt(timeDisplay / 60);
        float seconds = Mathf.FloorToInt(timeDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    
}
