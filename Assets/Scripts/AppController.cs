using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class AppController : Singleton<AppController>
    {
        public void PlayVideoAd()
        {
            VideoAdController.Instance.LoadData
            (
                (VideoAd ad) =>
                    {
                        VideoAdPlayer.Instance.PlayAd(ad.url);
                    },
                () =>
                    {
                        Debug.LogWarning("Ad request failed");
                    }
            );
        }
    }
}