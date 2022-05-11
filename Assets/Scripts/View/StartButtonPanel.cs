using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonPanel : Singleton<StartButtonPanel>
{
    public Button videoAdButton;
    public Button itemAdButton;
    public Fader fader;

    public event EventHandler VideoAdClicked;
    public event EventHandler ItemAdClicked;

    private void Start()
    {
        videoAdButton.onClick.AddListener(() =>
        {
            VideoAdClicked?.Invoke(this, null);
        });
        itemAdButton.onClick.AddListener(() =>
        {
            ItemAdClicked?.Invoke(this, null);
        });

        fader.FadeIn();
    }

    public void Lock()
    {
        SetLocked(true);
    }

    public void Unlock()
    {
        SetLocked(false);
    }

    private void SetLocked(bool value)
    {
        videoAdButton.interactable = !value;
        itemAdButton.interactable = !value;
    }
}