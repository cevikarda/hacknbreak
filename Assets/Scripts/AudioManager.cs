using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Assets")]
    [SerializeField]
    private AudioClip buttonClickAudioClip;
    [SerializeField]
    private AudioClip explodeAudioClip;
    [SerializeField]
    private AudioClip gameOverAudioClip;
    [SerializeField]
    private AudioClip startGameAudioClip;
    [SerializeField]
    private AudioClip wrongMoveAudioClip;
    [SerializeField]
    private AudioClip piecePutOnBoardAudioClip;

    [Header("References")]
    [SerializeField]
    private AudioSource audioSource;

    public void PlayButtonClickSFX()
    {
        audioSource.PlayOneShot(buttonClickAudioClip);
    }

    public void PlayExplodeSFX()
    {
        audioSource.PlayOneShot(explodeAudioClip);
    }

    public void PlayGameOverSFX()
    {
        audioSource.PlayOneShot(gameOverAudioClip);
    }

    public void PlayStartGameSFX()
    {
        audioSource.PlayOneShot(startGameAudioClip);
    }

    public void PlayWrongMoveSFX()
    {
        audioSource.PlayOneShot(wrongMoveAudioClip);
    }

    public void PlayPiecePutOnBoardSFX()
    {
        audioSource.PlayOneShot(piecePutOnBoardAudioClip);
    }
}