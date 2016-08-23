using System.Collections.Generic;
using UnityEngine;

public class GameParameters : Singleton<GameParameters>
{
    [Header("Game Parameters")]
    [Range(2, 10)]
    public int boardSize;
    [Range(1, 5)]
    public int handSize;
    public List<ColorType> allowedColorTypes;
    public int scorePerExplodedTileBlock;

    [Header("Scene Parameters")]
    public int tileSize;
}