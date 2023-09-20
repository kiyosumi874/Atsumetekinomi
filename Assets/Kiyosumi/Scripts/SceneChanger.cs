using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneChanger : SingletonMonoBehaviour<SceneChanger>
{
    [SerializeField] Image fadePanel;
    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;

    bool isFade = false;

    public void LoadSceneFaded(string nextSceneName)
    {
        if (isFade) { return; }

        TweenCallback onFadeOut = () =>
        {
            SceneManager.LoadScene(nextSceneName);
            fadePanel.DOFade(0.0f, fadeInTime).OnComplete(() => { isFade = false; });
        };

        isFade = true;
        fadePanel.DOFade(1.0f, fadeOutTime).OnComplete(onFadeOut);
    }
}
