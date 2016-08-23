using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject tilePrefab;

    [Header("References")]
    [SerializeField]
    private RectTransform rectTransform;

    private GameController gameController;
    private Tile[,] tiles;

    void Awake()
    {
        tiles = new Tile[0, 0];
    }

    public void Initialize(GameController gameController)
    {
        this.gameController = gameController;

        int tileSize = GameParameters.Instance.tileSize;
        int boardSize = GameParameters.Instance.boardSize;

        rectTransform.sizeDelta = new Vector2(tileSize * boardSize, tileSize * boardSize);
        tiles = new Tile[boardSize, boardSize];

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                GameObject tileGO = Instantiate(tilePrefab) as GameObject;
                tileGO.name = "Tile [" + i + "," + j + "]";

                RectTransform tileRectTransform = tileGO.GetComponent<RectTransform>();
                Tile tile = tileGO.GetComponent<Tile>();

                tileRectTransform.SetParent(rectTransform, false);

                tiles[i, j] = tile;
            }
        }

        SetTilesNeighbourReferences();
    }

    public void Clean()
    {
        foreach (Tile tile in tiles)
        {
            Destroy(tile.gameObject);
        }
    }

    public bool CheckSpaceForPieceType(PieceType pieceType)
    {
        bool isEnoughSpace = false;

        switch (pieceType)
        {
            case PieceType.Horizontal:
                isEnoughSpace = CheckEnoughSpaceForDirection(Direction.East);
                break;

            case PieceType.Vertical:
                isEnoughSpace = CheckEnoughSpaceForDirection(Direction.South);
                break;
        }

        return isEnoughSpace;
    }

    private bool CheckEnoughSpaceForDirection(Direction direction)
    {
        bool isEnoughSpace = false;
        int boardSize = GameParameters.Instance.boardSize;

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Tile tile0 = tiles[i, j];
                Tile tile1 = tile0.GetNeighbourTile(direction);

                if (tile1 != null && tile0.Block == null && tile1.Block == null)
                {
                    isEnoughSpace = true;
                }
            }
        }

        return isEnoughSpace;
    }

    public void CheckHandleTilesToExplode()
    {
        int boardSize = GameParameters.Instance.boardSize;
        HashSet<Tile> tilesToExplode = new HashSet<Tile>();

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Tile tile0 = tiles[i, j];

                if (CheckTilesExplodeInDirectionLegal(tile0, Direction.East))
                {
                    tilesToExplode.Add(tile0);
                    tilesToExplode.Add(tile0.GetNeighbourTile(Direction.East));
                    tilesToExplode.Add(tile0.GetNeighbourTile(Direction.East).GetNeighbourTile(Direction.East));
                }

                if (CheckTilesExplodeInDirectionLegal(tile0, Direction.South))
                {
                    tilesToExplode.Add(tile0);
                    tilesToExplode.Add(tile0.GetNeighbourTile(Direction.South));
                    tilesToExplode.Add(tile0.GetNeighbourTile(Direction.South).GetNeighbourTile(Direction.South));
                }
            }
        }

        foreach (Tile tileToExplode in tilesToExplode)
        {
            ExplodeTileBlock(tileToExplode);
        }
    }

    public void ExplodeTileBlock(Tile tileToExplode)
    {
        foreach (Tile tile in tiles)
        {
            if (tile == tileToExplode)
            {
                tile.Block.DestroyMe();
                tile.Block = null;

                gameController.OnTileBlockExploded();

                break;
            }
        }
    }

    private bool CheckTilesExplodeInDirectionLegal(Tile tile0, Direction direction)
    {
        bool isLegal = false;

        Tile tile1 = null;
        Tile tile2 = null;

        if (tile0.GetNeighbourTile(direction) != null)
        {
            tile1 = tile0.GetNeighbourTile(direction);

            if (tile1.GetNeighbourTile(direction) != null)
            {
                tile2 = tile1.GetNeighbourTile(direction);
            }
        }

        if (tile0 != null && tile1 != null && tile2 != null)
        {
            if (tile0.Block != null && tile1.Block != null && tile2.Block != null)
            {
                if (tile0.Block.ColorType == tile1.Block.ColorType && tile1.Block.ColorType == tile2.Block.ColorType)
                {
                    isLegal = true;
                }
            }
        }

        return isLegal;
    }

    private void SetTilesNeighbourReferences()
    {
        int boardSize = GameParameters.Instance.boardSize;

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Tile tile = tiles[i, j];

                Tile northNeighbour = null;
                Tile eastNeighbour = null;
                Tile southNeighbour = null;
                Tile westNeighbour = null;

                if (i > 0)
                {
                    northNeighbour = tiles[i - 1, j];
                }

                if (j < boardSize - 1)
                {
                    eastNeighbour = tiles[i, j + 1];
                }

                if (i < boardSize - 1)
                {
                    southNeighbour = tiles[i + 1, j];
                }

                if (j > 0)
                {
                    westNeighbour = tiles[i, j - 1];
                }

                tile.SetNeighbourTile(Direction.North, northNeighbour);
                tile.SetNeighbourTile(Direction.East, eastNeighbour);
                tile.SetNeighbourTile(Direction.South, southNeighbour);
                tile.SetNeighbourTile(Direction.West, westNeighbour);
            }
        }
    }
}