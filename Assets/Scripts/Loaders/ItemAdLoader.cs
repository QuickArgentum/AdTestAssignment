using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ItemAdLoader : Singleton<ItemAdLoader>
{
    [TextArea]
    public string url;

    public void LoadAd(Action<ItemAdInfo> success, Action<string> error)
    {
        StartCoroutine(RunRequest(success, error));
    }

    private IEnumerator RunRequest(Action<ItemAdInfo> success, Action<string> error)
    {
        UnityWebRequest uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
        uwr.SetRequestHeader("Content-Type", "application/json");
        uwr.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes("{}"));
        uwr.downloadHandler = new DownloadHandlerBuffer();
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
            error(uwr.error);
        else
        {
            // For whatever reason the API return JSON with single quotes which violates the JSON format and
            // makes Unity's JsonUtility throw en error. So we replace the single quotes whith double ones
            string text = uwr.downloadHandler.text.Replace("\'", "\"");

            ItemAdRawResponseInfo response = JsonUtility.FromJson<ItemAdRawResponseInfo>(text);

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
                result.priceText = string.Format("{0} {1}", response.currency_sign, response.price);

                success(result);
            }
        }
    }
}