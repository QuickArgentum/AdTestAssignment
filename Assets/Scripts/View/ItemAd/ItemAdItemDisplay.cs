using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemAdItemDisplay : MonoBehaviour
{
    public event EventHandler PurchaseClicked;
    public event EventHandler CancelClicked;

    public Text title;
    public Text subtitle;
    public RawImage image;
    public AspectRatioFitter imageFitter;
    public Text priceLabel;
    public Button purchaseButton;
    public Button cancelButton;
    public Texture defaultTexture;

    void Start()
    {
        purchaseButton.onClick.AddListener(() =>
        {
            PurchaseClicked.Invoke(this, null);
        });
        cancelButton.onClick.AddListener(() =>
        {
            CancelClicked.Invoke(this, null);
        });
    }

    public void SetData(ItemAdInfo data)
    {
        title.text = data.title;
        subtitle.text = data.subtitle;
        priceLabel.text = data.priceText;

        if (data.useDefaultTexture)
            image.texture = defaultTexture;
        else
            image.texture = data.texture;

        imageFitter.aspectRatio = (float)data.texture.width / (float)data.texture.height;
    }
}