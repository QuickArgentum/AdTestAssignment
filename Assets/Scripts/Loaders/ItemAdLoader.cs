using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ItemAdLoader : Singleton<ItemAdLoader>
{
    [TextArea]
    [Tooltip("The URL to load item ad data from")]
    public string adUrl;
    [TextArea]
    [Tooltip("The URL to send the user data from purchase form to purchase an item")]
    public string purchaseUrl;

    public void LoadAd(Action<ItemAdInfo> success, Action<string> error)
    {
        StartCoroutine(RunAdRequest(success, error));
    }

    public void PurchaseItem(ItemAdPurchaseInfo data, Action<string> success, Action<string> error)
    {
        StartCoroutine(RunPurchaseRequest(data, success, error));
    }

    private IEnumerator RunAdRequest(Action<ItemAdInfo> success, Action<string> error)
    {
        // Get item info for the item ad
        UnityWebRequest uwr = LoaderUtil.CreateRequest(adUrl, UnityWebRequest.kHttpVerbPOST, "{}");
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            error(uwr.error);
            yield break;
        }

        ItemAdRawResponseInfo response;
        try
        {
            response = LoaderUtil.ResponseFromJSON<ItemAdRawResponseInfo>(uwr);
        }
        catch (Exception)
        {
            error(Strings.ERROR_RESPONSE);
            yield break;
        }

        if (response.status != "Success")
        {
            error(response.error_code.ToString());
            yield break;
        }

        ItemAdInfo result = new ItemAdInfo();

        // Load the item texture. Use the default texture if the loading fails
        UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(response.item_image);
        yield return textureRequest.SendWebRequest();

        if (textureRequest.result != UnityWebRequest.Result.Success)
            result.useDefaultTexture = true;
        else
        {
            result.texture = ((DownloadHandlerTexture)textureRequest.downloadHandler).texture;
        }

        result.title = response.title;
        result.subtitle = response.item_name;
        result.SetPriceText(response.currency_sign, response.price);

        success(result);
    }

    private IEnumerator RunPurchaseRequest(ItemAdPurchaseInfo data, Action<string> success, Action<string> error)
    {
        // Send the item purchase request with user data from purchase form
        UnityWebRequest uwr = LoaderUtil.CreateRequest(purchaseUrl, UnityWebRequest.kHttpVerbPOST, JsonUtility.ToJson(data));
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            error(uwr.error);
            yield break;
        }

        ItemPurchaseRawResponseInfo response;
        try
        {
            response = LoaderUtil.ResponseFromJSON<ItemPurchaseRawResponseInfo>(uwr);
        }
        catch (Exception)
        {
            error(Strings.ERROR_RESPONSE);
            yield break;
        }

        if (response.status != "Success")
            error(response.error_code.ToString());
        else
            success(response.user_message);
    }
}