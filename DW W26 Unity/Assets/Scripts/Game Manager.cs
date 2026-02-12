using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{
    // to do list //
    // 1. layout rounds and win conditions for the rounds 
    // 2. add a timer that will play each round 
    // 3. have a way to check if the 6 players have gotten to a win state or after the tijers up decide a winner


    [SerializeField] private GameObject roundSystem = null;
    [SerializeField] public int scoreT1;
    [SerializeField] public int scoreT2;
    [SerializeField] public int WinScore;
    Timer a;
    [SerializeField] string TimerUiAAAA;
    public void Awake()
    {
        round1Fight(scoreT1, scoreT2);
        Timeroutput(TimerUiAAAA, a);
        
    }

    public static void round1Fight(int a, int b)
    {
        a = 0;
        b = 0;

        return;

    }
    public string Timeroutput(string output, Timer DaTime)
    {
        //score += points;
        DaTime.TIMEPRIME(output);
        return output;
    }
    public bool CheckForWin(int a, int b, int c)
    {
        if (a == c)
        {
            Debug.Log("yooo team 1 wins");
            return true;
        }
        if (b == c)
        {
            Debug.Log("team 2 wins");
            return false;
        }
        else
        {
            return false;
        }
    }
}
