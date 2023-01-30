using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState
{
    Playing,
    Win,
    Lost
}
public class GameController : MonoBehaviour
{
    public static GameState GameState;
    public static List<Tile> NonMineTiles = new List<Tile>(); 
    private void Start()
    {
        GameState = GameState.Playing;
    }

    private void Update()
    {
        Debug.Log(GameState);
    }

    public static void CheckIsWin()
    {
        if (Tile.OpenedForLost)
        {
            return;
        }
        
        foreach (var tile in NonMineTiles)
        {
            if (!tile.isOpened)
            {
                Debug.Log(tile.name);
                return;
            }
        }

        GameState = GameState.Win;
    }

    public static void RestartGame()
    {
        if (GameState != GameState.Playing)
        {
            NonMineTiles.Clear();
            SceneManager.LoadScene(0);
        }
    }
}
