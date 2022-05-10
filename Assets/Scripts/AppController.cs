using System;
using System.Collections;
using UnityEngine;


public class AppController : Singleton<AppController>
{
    private void Start()
    {
        StartButtonPanel.Instance.VideoAdClicked += (object sender, EventArgs e) =>
        {
            PlayVideoAd();
        };
        StartButtonPanel.Instance.ItemAdClicked += (object sender, EventArgs e) =>
        {
            PlayItemAd();
        };

        ItemAdPanel.Instance.PlaceOrderClicked += (object sender, EventArgs e) =>
        {
            PurchaseItem((e as ItemAdPanel.PurchaseEventArgs).PurchaseInfo);
        };
        ItemAdPanel.Instance.Closed += (object sender, EventArgs e) =>
        {
            StartButtonPanel.Instance.Unlock();
        };

        VideoAdPlayer.Instance.PlaybackCompleted += (object sender, EventArgs e) =>
        {
            StartButtonPanel.Instance.Unlock();
        };
    }

    public void PurchaseItem(ItemAdPurchaseInfo data)
    {
        Lock();
        ItemAdLoader.Instance.PurchaseItem
        (
            data,
            (string message) =>
            {
                MessageBox.ShowSuccess(message);
                LoadingOverlay.Remove();
                ItemAdPanel.Instance.Close();
            },
            (string err) =>
            {
                ShowError(err);
                ItemAdPanel.Instance.Close();
            }
        );
    }

    public void PlayVideoAd()
    {
        Lock();
        VideoAdLoader.Instance.LoadAd
        (
            (VideoAdInfo ad) =>
            {
                VideoAdPlayer.Instance.PlayAd(ad.url);
                LoadingOverlay.Remove();
            },
            (string err) =>
            {
                ShowError(err);
            }
        );
    }

    public void PlayItemAd()
    {
        Lock();
        ItemAdLoader.Instance.LoadAd
        (
            (ItemAdInfo ad) =>
            {
                ItemAdPanel.Instance.Show(ad);
                LoadingOverlay.Remove();
            },
            (string err) =>
            {
                ShowError(err);
            }
        );
    }

    private void ShowError(string message)
    {
        MessageBox.ShowError(message);
        LoadingOverlay.Remove();
        StartButtonPanel.Instance.Unlock();
    }

    private void Lock()
    {
        LoadingOverlay.Show();
        StartButtonPanel.Instance.Lock();
    }
}