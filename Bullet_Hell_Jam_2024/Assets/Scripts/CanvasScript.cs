using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public static CanvasScript instance;

    [SerializeField] Image[] lifes;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI time;

    private void Awake()
    {
        instance = this;
    }

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
    public void UpdateTime(float newTime)
    {
        float sec = newTime % 60;
        float min = Mathf.Floor(newTime / 60);
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
    public void UpdateWeapon1Gauge(int weaponIndex, float gauge1, float gauge2, float gauge3)
    {
        weaponGauges[weaponIndex][0].fillAmount = gauge1;
        weaponGauges[weaponIndex][1].fillAmount = gauge2;
        weaponGauges[weaponIndex][2].fillAmount = gauge3;
    }
}
