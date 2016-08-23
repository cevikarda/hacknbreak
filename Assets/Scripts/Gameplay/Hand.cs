using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject horizontalPiecePrefab;
    [SerializeField]
    private GameObject verticalPiecePrefab;

    [Header("References")]
    [SerializeField]
    private List<RectTransform> handSlots;

    private GameController gameController;
    private List<Piece> pieces;

    void Awake()
    {
        pieces = new List<Piece>();
    }

    public void Initialize(GameController gameController)
    {
        this.gameController = gameController;

        DrawNewHand();
    }

    public void Clean()
    {
        foreach (Piece piece in pieces)
        {
            Destroy(piece.gameObject);
        }
    }

    public void OnPiecePutOnBoard(Piece piece)
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i] == piece)
            {
                pieces.Remove(piece);
                break;
            }
        }

        CheckDrawNewHand();

        gameController.OnPiecePutOnBoard(piece);
    }

    private void DrawNewHand()
    {
        int handSize = GameParameters.Instance.handSize;

        pieces = new List<Piece>();

        for (int i = 0; i < handSize; i++)
        {
            Piece piece = InstantiateRandomPiece();
            pieces.Add(piece);
            piece.Initialize(this);
            RectTransform pieceRectTransform = piece.GetComponent<RectTransform>();
            pieceRectTransform.SetParent(handSlots[i], false);
        }
    }

    private Piece InstantiateRandomPiece()
    {
        int randomIndex = Random.Range(0, 2);

        GameObject pieceGO = null;

        switch (randomIndex)
        {
            case 0:
                pieceGO = Instantiate(horizontalPiecePrefab) as GameObject;
                break;

            case 1:
                pieceGO = Instantiate(verticalPiecePrefab) as GameObject;
                break;
        }

        Piece piece = pieceGO.GetComponent<Piece>();

        return piece;
    }

    private void CheckDrawNewHand()
    {
        if (pieces.Count == 0)
        {
            DrawNewHand();
        }
    }

    public List<Piece> Pieces
    {
        get
        {
            return pieces;
        }
    }
}