using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class VideoAdLoader : Singleton<VideoAdLoader>
{
    [TextArea]
    [Tooltip("The URL to get the video ad data from")]
    public string url;

    public void LoadAd(Action<VideoAdInfo> success, Action<string> error)
    {
        StartCoroutine(RunRequest(success, error));
    }

    private IEnumerator RunRequest(Action<VideoAdInfo> success, Action<string> error)
    {
        // Get the video ad info
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.Success)
        {
            VideoAdInfo result;
            try
            {
                result = VideoAdInfo.FromXML(uwr.downloadHandler.text);
            }
            catch (Exception)
            {
                error(Strings.ERROR_RESPONSE);
                yield break;
            }
            success(VideoAdInfo.FromXML(uwr.downloadHandler.text));
        }
        else
        {
            error(uwr.error);
        }
    }
}
