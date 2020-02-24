using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Health))]
public class GameController : MonoBehaviour
{

    public delegate void PlayerEvent();

    public static event PlayerEvent OnGameStart,
    OnGameEnd,
    OnGameOver,
    OnPlayerCorrect,
    OnPlayerWrong,
    OnPlayerAnswer,
    OnPlayerUnAnswer,
    OnQuestionChange;

    [Header("References")]
    [SerializeField] protected AnimalManager animalManager;
    [SerializeField] protected UIController uiController;
    [SerializeField] protected Image winPanel;
    [SerializeField] protected List<GameObject> decorations, flames;
    [SerializeField] protected SpriteRenderer bg;
    [SerializeField] protected Sprite onFireBG, redBG;
    [SerializeField] protected GameObject tutorial;

    [Space][Header("Questions")]
    [SerializeField] public int questionAmount = 10;

    [SerializeField] protected List<Animal> questionList;
    [SerializeField] protected int currentQuestion = 0;

    [Space][Header("Display")] public Image animalSprite;

    [SerializeField] protected Image countDownImage;
    [SerializeField] protected TextMeshProUGUI countDownText;

    [Space][Header("Answers")][SerializeField]
    protected List<AnswerZone> answerZones; //To identify which answer has been chosen by the player

    [SerializeField] protected string correctWord;

    public static string AnsweredWord;

    [Space][Header("Values")][SerializeField]
    public float timeStartGame = 10f; //Time till game starts

    [SerializeField] protected Health health;
    [SerializeField] public static bool IsDead = false;
    [SerializeField] public float timeQuestion = 10f; //Time between questions
    [SerializeField] public float timeBetweenQuestion = 3f;
    [SerializeField] protected float animSpeed = 0.25f;
    [SerializeField] protected Color selectedColor;
    [SerializeField] protected Sprite correctImage, incorrectImage;
    [SerializeField] protected bool tutDeactiavted = false;

    protected void Awake()
    {
        if (health == null)
        {
            gameObject.GetComponent<Health>();
        }
        GameController.IsDead = false;
        questionList = ShuffleQuestion(animalManager.animalList, questionAmount);
    }

    // Start is called before the first frame update
    void Start()
    {

        if (questionAmount == 0) return;
        OnStart();
    }

    protected virtual void OnStart()
    {
        tutorial.SetActive(true);
        OnGameStart?.Invoke();
        questionList = ShuffleQuestion(animalManager.animalList, questionAmount);
        StartCoroutine(StartCountDown(timeStartGame, ShuffleAnswerAndDisplayQuestion));
    }

    protected virtual IEnumerator StartCountDown(float second, Action eventToCall)
    {
        AnimateCountdown(second);

        while (second > 0) //Countdown to zero;
        {
            countDownText.text = "" + (int) second;
            second -= Time.deltaTime;
            yield return null;
        }

        eventToCall.Invoke();
    }
    protected virtual void AnimateCountdown(float duration)
    {
        countDownImage.fillAmount = 1;
        countDownImage.DOFillAmount(0, duration).SetEase(Ease.Linear).Restart();
    }

    protected virtual List<Animal> ShuffleQuestion(List<Animal> animalList, int amountQuestion)
    {
        List<Animal> tempList = new List<Animal>(animalList);
        List<Animal> randomizedList = new List<Animal>();

        for (int i = 0; i < amountQuestion; i++)
        {
            int randomNum = Random.Range(0, tempList.Count);
            randomizedList.Add(tempList[randomNum]);
            tempList.RemoveAt(randomNum);
        }

        return randomizedList;
    }

    protected virtual void ShuffleAnswerAndDisplayQuestion()
    {
        ClearVisuals();
        animalSprite.sprite = questionList[currentQuestion].animalSprite; //Set sprite
        animalSprite.DOFade(1, animSpeed);
        animalSprite.rectTransform.DOShakeScale(animSpeed, animSpeed).SetEase(Ease.InOutQuint); //Animate sprite bounce
    }

    protected virtual void ClearVisuals()
    {
        //animalSprite.DOFade(0, animSpeed);
        foreach (var zone in answerZones)
        {
            zone.answerText.text = "";
            zone.answerText.color = Color.white;
        }
    }

    protected void DisplayAnswerText(TextMeshProUGUI displayText, string textToDisplay)
    {
        displayText.DOText(textToDisplay, animSpeed);
    }

    protected virtual void CheckQuestion()
    {
        ClearVisuals();; //Clear the visual clutter so kids doesn't get confused
        if (AnsweredWord == correctWord)
        {
            animalSprite.sprite = correctImage;
            animalSprite.rectTransform.DOShakeScale(animSpeed, animSpeed).SetEase(Ease.InOutQuint);
            AudioManager.Instance.PlayCorrect();
        }
        else
        {
            bg.sprite = redBG;
            animalSprite.sprite = incorrectImage;
            animalSprite.rectTransform.DOShakeScale(animSpeed, animSpeed).SetEase(Ease.InOutQuint);
            AudioManager.Instance.PlayIncorrect();
            if (health.DecreaseHealth())
            {
                IsDead = true;
                UIController.Instance.GameOver(); //GAME OVER
                return;
            }
        }
        currentQuestion++;

        if (currentQuestion >= questionAmount)
        {
            StartGameEnd();
        }
        else if (currentQuestion < questionAmount)
        {
            bg.sprite = onFireBG;
            StartCoroutine(StartCountDown(timeBetweenQuestion, ShuffleAnswerAndDisplayQuestion));
        }
    }

    protected virtual void StartGameEnd()
    {
        StartCoroutine(OnGameFinished());
    }

    IEnumerator OnGameFinished()
    {
        bg.DOColor(Color.white, 1);
        AudioManager.Instance.PlayGameEnd();
        winPanel.gameObject.SetActive(true);
        winPanel.rectTransform.DOShakeScale(0.25f, 0.25f).SetUpdate(true);
        foreach (var flame in flames)
        {
            flame.transform.DOScale(Vector3.zero, 1);
            flame.SetActive(false);
        }
        foreach (var deco in decorations)
        {
            deco.SetActive(true);
            deco.transform.DOShakeScale(0.25f, 0.25f).SetUpdate(true);
        }
        yield return new WaitForSeconds(5);
        TransitionManager.Instance.StartLoadScene("LevelSelect");
    }

    public virtual void OnPlayerEnterZone(string word, AnswerZone answerZone)
    {
        if (!tutDeactiavted)
        {
            tutDeactiavted = true;
            tutorial.SetActive(false);
        }

        AnsweredWord = word;
        foreach (var zone in answerZones) //Turn other answerZones text white
        {
            zone.answerText.color = Color.white;
        }
        answerZone.answerText.DOColor(selectedColor, animSpeed);
    }

    public virtual void OnPlayerExitZone()
    {
        AnsweredWord = "";
    }

}