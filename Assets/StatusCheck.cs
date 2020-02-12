using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusCheck : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI answerText;
    
    private void Awake()
    {
        statusText.GetComponent<TextMeshProUGUI>();
        answerText.GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpellingController.OnPlayerAnswer += PlayerAnswer;
        SpellingController.OnPlayerUnAnswer += PlayerUnAnswer;
        SpellingController.OnPlayerCorrect += PlayerCorrect;
        SpellingController.OnPlayerWrong += PlayerWrong;
        SpellingController.OnGameEnd += OnGameEnd;
    }

    void PlayerAnswer()
    {
        if (SpellingController.AnsweredWord != null) ;
        answerText.text = "Answer : " + SpellingController.AnsweredWord;
    }

    void OnGameEnd()
    {
        statusText.text = "GAME FINISHED";
    }

    void PlayerUnAnswer()
    {
        answerText.text = "Unanswered";
    }
    void PlayerCorrect()
    {
        statusText.text = "CORRECT";
    }void PlayerWrong()
    {
        statusText.text = "INCORRECT";
    }
    
}
