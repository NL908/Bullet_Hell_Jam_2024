using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] Image[] lifes;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI time;
    
    /// <summary>
    ///  Update score UI to a new value
    /// </summary>
    /// <param name="newScore">The score you want to display</param>
    public void UpdateScore(int newScore)
    {
        score.text = newScore.ToString();
    }
    /// <summary>
    /// Update the time UI to a new value
    /// </summary>
    /// <param name="newTime">Time in seconds</param>
    public void UpdateTime(double newTime)
    {
        double sec = newTime % 60;
        double min = Math.Floor(newTime / 60.0);
        Debug.Log("Parsed: " + min.ToString() + " " + sec.ToString());
        string combineTime = min.ToString().PadLeft(2, '0') + ':' + sec.ToString().PadLeft(2, '0');
        time.text = combineTime;
    }
    /// <summary>
    /// Update the number of life icon left, if it excess max UI amount, all will be displayed
    /// </summary>
    /// <param name="lifeLeft">greater or equal 0, </param>
    public void UpdateLife(int lifeLeft)
    {
        int left = Math.Min(lifeLeft, lifes.Length);
        for (int i = 0; i < lifes.Length; ++i)
        {
            lifes[i].enabled = i < left;
        }
    }
}
