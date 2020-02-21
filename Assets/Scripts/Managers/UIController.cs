using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Image pausePanel;
    [SerializeField] private Image deadImage;
    [SerializeField] private TextMeshProUGUI deadText;
    [SerializeField] private string deadString;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GameController.IsDead = false;
        deadText.text = "";
        deadImage.color = Color.clear;
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
        pausePanel.rectTransform.DOShakeScale(0.25f,0.25f).SetUpdate(true);
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
        deadImage.color = Color.white;
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
