using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public static CanvasScript instance;

    private float scoreDisplayCap = 99999;

    [SerializeField] Image[] lifes;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI gameOverScore;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gameWinScreen;
    [SerializeField] TextMeshProUGUI gameWinScore;
    [SerializeField] Image[] weaponSelections;

    private void Awake()
    {
        instance = this;
        gameOverScreen.SetActive(false);
    }

    /// <summary>
    ///  Update score UI to a new value
    /// </summary>
    /// <param name="newScore">The score you want to display</param>
    public void UpdateScore(float newScore)
    {

        float displayed = Mathf.Clamp(newScore, 0, scoreDisplayCap);
        score.text = displayed.ToString().PadLeft(5, '0'); ;
    }
    /// <summary>
    /// Update the time UI to a new value
    /// </summary>
    /// <param name="newTime">Time in seconds</param>
    public void UpdateTime(float newTime)
    {
        float sec = newTime % 60;
        float min = Mathf.Floor(newTime / 60);
        string combineTime = min.ToString().PadLeft(2, '0') + ':' + sec.ToString("00.00");
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
    /// <summary>
    /// Display the result screen after game over
    /// </summary>
    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        gameOverScore.text = score.text;
    }

    /// <summary>
    /// Display the result screen after game over
    /// </summary>
    public void ShowWiningScreen()
    {
        gameWinScreen.SetActive(true);
        gameWinScore.text = score.text;
    }

    /// <summary>
    /// Called when the retry button in result screen is clicked
    /// </summary>
    public void OnClickRetry()
    {
        GameMaster.instance.Restart();
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

    private void SetNewProgress(Image image, Image start, Image end, float progress)
    {
        Vector3 newPos = Vector3.Lerp(start.transform.localPosition, end.transform.localPosition, progress);
        //float newY = Mathf.Lerp(enemyStartY, enemyEndY, progress);
        //Vector3 oldPos = image.transform.localPosition;
        //pos.y = newY;
        //image.transform.localPosition = pos;
        image.transform.localPosition = newPos;
    }


    [SerializeField] Image[] weapon1Enemies;
    [SerializeField] Image[] weapon1Pos;
    [SerializeField] Image[] weapon2Enemies;
    [SerializeField] Image[] weapon2Pos;
    [SerializeField] Image[] weapon3Enemies;
    [SerializeField] Image[] weapon3Pos;
    /// <summary>
    /// Update the enemey progress bar based on given percentage
    /// </summary>
    /// <param name="weaponIndex">Weapon index, 0, 1, 2</param>
    /// <param name="droneProg">Number between 0 and 1 (inclusive)</param>
    /// <param name="shooterProg">Number between 0 and 1 (inclusive)</param>
    /// <param name="tankProg">Number between 0 and 1 (inclusive)</param>
    public void UpdateEnemeyProgress(int weaponIndex, float droneProg, float shooterProg, float tankProg)
    {
        switch (weaponIndex)
        {
            case 0:
                SetNewProgress(weapon1Enemies[0], weapon1Pos[0], weapon1Pos[1], droneProg);
                SetNewProgress(weapon1Enemies[1], weapon1Pos[0], weapon1Pos[1], shooterProg);
                SetNewProgress(weapon1Enemies[2], weapon1Pos[0], weapon1Pos[1], tankProg);
                break;
            case 1:
                SetNewProgress(weapon2Enemies[0], weapon2Pos[0], weapon2Pos[1], droneProg);
                SetNewProgress(weapon2Enemies[1], weapon2Pos[0], weapon2Pos[1], shooterProg);
                SetNewProgress(weapon2Enemies[2], weapon2Pos[0], weapon2Pos[1], tankProg);
                break;
            case 2:
                SetNewProgress(weapon3Enemies[0], weapon3Pos[0], weapon3Pos[1], droneProg);
                SetNewProgress(weapon3Enemies[1], weapon3Pos[0], weapon3Pos[1], shooterProg);
                SetNewProgress(weapon3Enemies[2], weapon3Pos[0], weapon3Pos[1], tankProg);
                break;
            default:
                Debug.LogError("CanvasScript: Bad weaponIndex");
                break;
        }
    }
}
