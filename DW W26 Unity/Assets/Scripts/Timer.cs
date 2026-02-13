using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
public class Timer : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI RoundTimerText;
   [SerializeField] float RoundTimer;
    // Update is called once per frame
    public void Update()
    {
        if (RoundTimer > 0)
        {
            RoundTimer -= Time.deltaTime;

        }
        else if (RoundTimer < 0)
        {
            RoundTimer = 0;
            // add game over here when done
            RoundTimerText.color = Color.red;

        }

        RoundTimer -= Time.deltaTime;
        int mins = Mathf.FloorToInt(RoundTimer/60);
        int secs = Mathf.FloorToInt(RoundTimer % 60);
        RoundTimerText.text = string.Format("{0:00}:{1:00}", mins, secs);



    }
    public string TIMEPRIME(string output)
    {


        string TimerOutput = RoundTimerText.text;
        return TimerOutput;
    }
}
