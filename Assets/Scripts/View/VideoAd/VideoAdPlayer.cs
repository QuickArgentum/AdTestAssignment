using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoAdPlayer : Singleton<VideoAdPlayer>
{
    public event EventHandler PlaybackCompleted;
    public event EventHandler PrepareCompleted;
    public event EventHandler PlaybackError;

    public VideoPlayer player;
    public GameObject panel;
    public RawImage image;
    public AspectRatioFitter ratioFitter;
    public Fader fader;

    private void Start()
    {
        player.loopPointReached += OnPlaybackCompleted;
        player.prepareCompleted += OnPrepareCompleted;
        player.errorReceived += OnError;

        fader.FadeOutCompleted += (object sender, EventArgs e) =>
        {
            fader.gameObject.SetActive(false);
        };
        fader.gameObject.SetActive(false);
    }

    public void PlayAd(string url)
    {
        fader.gameObject.SetActive(true);
        fader.Hide();
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

        PrepareCompleted?.Invoke(this, null);
        
        fader.FadeIn();
        player.Play();
    }

    private void OnPlaybackCompleted(VideoPlayer player)
    {
        fader.FadeOut();
        PlaybackCompleted?.Invoke(this, null);
    }

    private void OnError(VideoPlayer source, string message)
    {
        fader.gameObject.SetActive(false);
        PlaybackError?.Invoke(this, new VideoAdErrorEventArgs { Message = message });
    }

    public class VideoAdErrorEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}