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
        UnityWebRequest uwr = LoaderUtil.CreateRequest(adUrl, UnityWebRequest.kHttpVerbPOST, "{}");
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
            error(uwr.error);
        else
        {
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
        UnityWebRequest uwr = LoaderUtil.CreateRequest(purchaseUrl, UnityWebRequest.kHttpVerbPOST, JsonUtility.ToJson(data));
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
            error(uwr.error);
        else
        {
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
}