using System;
using UnityEngine;

public class ItemAdPanel : Singleton<ItemAdPanel>
{
    public event EventHandler PlaceOrderClicked;
    public event EventHandler Closed;

    public ItemAdItemDisplay itemDisplay;
    public ItemAdPurchaseForm purchaseForm;
    public Fader fader;
    public Animation flipAnimation;

    void Start()
    {
        fader.FadeOutCompleted += (object sender, EventArgs e) =>
        {
            fader.gameObject.SetActive(false);
            Closed?.Invoke(this, null);
        };
        fader.gameObject.SetActive(false);

        itemDisplay.CancelClicked += (object sender, EventArgs e) =>
        {
            Close();
        };
        itemDisplay.PurchaseClicked += (object sender, EventArgs e) =>
        {
            flipAnimation.Play(Animations.ITEM_AD_TO_PURCHASE);
        };

        purchaseForm.CancelClicked += (object sender, EventArgs e) =>
        {
            flipAnimation.Play(Animations.ITEM_AD_TO_DISPLAY);
        };
        purchaseForm.ConfirmClicked += OnPurchaseClicked;
    }

    private void OnPurchaseClicked(object sender, EventArgs e)
    {
        PlaceOrderClicked.Invoke(this, e);
    }

    public void Show(ItemAdInfo data)
    {
        fader.gameObject.SetActive(true);
        fader.FadeIn();

        SwitchToDisplayPage(true);
        itemDisplay.SetData(data);
    }

    public void Close()
    {
        fader.FadeOut();
    }

    private void SwitchToDisplayPage(bool value)
    {
        itemDisplay.gameObject.SetActive(value);
        purchaseForm.gameObject.SetActive(!value);
    }

    public class PurchaseEventArgs : EventArgs
    {
        public ItemAdPurchaseInfo PurchaseInfo { get; set; }
    }
}