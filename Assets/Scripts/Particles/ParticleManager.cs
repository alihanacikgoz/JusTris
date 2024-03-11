using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem[] allPArticleEffects;

    private void Start()
    {
        allPArticleEffects = GetComponentsInChildren<ParticleSystem>();
    }

    public void PlayEffectFNC()
    {
        foreach (ParticleSystem particleEffect in allPArticleEffects)
        {
            particleEffect.Stop();
            particleEffect.Play();
        }
    }
}
