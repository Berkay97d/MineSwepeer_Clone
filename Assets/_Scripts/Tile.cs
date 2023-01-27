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


    private void Start()
    {
        DefineTileType();
    }

    private Tile[] Neighbors()
    {
        var myNeighbors = new[] {N_Left, N_Right, N_Down, N_Up, N_DownLeft, N_UpLeft, N_DownRight, N_UpRight};
        return myNeighbors;
    }

    private void DefineTileType()
    {
        if (isMine)
        {
            tileType = TileType.Mine;
            return;
        }

        
        foreach (var neighbor in Neighbors())
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
        Debug.Log("Cliced to " + this.name);
        tile.transform.position = new Vector3(100, 100, 0);
        
        if (neighborMineCount == 0)
        {
            foreach (var neighbor in Neighbors())
            {
                if (neighbor == null)
                {
                    continue;
                }
                
                Open(neighbor);
            }
        }
    }
}
