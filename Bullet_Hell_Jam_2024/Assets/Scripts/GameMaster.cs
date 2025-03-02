using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public bool isGameStarted = false;

    public Vector2 arenaSize;

    private float score;
    private int stepBeforeStart = 5;
    private float scorePerSec = 10;
    private float survivalBonus = 1000;

    public float Score
    {
        get { return score; }
        set
        {
            if (isGameStarted)
            {
                score = value;
                try
                {
                    CanvasScript.instance.UpdateScore((int)score);
                }
                catch (Exception e) { Debug.LogError("Score UI update error with: " + e.Message); }
            }
        }
    }

    // The maximum time for the game in seconds
    [SerializeField]
    public int maxTime = 120;
    private float currTimer;
    public float CurrTimer
    {
        get { return currTimer; }
        set
        {
            currTimer = Mathf.Max(value, 0);
            try
            {
                CanvasScript.instance.UpdateTime(currTimer);
                CanvasScript.instance.UpdateTimeFill(Mathf.Round(currTimer) / maxTime);
            }
            catch (Exception e) { Debug.LogError("Timer UI update error with: " + e.Message); }
        }
    }

    [SerializeField]
    private GameObject controlCanvas;
    [SerializeField] GameObject winingScreenClear;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Score = 0;
        CurrTimer = maxTime;
        stepBeforeStart = 5;
    }

    private void LateUpdate()
    {
        // Reduce & update timer here
        if (isGameStarted)
        {
            CurrTimer -= Time.deltaTime;
            Score += Time.deltaTime * scorePerSec;
            if (CurrTimer <= 0)
            {
                Debug.Log("Time is over!");
                GameWin();
            }
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        EnemyGenerationManager.instance.isActive = true;
        Score = 0;
        CurrTimer = maxTime;
        controlCanvas.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("Game over");
        CanvasScript.instance.ShowGameOverScreen();
        EnemyGenerationManager.instance.isActive = false;
        isGameStarted = false;
    }

    public void GameWin()
    {
        Debug.Log("Game Finished");
        Instantiate(winingScreenClear, Vector3.zero, Quaternion.identity);
        Score += survivalBonus;
        CanvasScript.instance.ShowWiningScreen();
        EnemyGenerationManager.instance.isActive = false;
        isGameStarted = false;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DestroyStartEnemey()
    {
        stepBeforeStart--;
        if (stepBeforeStart == 0)
        {
            StartGame();
        }
    }
}
