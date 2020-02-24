using TMPro;
using UnityEngine;
using DG.Tweening;
using Managers;
using UnityEngine.UI;


public class AestheticController : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private GameController gameController;
    [SerializeField] private Image animalImage;
    [SerializeField] private Sprite correctImage, incorrectImage;

    [Space] [Header("Animation Values")] [SerializeField]
    private float animMultiplier = 2f;

    [SerializeField] private float animDuration = 0.5f;
    [SerializeField] private float rotateAngle = 30f;

    private void Awake()
    {
        DOTween.Init();

    }
    // Start is called before the first frame update
    private void Start()
    {
        if (gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
        animalImage = gameController.animalSprite;

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
           // answerText.text = "Answer : " + GameController.AnsweredWord;
        }
    }

    private void OnGameEnd()
    {
        //statusText.text = "GAME FINISHED";
    }

    private void PlayerUnAnswer()
    {
        //answerText.text = "Unanswered";
    }

    private void PlayerCorrect()
    {

    }

    private void PlayerWrong()
    {

    }

}

