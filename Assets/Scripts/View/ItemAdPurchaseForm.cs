using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemAdPurchaseForm : MonoBehaviour
{
    public InputField emailInput;
    public InputField cardInput;
    public InputField expirationMonthInput;
    public InputField expirationYearInput;
    public Button purchaseButton;
    public Button cancelButton;

    public event EventHandler ConfirmClicked;
    public event EventHandler CancelClicked;

    private void Start()
    {
        purchaseButton.onClick.AddListener(() =>
        {
            ConfirmClicked.Invoke(this, null);
        });
        cancelButton.onClick.AddListener(() =>
        {
            CancelClicked.Invoke(this, null);
        });
    }

    public ItemAdPurchaseInfo CreateData()
    {
        ItemAdPurchaseInfo result = new ItemAdPurchaseInfo();
        result.email = emailInput.text;
        result.creditCard = cardInput.text;
        result.expirationDate = string.Format("{0}/{1}", expirationMonthInput.text, expirationYearInput.text);

        return result;
    }
}