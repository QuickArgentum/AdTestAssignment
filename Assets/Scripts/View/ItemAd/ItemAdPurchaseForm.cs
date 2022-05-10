using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemAdPurchaseForm : MonoBehaviour
{
    public ItemAdInputField emailInput;
    public ItemAdInputField cardInput;
    public ItemAdInputField expirationMonthInput;
    public ItemAdInputField expirationYearInput;
    public Button purchaseButton;
    public Button cancelButton;

    public event EventHandler ConfirmClicked;
    public event EventHandler CancelClicked;

    private void Start()
    {
        purchaseButton.onClick.AddListener(OnConfirmClicked);
        cancelButton.onClick.AddListener(() =>
        {
            CancelClicked.Invoke(this, null);
        });
    }

    private void OnConfirmClicked()
    {
        if 
        (
            emailInput.Verify() &&
            cardInput.Verify() &&
            expirationMonthInput.Verify() &&
            expirationYearInput.Verify()
        )
        {
            ConfirmClicked.Invoke(this, new ItemAdPanel.PurchaseEventArgs { PurchaseInfo = CreateData() });
        }
    }

    private ItemAdPurchaseInfo CreateData()
    {
        ItemAdPurchaseInfo result = new ItemAdPurchaseInfo();
        result.email = emailInput.GetText();
        result.creditCard = cardInput.GetText();
        result.expirationDate = string.Format("{0}/{1}", expirationMonthInput.GetText(), expirationYearInput.GetText());

        return result;
    }
}