using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemAdItemDisplay : MonoBehaviour
{
    public Text title;
    public Text subtitle;
    public RawImage image;
    public Text priceLabel;
    public Button purchaseButton;
    public Button cancelButton;

    public event EventHandler PurchaseClicked;
    public event EventHandler CancelClicked;

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
        image.texture = data.texture;
        priceLabel.text = data.priceText;
    }
}