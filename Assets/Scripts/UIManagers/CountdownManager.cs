using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CountdownManager : MonoBehaviour
{
    public GameObject[] countdownNumbers;
    public GameObject countdownBackgroundTransform;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        StartCoroutine(ShowTheCountdownRoutine());
    }

    IEnumerator ShowTheCountdownRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        countdownBackgroundTransform.GetComponent<RectTransform>().DORotate(Vector3.zero, .3f).SetEase(Ease.OutBack);
        countdownBackgroundTransform.GetComponent<CanvasGroup>().DOFade(1, .3f);

        yield return new WaitForSeconds(0.2f);
        int counter = 0;
        while (counter < countdownNumbers.Length)
        {
            countdownNumbers[counter].GetComponent<RectTransform>().DOLocalMoveY(0, .3f).SetEase(Ease.OutBack);
            countdownNumbers[counter].GetComponent<CanvasGroup>().DOFade(1, .3f);
            countdownNumbers[counter].GetComponent<RectTransform>().DOScale(0.3f, .3f).SetEase(Ease.OutBounce)
                .OnComplete(() =>
                    countdownNumbers[counter].GetComponent<RectTransform>().DOScale(0.15f, .3f)
                        .SetEase(Ease.OutBounce));
            yield return new WaitForSeconds(0.5f);

            countdownNumbers[counter].GetComponent<RectTransform>().DOLocalMoveY(100, .3f).SetEase(Ease.OutBounce);
            countdownNumbers[counter].GetComponent<CanvasGroup>().DOFade(0, .3f);
            yield return new WaitForSeconds(0.5f);

            counter++;
        }

        countdownBackgroundTransform.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 180), .3f)
            .SetEase(Ease.InBack).OnComplete(
                () =>
                {
                    countdownBackgroundTransform.GetComponent<CanvasGroup>().DOFade(0, .3f);
                    countdownBackgroundTransform.transform.parent.gameObject.SetActive(false);
                    _gameManager.StartTheGame();
                }
            );
    }
}