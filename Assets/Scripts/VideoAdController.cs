using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class VideoAdController : Singleton<VideoAdController>
{
    public string url;

    public void LoadData(Action<VideoAd> success, Action error) {
        StartCoroutine(RunRequest(success, error));
    }

    private IEnumerator RunRequest(Action<VideoAd> success, Action error)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.Success)
        {
            success(VideoAd.FromXML(uwr.downloadHandler.text));
        }
        else
        {
            error();
        }
    }
}
