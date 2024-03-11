using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ScreenFadeManager : MonoBehaviour
{
    public float initiationAlpha = 1f,endAlpha = 0f, waitingTime = 0f, fadeTime = 1f;


    private void Start()
    {
        GetComponent<CanvasGroup>().alpha = initiationAlpha;
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        yield return new WaitForSeconds(waitingTime);
        GetComponent<CanvasGroup>().DOFade(endAlpha, fadeTime);
    }
}
