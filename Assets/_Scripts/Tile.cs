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
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    
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
        Open(this);
    }

    private void Open(Tile tile)
    {
        spriteRenderer.sortingOrder = 2; //TIKLADIĞIMI AÇ
        tile.isOpened = true;
            
        if (tile.isMine) {      //TIKLADIĞIM MAYINSA LOST 
            //TODO GAME OVER
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
            spriteRenderer.sprite = sprites[8];
        }

        if (tileType == TileType.Mine)
        {
            spriteRenderer.sprite = sprites[9];
        }

        spriteRenderer.sprite = sprites[neighborMineCount - 1];
    }
    
}
