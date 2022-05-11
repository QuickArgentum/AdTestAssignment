using UnityEngine;

public class ItemAdInfo
{
    public string title;
    public string subtitle;
    public Texture texture;
    public bool useDefaultTexture = false;
    public string priceText;

    public void SetPriceText(string currencySign, float price)
    {
        this.priceText = string.Format("{0} {1}", currencySign, price.ToString("0.00"));
    }
}