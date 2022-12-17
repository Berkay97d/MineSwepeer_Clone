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
    [SerializeField] private TMP_Dropdown difficulity;
    [SerializeField] private TMP_InputField rows;
    [SerializeField] private TMP_InputField colums;

    private int rowSize;
    private int columSize;
    private GameDifficulity gameDifficulity;

    private void Update()
    {
        
    }

    private bool IsValueEntered(TMP_InputField inputField)
    {
        return rows.text != "";
    }

    public void StartGame()
    {
        if (IsValueEntered(rows))
        {
            rowSize = Convert.ToInt32(rows.text);
        }
        else
        {
            rowSize = Convert.ToInt32(rows.placeholder.GetComponent<TMP_Text>().text);
        }
        
        if (IsValueEntered(colums))
        {
            columSize = Convert.ToInt32(colums.text);
        }
        else
        {
            columSize = Convert.ToInt32(colums.placeholder.GetComponent<TMP_Text>().text);
        }
        
        
    }
}
