using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Score = 0;
    }

    public void StartGame()
    {
        EnemyGenerationManager.instance.isActive = true;
        Score = 0;
    }

    public void GameOver()
    {
        Debug.Log("Game over");
    }
}
