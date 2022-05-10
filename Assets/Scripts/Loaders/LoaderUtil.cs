using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class LoaderUtil
{
    public static T ResponseFromJSON<T>(UnityWebRequest uwr)
    {
        // For whatever reason the API return JSON with single quotes which violates the JSON format and
        // makes Unity's JsonUtility throw en error. So we replace the single quotes whith double ones
        string text = uwr.downloadHandler.text.Replace("\'", "\"");

        return JsonUtility.FromJson<T>(text);
    }

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