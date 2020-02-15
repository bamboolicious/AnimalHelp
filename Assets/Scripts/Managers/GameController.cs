using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameController = new GameObject("GameController");
                gameController.AddComponent<GameController>();
            }
            return _instance;
        }
    }

    public delegate void PlayerEvent();

    public static event PlayerEvent OnGameStart,OnGameEnd,OnPlayerCorrect, OnPlayerWrong, OnPlayerAnswer, OnPlayerUnAnswer, OnQuestionChange;

    [SerializeField] protected AnimalManager animalManager;
    
    [Space]
    [Header("Questions")]
    [SerializeField] protected int questionAmount = 10;
    [SerializeField] protected List<Animal> questionList;
    [SerializeField] protected int currentQuestion = 0;

    [Space]
    [Header("Display")]
    [SerializeField] protected Image animalSprite;
    [SerializeField] protected Image countDownImage;
    [SerializeField] protected TextMeshProUGUI countDownText;

    [Space]
    [Header("Answers")]
    [SerializeField] protected List<AnswerZone> answerZones; //To identify which answer has been chosen by the player
    [SerializeField] protected string correctWord;
    
    public static string AnsweredWord;
    
    [Space][Header("Values")]
    [SerializeField] protected float timeStartGame = 10f; //Time till game starts
    [SerializeField] protected float timeQuestion = 10f; //Time between questions
    [SerializeField] protected float timeBetweenQuestion = 3f;
    [SerializeField] protected float animSpeed = 0.25f;


    protected virtual void Awake() => _instance = this;

    // Start is called before the first frame update
    void Start()
    {
        if (questionAmount == 0) return;
        OnStart();
    }

    protected virtual void OnStart()
    {
        OnGameStart?.Invoke();
        questionList = ShuffleQuestion(animalManager.animalList, questionAmount);
        StartCoroutine(StartCountDown(timeStartGame,ShuffleAnswerAndDisplayQuestion));
    }


    protected virtual IEnumerator StartCountDown(float second,Action eventToCall)
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
        OnQuestionChange?.Invoke();
        animalSprite.sprite = questionList[currentQuestion].animalSprite; //Set sprite
        animalSprite.rectTransform.DOPunchScale(Vector2.up, animSpeed, 1, 1f); //Animate sprite bounce

        // bool correctWordPlaced = false; //Check if correct word has been placed. If so, just put wrong words everywhere else
        // while (tempList.Count > 0)
        // {
        //     for (int i = Random.Range(0, tempList.Count); i < tempList.Count; i++) //Random where to put the words
        //     {
        //         if (!correctWordPlaced)
        //         {
        //             DisplayAnswerText(tempList[i].answerText, questionList[currentQuestion].correctWord);
        //             correctWord = questionList[currentQuestion].correctWord; //Keep correct word to check if player gets the answer right
        //             correctWordPlaced = true; //There can only be one correct word
        //         }
        //         else
        //         {
        //             DisplayAnswerText(tempList[i].answerText, questionList[currentQuestion].wrongWord);
        //         }
        //         tempList.RemoveAt(i); //Remove from temp list so doesn't loop over again
        //         i = 0; //So the list starts at the beginning
        //     }
        // }
        // StartCoroutine(StartCountDown(timeQuestion, CheckQuestion)); //START THE COUNTDOWN
    }

    protected virtual void DisplayAnswerText(TextMeshProUGUI displayText, string textToDisplay)
    {
        displayText.DOText(textToDisplay, animSpeed);
    }

    protected virtual void CheckQuestion()
    {
        if (AnsweredWord == correctWord)
        {
            OnPlayerCorrect?.Invoke();
            print("CORRECT");
        }
        else
        {
            OnPlayerWrong?.Invoke();
            print("INCORRECT");
            //Decrease health and check for game over
        }

        currentQuestion++;

        if (currentQuestion >= questionAmount)
        {
            OnGameEnd?.Invoke();
            print("FINISHED");
        }
        else if (currentQuestion < questionAmount)
        {
            StartCoroutine(StartCountDown(timeBetweenQuestion, ShuffleAnswerAndDisplayQuestion));
        }
    }

    public virtual void OnPlayerEnterZone(string word)
    {
        OnPlayerAnswer?.Invoke();
        AnsweredWord = word;
    }

    public virtual void OnPlayerExitZone()
    {
        OnPlayerUnAnswer?.Invoke();
        AnsweredWord = "";
    }
    
}