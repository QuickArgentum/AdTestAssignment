using System;
using System.Collections;
using UnityEngine;


public class AppController : Singleton<AppController>
{
    private void Start()
    {
        ItemAdPanel.Instance.PlaceOrderClicked += (object sender, EventArgs e) =>
        {
            PurchaseItem((e as ItemAdPanel.PurchaseEventArgs).PurchaseInfo);
        };
    }

    public void PurchaseItem(ItemAdPurchaseInfo data)
    {
        LoadingOverlay.Show();
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
                MessageBox.ShowError(err);
                LoadingOverlay.Remove();
                ItemAdPanel.Instance.Close();
            }
        );
    }

    public void PlayVideoAd()
    {
        LoadingOverlay.Show();
        VideoAdLoader.Instance.LoadAd
        (
            (VideoAdInfo ad) =>
                {
                    VideoAdPlayer.Instance.PlayAd(ad.url);
                    LoadingOverlay.Remove();
                },
            (string err) =>
                {
                    MessageBox.ShowError(err);
                    LoadingOverlay.Remove();
                }
        );
    }

    public void PlayItemAd()
    {
        LoadingOverlay.Show();
        ItemAdLoader.Instance.LoadAd
        (
            (ItemAdInfo ad) =>
            {
                ItemAdPanel.Instance.Show(ad);
                LoadingOverlay.Remove();
            },
            (string err) =>
            {
                MessageBox.ShowError(err);
                LoadingOverlay.Remove();
            }
        );
    }
}