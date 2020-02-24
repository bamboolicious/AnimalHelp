using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Image pausePanel;
    [SerializeField] private Image deadImage;
    [SerializeField] private TextMeshProUGUI deadText;
    [SerializeField] private string deadString;
    [SerializeField] private Sprite loseSprite, pauseSprite;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GameController.IsDead = false;
        deadText.text = "";
        deadImage.color = Color.white;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameController.IsDead)
        {
            if (Time.timeScale == 1)
            {
                Pause();
            }
            else if (Time.timeScale == 0)
            {
                UnPause();
            }
        }
    }
    private void Pause()
    {
        pausePanel.gameObject.SetActive(true);
        deadImage.sprite = pauseSprite;
        pausePanel.rectTransform.DOShakeScale(0.25f, 0.25f).SetUpdate(true);
        Time.timeScale = 0;
    }
    private void UnPause()
    {
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        deadText.text = deadString;
        deadImage.sprite = loseSprite;
        Pause();
    }

    public void Restart()
    {
        UnPause();
        GameController.IsDead = true;
        TransitionManager.Instance.StartLoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        UnPause();
        GameController.IsDead = true;
        TransitionManager.Instance.StartLoadScene("MainMenu");
    }
}