using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("References")]
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Block block0;
    [SerializeField]
    private Block block1;

    [Header("Parameters")]
    [SerializeField]
    private PieceType pieceType;
    [SerializeField]
    private Direction block1Direction;

    private Hand hand;

    public void Initialize(Hand hand)
    {
        this.hand = hand;

        AssignRandomColorToBlock(block0);
        AssignRandomColorToBlock(block1);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PointerEventData block0EventData = new PointerEventData(EventSystem.current);
        block0EventData.position = block0.GetComponent<RectTransform>().position;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(block0EventData, raycastResults);

        bool isTileFound = false;
        foreach (RaycastResult raycastResult in raycastResults)
        {
            Tile tile = raycastResult.gameObject.GetComponent<Tile>();

            if (CheckPutOnTileLegal(tile))
            {
                isTileFound = true;
                HandleTileHit(tile);
            }
        }

        if (isTileFound == false)
        {
            PutPieceBack();
        }
    }

    private void AssignRandomColorToBlock(Block block)
    {
        int randomIndex = Random.Range(0, GameParameters.Instance.allowedColorTypes.Count);
        ColorType colorType = GameParameters.Instance.allowedColorTypes[randomIndex];

        block.Initialize(colorType);
    }

    private bool CheckPutOnTileLegal(Tile tile)
    {
        bool isLegal = false;

        if (tile != null)
        {
            Tile neighbourTile = tile.GetNeighbourTile(block1Direction);

            if (neighbourTile != null)
            {
                if (tile.Block == null && neighbourTile.Block == null)
                {
                    isLegal = true;
                }
            }
        }

        return isLegal;
    }

    private void HandleTileHit(Tile tile)
    {
        if (tile.Block == null)
        {
            PutBlocksOnTiles(tile);
            hand.OnPiecePutOnBoard(this);
            DestroyMe();
        }
        else
        {
            PutPieceBack();
        }
    }

    private void PutPieceBack()
    {
        AudioManager.Instance.PlayWrongMoveSFX();

        rectTransform.localPosition = Vector2.zero;
    }

    private void PutBlocksOnTiles(Tile tile0)
    {
        tile0.Block = block0;
        Tile tile1 = tile0.GetNeighbourTile(block1Direction);
        tile1.Block = block1;

        RectTransform block0RectTransform = block0.GetComponent<RectTransform>();
        RectTransform block1RectTransform = block1.GetComponent<RectTransform>();

        block0RectTransform.SetParent(tile0.GetComponent<RectTransform>(), false);
        block1RectTransform.SetParent(tile1.GetComponent<RectTransform>(), false);

        block0RectTransform.localPosition = Vector2.zero;
        block1RectTransform.localPosition = Vector2.zero;
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }

    public PieceType PieceType
    {
        get
        {
            return pieceType;
        }
    }
}