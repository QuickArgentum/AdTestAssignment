using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoAdPlayer : Singleton<VideoAdPlayer>
{
    public event EventHandler PlaybackCompleted;

    private VideoPlayer player;
    private GameObject panel;
    private RawImage image;
    private AspectRatioFitter ratioFitter;

    private void Start()
    {
        Transform content = transform.Find("Panel/VideoContainer/VideoContent");
        player = content.GetComponent<VideoPlayer>();
        player.loopPointReached += OnPlaybackCompleted;
        player.prepareCompleted += OnPrepareCompleted;

        image = content.GetComponent<RawImage>();
        ratioFitter = content.GetComponent<AspectRatioFitter>();
        panel = transform.Find("Panel").gameObject;

        panel.SetActive(false);
    }

    public void PlayAd(string url)
    {
        panel.SetActive(true);
        image.enabled = false;

        player.url = url;
        player.Prepare();
    }

    private void OnPrepareCompleted(VideoPlayer source)
    {
        image.enabled = true;
        Texture texture = player.texture;

        image.texture = texture;
        ratioFitter.aspectRatio = (float)texture.width / (float)texture.height;
        player.Play();
    }

    private void OnPlaybackCompleted(VideoPlayer player)
    {
        panel.SetActive(false);
        PlaybackCompleted?.Invoke(this, null);
    }
}