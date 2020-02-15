using System;
using TMPro;
using UnityEngine;

public class AnswerZone : MonoBehaviour
{
    // //public bool isCorrectWord = false;
    // public string displayText;
    public TextMeshProUGUI answerText;
    public GameController gameController;

    private void Awake()
    {
        if (gameController == null)
        {
            throw new Exception("GAMECONTROLLER NOT FOUND ON " + this.name);
        }
    }

    protected void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameController.OnPlayerEnterZone(answerText.text);
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameController.OnPlayerExitZone();
        }
    }
    
}
