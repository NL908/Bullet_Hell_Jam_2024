using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public Vector2 arenaSize;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        EnemyGenerationManager.instance.isActive = true;
    }

    public void GameOver()
    {

    }
}
