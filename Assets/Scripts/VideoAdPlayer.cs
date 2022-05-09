using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoAdPlayer : Singleton<VideoAdPlayer>
{
    private VideoPlayer player;
    private GameObject panel;
    private RawImage image;

    private void Start()
    {
        Transform content = transform.Find("Panel/VideoContent");
        player = content.GetComponent<VideoPlayer>();
        player.loopPointReached += OnPlaybackCompleted;
        player.prepareCompleted += OnPrepareCompleted;

        image = content.GetComponent<RawImage>();
        panel = transform.Find("Panel").gameObject;
    }

    public void PlayAd(string url)
    {
        panel.SetActive(true);
        player.url = url;
        player.Prepare();
    }

    private void OnPrepareCompleted(VideoPlayer source)
    {
        image.texture = player.texture;
        player.Play();
    }

    private void OnPlaybackCompleted(VideoPlayer player)
    {
        panel.SetActive(false);
    }
}