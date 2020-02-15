using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public string sceneToLoad;

    [SerializeField] private Image fadeImage;
    private Transform imageTransfrom;
    private readonly int midScreen = Screen.width / 2;
    private AsyncOperation async;

    private void Awake()
    {
        imageTransfrom = fadeImage.transform;
    }

    private void Start()
    {
        UnfadeScene();
    }

    private void UnfadeScene()
    {
        imageTransfrom.DOMoveX(midScreen * 3.5f, 1f).SetEase(Ease.OutQuint).OnComplete(ResetFadeImage);
    }

    private void ResetFadeImage()
    {
        imageTransfrom.position = new Vector2(midScreen * -3.5f,imageTransfrom.position.y);
    }

    public void StartLoadScene(string scene)
    {
        async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;
        imageTransfrom.DOMoveX(midScreen, 1f).SetEase(Ease.OutQuint).OnComplete(AllowLoadScene);
    }

    public void AllowLoadScene()
    {
        async.allowSceneActivation = true;
    }
}
