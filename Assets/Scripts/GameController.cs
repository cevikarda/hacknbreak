using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Board board;
    [SerializeField]
    private Hand hand;
    [SerializeField]
    private UIController uiController;

    private int score;

    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        AudioManager.Instance.PlayStartGameSFX();

        board.Clean();
        hand.Clean();

        board.Initialize(this);
        hand.Initialize(this);

        score = 0;
        int highScore = PlayerPrefs.GetInt("High Score", 0);
        uiController.OnScoreUpdated(score);
        uiController.OnHighScoreUpdated(highScore);
    }

    private void FinishGame()
    {
        AudioManager.Instance.PlayGameOverSFX();

        CheckUpdateHighScore();
        uiController.OnGameFinished();
    }

    public void OnStartGameIntent()
    {
        StartGame();
    }

    public void OnPiecePutOnBoard(Piece piece)
    {
        AudioManager.Instance.PlayPiecePutOnBoardSFX();

        board.CheckHandleTilesToExplode();

        if (CheckGameOver(board, hand) == true)
        {
            FinishGame();
        }
    }

    public void OnTileBlockExploded()
    {
        AudioManager.Instance.PlayExplodeSFX();

        score += GameParameters.Instance.scorePerExplodedTileBlock;
        uiController.OnScoreUpdated(score);
    }

    private bool CheckGameOver(Board board, Hand hand)
    {
        bool isGameOver = false;

        foreach (Piece piece in hand.Pieces)
        {
            if (board.CheckSpaceForPieceType(piece.PieceType) == false)
            {
                isGameOver = true;
            }
        }

        return isGameOver;
    }

    private void CheckUpdateHighScore()
    {
        int highScore = PlayerPrefs.GetInt("High Score", 0);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("High Score", score);
            uiController.OnHighScoreUpdated(highScore);
        }
    }
}