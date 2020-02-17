using TMPro;
using UnityEngine;
using DG.Tweening;
using Managers;
using UnityEngine.UI;


public class VisualController : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private TextMeshProUGUI statusText;

    [SerializeField] private GameController gameController;
    [SerializeField] private TextMeshProUGUI answerText;
    [SerializeField] private Image animalImage;
    [SerializeField] private Sprite correctImage, incorrectImage;

    [Space] [Header("Animation Values")] [SerializeField]
    private float animMultiplier = 2f;

    [SerializeField] private float animDuration = 0.5f;
    [SerializeField] private float rotateAngle = 30f;

    private void Awake()
    {
        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
        }
        animalImage = gameController.animalSprite;
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameController.OnPlayerAnswer += PlayerAnswer;
        GameController.OnPlayerUnAnswer += PlayerUnAnswer;
        GameController.OnPlayerCorrect += PlayerCorrect;
        GameController.OnPlayerWrong += PlayerWrong;
        GameController.OnGameEnd += OnGameEnd;
    }

    private void PlayerAnswer()
    {
        if (GameController.AnsweredWord != null)
        {
            answerText.text = "Answer : " + GameController.AnsweredWord;
        }
    }

    private void OnGameEnd()
    {
        statusText.text = "GAME FINISHED";
    }

    private void PlayerUnAnswer()
    {
        answerText.text = "Unanswered";
    }

    private void PlayerCorrect()
    {
        animalImage.sprite = correctImage;
        animalImage.rectTransform.DOShakeScale(animDuration, animDuration).SetEase(Ease.InOutQuint);
        statusText.text = "CORRECT";
    }

    private void PlayerWrong()
    {
        animalImage.sprite = incorrectImage;
        animalImage.rectTransform.DOShakeScale(animDuration, animDuration).SetEase(Ease.InOutQuint);
        statusText.text = "INCORRECT";
    }

}

