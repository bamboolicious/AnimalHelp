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

    [Space] [Header("Questions")] [SerializeField]
    protected int questionAmount = 10;

    [SerializeField] protected List<Animal> questionList;
    [SerializeField] protected int currentQuestion = 0;

    [Space] [Header("Display")] public Image animalSprite;

    [SerializeField] protected Image countDownImage;
    [SerializeField] protected TextMeshProUGUI countDownText;

    [Space] [Header("Answers")] [SerializeField]
    protected List<AnswerZone> answerZones; //To identify which answer has been chosen by the player

    [SerializeField] protected string correctWord;

    public static string AnsweredWord;

    [Space] [Header("Values")] [SerializeField]
    protected float timeStartGame = 10f; //Time till game starts

    [SerializeField] protected Health health;
    [SerializeField] public static bool isDead = false;
    [SerializeField] protected float timeQuestion = 10f; //Time between questions
    [SerializeField] protected float timeBetweenQuestion = 3f;
    [SerializeField] protected float animSpeed = 0.25f;
    [SerializeField] protected Color selectedColor;
    [SerializeField] protected Sprite correctImage, incorrectImage;


    protected void Awake()
    {
        if (health == null)
        {
            gameObject.GetComponent<Health>();
        }
        GameController.isDead = false;
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
        OnGameStart?.Invoke();
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
        countDownImage.DOFillAmount(0, duration).SetEase(Ease.Linear);
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
        animalSprite.rectTransform.DOShakeScale(animSpeed,animSpeed).SetEase(Ease.InOutQuint); //Animate sprite bounce
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
        ClearVisuals();
        ; //Clear the visual clutter so kids doesn't get confused
        if (AnsweredWord == correctWord)
        {
            animalSprite.sprite = correctImage;
            animalSprite.rectTransform.DOShakeScale(animSpeed, animSpeed).SetEase(Ease.InOutQuint);
        }
        else
        {
            animalSprite.sprite = incorrectImage;
            animalSprite.rectTransform.DOShakeScale(animSpeed, animSpeed).SetEase(Ease.InOutQuint);
            if (health.DecreaseHealth())
            {
                isDead = true;
                UIController.Instance.GameOver(); //GAME OVER
                return;
            }
        }
        currentQuestion++;

        if (currentQuestion >= questionAmount)
        {
            //GAME FINISHED
        }
        else if (currentQuestion < questionAmount)
        {
            StartCoroutine(StartCountDown(timeBetweenQuestion, ShuffleAnswerAndDisplayQuestion));
        }
    }

    public virtual void OnPlayerEnterZone(string word,AnswerZone answerZone)
    {
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
