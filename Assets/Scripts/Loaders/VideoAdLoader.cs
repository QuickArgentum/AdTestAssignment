using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class VideoAdLoader : Singleton<VideoAdLoader>
{
    [TextArea]
    public string url;

    public void LoadAd(Action<VideoAdInfo> success, Action<string> error) {
        StartCoroutine(RunRequest(success, error));
    }

    private IEnumerator RunRequest(Action<VideoAdInfo> success, Action<string> error)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.Success)
            success(VideoAdInfo.FromXML(uwr.downloadHandler.text));
        else
            error(uwr.error);
    }
}