using System;
using System.Collections;
using UnityEngine;


public class AppController : Singleton<AppController>
{
    private void Start()
    {
        ItemAdPanel.Instance.PlaceOrderClicked += (object sender, EventArgs e) =>
        {
            Debug.Log("Purchasing an item!");
            ItemAdPanel.Instance.Close();
        };
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
                    Debug.LogWarning("Video ad request failed: " + err);
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
                Debug.LogWarning("Item ad request failed: " + err);
            }
        );
    }
}