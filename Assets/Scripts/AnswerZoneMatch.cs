using TMPro;
using UnityEngine;

public class AnswerZoneMatch : MonoBehaviour
{
    // //public bool isCorrectWord = false;
    // public string displayText;
    public TextMeshProUGUI answerText;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MatchingController.OnPlayerEnterZone(answerText.text);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MatchingController.OnPlayerExitZone();
        }
    }
    
}