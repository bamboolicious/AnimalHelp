using System;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerZone : MonoBehaviour
{
    // //public bool isCorrectWord = false;
    // public string displayText;
    [Header("References")]
    public Image answerZone;
    public TextMeshProUGUI answerText;
    public GameController gameController;

    [Space] [Header("Animation Values")] 
    [SerializeField] private float punchMultiplier = 2f;

    [SerializeField] private float punchDuration = 0.5f;

    private Tweener punchTween;

    private void Awake()
    {
        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
        }
        if (answerText == null)
        {
            answerText = answerZone.GetComponentInChildren<TextMeshProUGUI>();
        }
        CreateTween();
    }

    void CreateTween()
    {
        punchTween = answerZone.transform.DOPunchPosition(Vector2.up * punchMultiplier, punchDuration)
            .SetAutoKill(false);
    }
    private void OnPlayerHit()
    {
        punchTween.Restart();
        gameController.OnPlayerEnterZone(answerText.text,this);
    }

    public void OnPlayerExit()
    {
        gameController.OnPlayerExitZone();
    }
    
    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerHit();
        }
    }
}