using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FieldInitilazor : MonoBehaviour
{
    [SerializeField] private Tile tile;
    
    private GameData gameData;
    private  int xSize;
    private int ySize;
    private const int MinePercentageMultiplier = 10;
    private static Tile[,] Field;
    
    
    private void Awake()
    {
        gameData = Data.CurrentGameData;
        InitArea();
        DefineNeighbors();
    }
    
    
    private void InitArea()
    {
        xSize = gameData.RowSize;
        ySize = gameData.ColumSize;
        Field = new Tile[xSize, ySize];
        int RandomNum;
        
        
        for (int i = 0; i < gameData.RowSize; i++)
        {
            for (int j = 0; j < gameData.ColumSize; j++)
            {
                var myTile = Instantiate(tile, new Vector3(j, i, 0), Quaternion.identity);
                Field[i, j] = myTile;
                myTile.name = i + "x" + j;

                RandomNum = Random.Range(0, 100);
                if (RandomNum < MinePercencentage())
                {
                    myTile.isMine = true;
                }
            }
        }
    }

    private void DefineNeighbors()
    {
        for (int i = 0; i < gameData.RowSize; i++)
        {
            for (int j = 0; j < gameData.ColumSize; j++)
            {
                Tile myTile;
                myTile = Field[i, j];
                
                try
                {
                    myTile.N_Left = Field[i-1, j];
                    
                }
                catch (IndexOutOfRangeException e) { }

                try
                {
                    myTile.N_Right = Field[i+1, j];
                }
                catch (IndexOutOfRangeException e) { }
                
                try
                {
                    myTile.N_Up = Field[i, j+1];
                }
                catch (IndexOutOfRangeException e) { }
                
                try
                {
                    myTile.N_Down = Field[i, j-1];
                }
                catch (IndexOutOfRangeException e) { }
                
                try
                {
                    myTile.N_UpLeft = Field[i+1, j-1];
                }
                catch (IndexOutOfRangeException e) { }
                
                try
                {
                    myTile.N_UpRight = Field[i+1, j+1];
                }
                catch (IndexOutOfRangeException e) { }
                
                try
                {
                    myTile.N_DownLeft = Field[i-1, j-1];
                }
                catch (IndexOutOfRangeException e) { }
                
                try
                {
                    myTile.N_DownRight = Field[i-1, j+1];
                }
                catch (IndexOutOfRangeException e) { }
            }
        }
    }
    
    private int MinePercencentage()
    {
        if (gameData.GameDifficulity == GameDifficulity.EASY)
        {
            return MinePercentageMultiplier;
        }
        
        if (gameData.GameDifficulity == GameDifficulity.MİD)
        {
            return MinePercentageMultiplier * 2;
        }

        return MinePercentageMultiplier * 3;

    }
}




