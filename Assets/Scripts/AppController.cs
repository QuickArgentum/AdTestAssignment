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
        ItemAdLoader.Instance.PurchaseItem
        (
            data,
            (string message) =>
            {
                MessageBox.ShowSuccess(message);
                ItemAdPanel.Instance.Close();
            },
            (string err) =>
            {
                MessageBox.ShowError(err);
                ItemAdPanel.Instance.Close();
            }
        );
    }

    public void PlayVideoAd()
    {
        VideoAdLoader.Instance.LoadAd
        (
            (VideoAdInfo ad) =>
                {
                    VideoAdPlayer.Instance.PlayAd(ad.url);
                },
            (string err) =>
                {
                    MessageBox.ShowError(err);
                }
        );
    }

    public void PlayItemAd()
    {
        ItemAdLoader.Instance.LoadAd
        (
            (ItemAdInfo ad) =>
            {
                ItemAdPanel.Instance.Show(ad);
            },
            (string err) =>
            {
                MessageBox.ShowError(err);
            }
        );
    }
}