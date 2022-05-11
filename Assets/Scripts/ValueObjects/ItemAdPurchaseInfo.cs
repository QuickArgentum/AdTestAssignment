using System;

/// <summary>
/// Stores information entered by user into the purchase form to send to the server
/// </summary>
[Serializable]
public class ItemAdPurchaseInfo
{
    public string email;
    public string creditCard;
    public string expirationDate;
}