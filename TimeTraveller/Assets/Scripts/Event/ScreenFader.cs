using System;
using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader instance = null;
    [SerializeField] private AnimationClip fadeAnimationClip;
    private Animator animator;

    private void Awake() {
        instance = this;
        animator = GetComponent<Animator>();
    }

    public void Fade(Action action)
    {
        StartCoroutine(FadeCoroutine(action));
    }

    private IEnumerator FadeCoroutine(Action action)
    {
        animator.SetTrigger("Fade");
        yield return new WaitForSeconds(fadeAnimationClip.length / 2.0f);
        action.Invoke();

    }
}
