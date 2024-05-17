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
}
