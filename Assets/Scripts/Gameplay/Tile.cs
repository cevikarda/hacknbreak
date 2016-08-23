using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Block block;
    private Dictionary<Direction, Tile> neighbourTiles;

    void Awake()
    {
        neighbourTiles = new Dictionary<Direction, Tile>();
        neighbourTiles[Direction.North] = null;
        neighbourTiles[Direction.East] = null;
        neighbourTiles[Direction.South] = null;
        neighbourTiles[Direction.West] = null;
    }

    public Tile GetNeighbourTile(Direction direction)
    {
        return neighbourTiles[direction];
    }

    public void SetNeighbourTile(Direction direction, Tile neighbourTile)
    {
        neighbourTiles[direction] = neighbourTile;
    }

    public Block Block
    {
        get
        {
            return block;
        }
        set
        {
            block = value;
        }
    }
}