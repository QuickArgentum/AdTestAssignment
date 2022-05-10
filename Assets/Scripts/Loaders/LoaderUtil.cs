using System.Collections;
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
}