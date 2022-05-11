using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class LoaderUtil
{
    /// <summary>
    /// Parse UnityWebRequest's response as JSON. Automatically replaces single quotes.
    /// </summary>
    public static T ResponseFromJSON<T>(UnityWebRequest uwr)
    {
        // For whatever reason the API returns JSON with single quotes which violates the JSON format and
        // makes Unity's JsonUtility throw en error. So we replace the single quotes with double ones
        string text = uwr.downloadHandler.text.Replace("\'", "\"");

        return JsonUtility.FromJson<T>(text);
    }

    /// <summary>
    /// Create a UnityWebRequest and initialize the necessary fields for it
    /// </summary>
    public static UnityWebRequest CreateRequest(string url, string type, string body = null)
    {
        UnityWebRequest uwr = new UnityWebRequest(url, type);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        if (type == UnityWebRequest.kHttpVerbPOST)
        {
            uwr.SetRequestHeader("Content-Type", "application/json");
            uwr.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
        }

        return uwr;
    }
}