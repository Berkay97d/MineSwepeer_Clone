using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum GameDifficulity
{
    EASY,
    MİD,
    HARD
}

public class Datas : MonoBehaviour
{
    [Header("UI REFERENCES")]
    [SerializeField] private TMP_Dropdown difficulity;
    [SerializeField] private TMP_InputField rows;
    [SerializeField] private TMP_InputField colums;
    
    private int rowSize;
    private int columSize;
    
    private readonly int maxSize = 30;
    private readonly int minSize = 4;
    
    private GameDifficulity gameDifficulity;
    
    private struct GameData
    {
        public int RowSize;
        public int ColumSize;
        public GameDifficulity GameDifficulity;
    }

    
    
    public void StartGame()
    {
        CheckGameDatas();
        Debug.Log("Row size: " + rowSize);
        Debug.Log("Colum size: " + columSize);
        Debug.Log("Difficultiy size: " + gameDifficulity);
    }
    
    private GameData CheckGameDatas()
    {
        GameData gameData = new GameData();
        gameData.RowSize = RowSizeData();
        gameData.ColumSize = ColumSizeData();
        gameData.GameDifficulity = DifficulityData();

        return gameData;
    }
    
    private int RowSizeData()
    {
        if (IsValueEntered(rows))
        {
            rowSize = Mathf.Clamp(Convert.ToInt32(rows.text), minSize, maxSize); //Convert.ToInt32(rows.text);
            rows.text = rowSize.ToString();
        }
        else
        {
            rowSize = Convert.ToInt32(rows.placeholder.GetComponent<TMP_Text>().text);
        }
        
        return rowSize;
    }
    
    private int ColumSizeData()
    {
        if (IsValueEntered(colums))
        {
            columSize = Mathf.Clamp(Convert.ToInt32(colums.text), minSize, maxSize); //Convert.ToInt32(colums.text);
            colums.text = columSize.ToString();
        }
        else
        {
            columSize = Convert.ToInt32(colums.placeholder.GetComponent<TMP_Text>().text);
        }
        
        return columSize;
    }

    private GameDifficulity DifficulityData()
    {
        var selectedDif = difficulity.value;

        gameDifficulity = selectedDif switch
        {
            0 => GameDifficulity.EASY,
            1 => GameDifficulity.MİD,
            _ => GameDifficulity.HARD
        };

        return gameDifficulity;
    }
    
    private bool IsValueEntered(TMP_InputField inputField)
    {
        return inputField.text != "";
    }
    
    
}
