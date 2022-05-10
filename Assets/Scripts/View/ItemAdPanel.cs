using System;

public class ItemAdPanel : Singleton<ItemAdPanel>
{
    public event EventHandler PlaceOrderClicked;
    public event EventHandler Closed;

    public ItemAdItemDisplay itemDisplay;
    public ItemAdPurchaseForm purchaseForm;
    public Fader fader;

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
            SwitchToDisplayPage(false);
        };

        purchaseForm.CancelClicked += (object sender, EventArgs e) =>
        {
            SwitchToDisplayPage(true);
        };
        purchaseForm.ConfirmClicked += OnPurchaseClicked;
    }

    private void OnPurchaseClicked(object sender, EventArgs e)
    {
        PurchaseEventArgs data = new PurchaseEventArgs { PurchaseInfo = purchaseForm.CreateData() };
        PlaceOrderClicked.Invoke(this, data);
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