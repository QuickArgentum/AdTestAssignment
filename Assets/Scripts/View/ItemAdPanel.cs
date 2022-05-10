using System;

public class ItemAdPanel : Singleton<ItemAdPanel>
{
    public event EventHandler PlaceOrderClicked;

    private ItemAdItemDisplay itemDisplay;
    private ItemAdPurchaseForm purchaseForm;

    void Start()
    {
        itemDisplay = GetComponentInChildren<ItemAdItemDisplay>(true);
        purchaseForm = GetComponentInChildren<ItemAdPurchaseForm>(true);

        Close();

        itemDisplay.CancelClicked += (object sender, EventArgs e) =>
        {
            itemDisplay.gameObject.SetActive(false);
        };
        itemDisplay.PurchaseClicked += (object sender, EventArgs e) =>
        {
            itemDisplay.gameObject.SetActive(false);
            purchaseForm.gameObject.SetActive(true);
        };

        purchaseForm.CancelClicked += (object sender, EventArgs e) =>
        {
            itemDisplay.gameObject.SetActive(true);
            purchaseForm.gameObject.SetActive(false);
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
        itemDisplay.gameObject.SetActive(true);
        itemDisplay.SetData(data);
    }

    public void Close()
    {
        itemDisplay.gameObject.SetActive(false);
        purchaseForm.gameObject.SetActive(false);
    }

    public class PurchaseEventArgs : EventArgs
    {
        public ItemAdPurchaseInfo PurchaseInfo { get; set; }
    }
}