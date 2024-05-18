using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public bool isGameStarted = false;

    public Vector2 arenaSize;

    private float score;
    public float Score
    {
        get { return score; }
        set
        {
            score = value;
            try
            {
                CanvasScript.instance.UpdateScore((int)score);
            }
            catch (Exception e) { Debug.LogError("Score UI update error with: " + e.Message); }
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
            currTimer = value;
            try
            {
                CanvasScript.instance.UpdateTime(currTimer);
            }
            catch (Exception e) { Debug.LogError("Timer UI update error with: " + e.Message); }
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Score = 0;
        CurrTimer = maxTime;
    }

    private void LateUpdate()
    {
        // Reduce & update timer here
        if (isGameStarted)
        {
            CurrTimer -= Time.deltaTime;

            if (CurrTimer <= 0)
            {
                Debug.Log("Time is over!");
                GameOver();
            }
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        EnemyGenerationManager.instance.isActive = true;
        Score = 0;
        CurrTimer = maxTime;
    }

    public void GameOver()
    {
        Debug.Log("Game over");
        CanvasScript.instance.ShowResultScreen();
        isGameStarted = false;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
