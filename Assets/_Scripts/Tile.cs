using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TileType
{
    Empty,
    Number,
    Mine
}

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite[] insideSprites;
    [SerializeField] private Sprite[] defaultSprites;
    [SerializeField] private SpriteRenderer insideRenderer;
    [SerializeField] private SpriteRenderer defaultRenderer;
    
    
    public TileType tileType;
    
    public Tile N_UpLeft { get; set; }
    public Tile N_Up { get; set; }
    public Tile N_UpRight { get; set; }
    public Tile N_Right { get; set; }
    public Tile N_DownRight { get; set; }
    public Tile N_Down { get; set; }
    public Tile N_DownLeft { get; set; }
    public Tile N_Left { get; set; }

    public bool isMine { get; set; }

    private int neighborMineCount;

    public bool isOpened = false;

    private bool isFlagged = false;

    private List<Tile> allNeighbors = new List<Tile>();

    public static bool OpenedForLost = false;

    private void Update()
    {
        if (GameController.GameState == GameState.Lost)
        {
            if (isOpened)
            {
                return;
            }
            Open(this);
            OpenedForLost = true;
        }
    }

    private void Start()
    {
        OpenedForLost = false;
        DefineNeighbors();
        DefineTileType();
        FillInside();

        if (!isMine)
        {
            GameController.NonMineTiles.Add(this);
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    private void DefineNeighbors()
    {
        allNeighbors.AddAll(N_Left, N_Right, N_Down, N_Up, N_DownLeft, N_UpLeft, N_DownRight, N_UpRight);
    }

    private void DefineTileType()
    {
        if (isMine)
        {
            tileType = TileType.Mine;
            return;
        }

        
        foreach (var neighbor in allNeighbors)
        {
            if (neighbor == null)
            {
                continue;
            }
            
            if (neighbor.isMine)
            {
                neighborMineCount++;
            }
        }

        if (neighborMineCount != 0)
        {
            tileType = TileType.Number;
        }
        
    }

    private void OnMouseDown()
    {
        if (isFlagged || GameController.GameState == GameState.Lost || GameController.GameState == GameState.Win) return;

        Open(this);
    }
       
    private void OnMouseOver()
    {
        if (GameController.GameState == GameState.Lost || GameController.GameState == GameState.Win) return;
        
        if (Input.GetMouseButtonUp(1))
        {
            isFlagged = !isFlagged;
            AdjustDefaultSprite();
        }
    }

    private void Open(Tile tile)
    {
        insideRenderer.sortingOrder = 2; //TIKLADIĞIMI AÇ
        tile.isOpened = true;
            
        if (tile.isMine) {      //TIKLADIĞIM MAYINSA LOST 
            GameController.GameState = GameState.Lost;
            return;
        }
        
        GameController.CheckIsWin();
        if (neighborMineCount != 0) return; //KOMŞULARIMDA MAYIN VARSA KAPAT

        foreach (var neighbor in allNeighbors)  //KOMŞULARIMDA MAYIN YOKSA VE BEN BİR MAYIN DEĞİLSEM BÜTÜN KOMŞULARIMI AÇ
        {
            if (neighbor == null || neighbor.isOpened)
            {
                continue;   //NULL OLAN VE AÇILMIŞ OLAN KOMŞULARI AÇMAYA ÇALIŞMA
            }
            neighbor.Open(neighbor);
        }
        
        GameController.CheckIsWin();
    }

    private void FillInside()
    {
        if (this.tileType == TileType.Empty)
        {
            insideRenderer.sprite = insideSprites[8];
            return;
        }

        if (tileType == TileType.Mine)
        {
            insideRenderer.sprite = insideSprites[9];
            return;
        }

        insideRenderer.sprite = insideSprites[neighborMineCount - 1];
    }

    private void AdjustDefaultSprite()
    {
        defaultRenderer.sprite = defaultSprites[isFlagged ? 1 : 0];
    }
    
    
}
