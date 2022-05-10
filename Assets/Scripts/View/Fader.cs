using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class Fader : MonoBehaviour
{
    public event EventHandler FadeOutCompleted;

    public Animation anim;

    private float fadeOutTime;
 
    void Start()
    {
        AnimationClip clip = anim.GetClip(Animations.FADE_OUT);
        fadeOutTime = clip.length;
    }

    public void FadeIn()
    {
        anim.Play(Animations.FADE_IN);
    }

    public void FadeOut()
    {
        anim.Play(Animations.FADE_OUT);
        StartCoroutine(FadeOutTimer());
    }

    private IEnumerator FadeOutTimer()
    {
        yield return new WaitForSeconds(fadeOutTime);

        OnFadeOutCompleted();
    }

    public void OnFadeOutCompleted()
    {
        FadeOutCompleted?.Invoke(this, null);
    }

    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }
}