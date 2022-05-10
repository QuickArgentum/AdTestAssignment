using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ItemAdLoader : Singleton<ItemAdLoader>
{
    [TextArea]
    public string adUrl;
    [TextArea]
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
        UnityWebRequest uwr = new UnityWebRequest(adUrl, UnityWebRequest.kHttpVerbPOST);
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes("{}"));
        uwr.downloadHandler = new DownloadHandlerBuffer();
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
            error(uwr.error);
        else
        {
            ItemAdRawResponseInfo response = LoaderUtil.ResponseFromJSON<ItemAdRawResponseInfo>(uwr);

            if (response.status != "Success")
                error(response.error_code.ToString());
            else
            {
                ItemAdInfo result = new ItemAdInfo();

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
                result.priceText = string.Format("{0} {1}", response.currency_sign, response.price.ToString("0.00"));

                success(result);
            }
        }
    }

    private IEnumerator RunPurchaseRequest(ItemAdPurchaseInfo data, Action<string> success, Action<string> error)
    {
        UnityWebRequest uwr = new UnityWebRequest(purchaseUrl, UnityWebRequest.kHttpVerbPOST);
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonUtility.ToJson(data)));
        uwr.downloadHandler = new DownloadHandlerBuffer();
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
            error(uwr.error);
        else
        {
            ItemPurchaseRawResponseInfo response = LoaderUtil.ResponseFromJSON<ItemPurchaseRawResponseInfo>(uwr);

            if (response.status != "Success")
                error(response.error_code.ToString());
            else
            {
                success(response.user_message);
            }
        }
    }
}