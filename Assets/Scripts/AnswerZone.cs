using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AnswerZone : MonoBehaviour
{
    // //public bool isCorrectWord = false;
    // public string displayText;
    public TextMeshProUGUI answerText;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpellingController.OnPlayerEnterZone(answerText.text);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpellingController.OnPlayerExitZone();
        }
    }
    
}
