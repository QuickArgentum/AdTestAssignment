using System;

/// <summary>
/// Stores JSON response from purchase item request
/// </summary>
[Serializable]
public class ItemPurchaseRawResponseInfo
{
    public string user_message;
    public string status;
    public string error_code;
}