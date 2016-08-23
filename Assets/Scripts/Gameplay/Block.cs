using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField]
    private Sprite blueSprite;
    [SerializeField]
    private Sprite greenSprite;
    [SerializeField]
    private Sprite greySprite;
    [SerializeField]
    private Sprite purpleSprite;
    [SerializeField]
    private Sprite redSprite;
    [SerializeField]
    private Sprite yellowSprite;

    [Header("References")]
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Image image;

    private ColorType colorType;

    public void Initialize(ColorType colorType)
    {
        this.colorType = colorType;

        PopulateImage(colorType);
    }

    public void DestroyMe()
    {
        LeanTween.scale(rectTransform, Vector3.zero, 0.5f).setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void PopulateImage(ColorType colorType)
    {
        Sprite sprite = null;

        switch (colorType)
        {
            case ColorType.Blue:
                sprite = blueSprite;
                break;

            case ColorType.Green:
                sprite = greenSprite;
                break;

            case ColorType.Grey:
                sprite = greySprite;
                break;

            case ColorType.Purple:
                sprite = purpleSprite;
                break;

            case ColorType.Red:
                sprite = redSprite;
                break;

            case ColorType.Yellow:
                sprite = yellowSprite;
                break;
        }

        image.sprite = sprite;
    }

    public ColorType ColorType
    {
        get
        {
            return colorType;
        }
    }
}