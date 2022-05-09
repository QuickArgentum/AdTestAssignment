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
                        Debug.Log(ad.url);
                    },
                () =>
                    {
                        Debug.LogWarning("Ad request failed");
                    }
            );
        }
    }
}