using System;

public class LoadingOverlay : Singleton<LoadingOverlay>
{
    public Fader overlay;

    private void Start()
    {
        overlay.FadeOutCompleted += (object sender, EventArgs e) =>
        {
            overlay.gameObject.SetActive(false);
        };
        overlay.gameObject.SetActive(false);
    }

    public static void Show()
    {
        Instance.overlay.gameObject.SetActive(true);
        Instance.overlay.FadeIn();
    }

    public static void Remove()
    {
        Instance.overlay.FadeOut();
    }
}