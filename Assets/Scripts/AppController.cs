using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class AppController : Singleton<AppController>
    {
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
                    Debug.Log(ad.title);
                },
                (string err) =>
                {
                    Debug.LogWarning("Item ad request failed: " + err);
                }
            );
        }
    }
}