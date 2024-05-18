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
    [SerializeField] Image[] weaponSelections;

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
    [SerializeField] Image[] weapon1Gauges;
    [SerializeField] Image[] weapon2Gauges;
    [SerializeField] Image[] weapon3Gauges;
    /// <summary>
    /// Update the enemey gauges UI of a particular weapon
    /// </summary>
    /// <param name="weaponIndex">Index of weapon, 0, 1, 2</param>
    /// <param name="gauge1">Value of drone gauge 1, must be between 0 and 1 (inclusive)</param>
    /// <param name="gauge2">Value of shooter gauge 1, must be between 0 and 1 (inclusive)</param>
    /// <param name="gauge3">Value of tank gauge 1, must be between 0 and 1 (inclusive)</param>
    public void UpdateWeaponGauge(int weaponIndex, float gauge1, float gauge2, float gauge3)
    {
        switch (weaponIndex)
        {
            case 0:
                weapon1Gauges[0].fillAmount = gauge1;
                weapon1Gauges[1].fillAmount = gauge2;
                weapon1Gauges[2].fillAmount = gauge3;
                break;
            case 1:
                weapon2Gauges[0].fillAmount = gauge1;
                weapon2Gauges[1].fillAmount = gauge2;
                weapon2Gauges[2].fillAmount = gauge3;
                break;
            case 2:
                weapon3Gauges[0].fillAmount = gauge1;
                weapon3Gauges[1].fillAmount = gauge2;
                weapon3Gauges[2].fillAmount = gauge3;
                break;
            default:
                Debug.LogError("CanvasScript: Bad weaponIndex");
                break;
        }
    }

    /// <summary>
    /// Update selection UI for weapon
    /// </summary>
    /// <param name="weaponIndex">Index of weapon, 0, 1, 2</param>
    public void UpdateSelectedWeapon(int weaponIndex)
    {
        if (weaponIndex  >= 0 && weaponIndex < weaponSelections.Length)
        {
            for (int i = 0;i < weaponSelections.Length; ++i)
            {
                weaponSelections[i].enabled = i == weaponIndex;
            }
        }
        else
        {
            Debug.LogError("CanvasScript: Bad weaponIndex");
        }
    }
}
