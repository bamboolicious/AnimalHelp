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
    public static TransitionManager Instance;
    [SerializeField] private Image fadeImage;
    private Image imageTransfrom;
    private readonly int midScreen = Screen.width / 2;
    private AsyncOperation async;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UnfadeScene();
    }

    private void UnfadeScene()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(0,1f);
    }


    public void StartLoadScene(string scene)
    {
        async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;
        fadeImage.DOFade(1, 0.25f).OnComplete(AllowLoadScene).SetUpdate(true);
        fadeImage.gameObject.SetActive(false);
    }

    public void AllowLoadScene()
    {
        async.allowSceneActivation = true;
    }
}
