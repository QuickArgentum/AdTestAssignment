using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class Fader : MonoBehaviour
{
    public event EventHandler FadeOutCompleted;

    public Animation anim;
 
    void Start()
    {
        AnimationClip clip = anim.GetClip(Animations.FADE_OUT);

        AnimationEvent evt = new AnimationEvent();
        evt.time = clip.averageDuration;
        evt.functionName = "OnFadeOutCompleted";

        clip.AddEvent(evt);
    }

    public void FadeIn()
    {
        anim.Play(Animations.FADE_IN);
    }

    public void FadeOut()
    {
        anim.Play(Animations.FADE_OUT);
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