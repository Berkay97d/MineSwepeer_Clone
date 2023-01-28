using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool isMine { private get; set; }

    private int neighborMineCount;

    private bool isOpened = false;

    private bool isFlagged = false;

    private List<Tile> allNeighbors = new List<Tile>();


    private void Start()
    {
        DefineNeighbors();
        DefineTileType();
        FillInside();
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
        if (isFlagged || GameController.isLost || GameController.isWin) return;

        Open(this);
    }
       
    private void OnMouseOver()
    {
        if (isFlagged || GameController.isLost || GameController.isWin) return;
        
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
            GameController.isLost = true;
            return;
        }
        
        if (neighborMineCount != 0) return; //KOMŞULARIMDA MAYIN VARSA KAPAT

        foreach (var neighbor in allNeighbors)  //KOMŞULARIMDA MAYIN YOKSA VE BEN BİR MAYIN DEĞİLSEM BÜTÜN KOMŞULARIMI AÇ
        {
            if (neighbor == null || neighbor.isOpened)
            {
                continue;   //NULL OLAN VE AÇILMIŞ OLAN KOMŞULARI AÇMAYA ÇALIŞMA
            }
            neighbor.Open(neighbor);
        }
        
    }

    private void FillInside()
    {
        if (this.tileType == TileType.Empty)
        {
            insideRenderer.sprite = insideSprites[8];
        }

        if (tileType == TileType.Mine)
        {
            insideRenderer.sprite = insideSprites[9];
        }

        insideRenderer.sprite = insideSprites[neighborMineCount - 1];
    }

    private void AdjustDefaultSprite()
    {
        defaultRenderer.sprite = defaultSprites[isFlagged ? 1 : 0];
    }
    
}
