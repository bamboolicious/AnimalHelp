using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpellingController : MonoBehaviour
{
    private static SpellingController _instance;
    public static SpellingController Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject spellingController = new GameObject("SpellingController");
                spellingController.AddComponent<SpellingController>();
            }
            return _instance;
        }
    }

    public delegate void PlayerEvent();
    public static event PlayerEvent OnPlayerCorrect,OnPlayerWrong,OnPlayerAnswer,OnPlayerUnAnswer,OnQuestionChange,OnGameEnd;

    [SerializeField] private AnimalManager animalManager;
    
    [Space]
    [Header("Questions")]
    [SerializeField] private int questionAmount = 10;
    [SerializeField] private List<Animal> questionList;
    [SerializeField] private int currentQuestion = 0;

    [Space]
    [Header("Display")]
    [SerializeField] private Image animalSprite;
    [SerializeField] private Image countDownImage;

    [SerializeField] private TextMeshProUGUI countDownText;
    //[SerializeField] private List<TextMeshProUGUI> texts;
    [Space]
    [Header("Answers")]
    [SerializeField] private List<AnswerZone> answerZones; //To identify which answer has been chosen by the player
    [SerializeField] private string correctWord;
    
    public static string AnsweredWord;
    
    [Space][Header("Values")]
    [SerializeField] private float timeStartGame = 10f; //Time till game starts
    [SerializeField] private float timeQuestion = 10f; //Time between questions
    [SerializeField] private float animSpeed = 0.25f;


    private void Awake() => _instance = this;

    // Start is called before the first frame update
    void Start()
    {
        if (questionAmount == 0) return;
        OnStart();
    }

    void OnStart()
    {
        questionList = ShuffleQuestion(animalManager.animalList, questionAmount);
        ShuffleAnswerAndDisplayQuestion(answerZones, questionList[currentQuestion]);
        StartCoroutine(StartCountDown(timeStartGame));
    }

    private IEnumerator StartCountDown(float second)
    {
        countDownText.text = "";
        countDownImage.fillAmount = 1; //Fill up the countdown image

        //Tween fillAmount to zero based on second and call a function
        DOTween.To(() => countDownImage.fillAmount, x => countDownImage.fillAmount = x, 0, second).SetEase(Ease.Linear);
        while (second > 0) //Countdown to zero;
        {
            countDownText.text = "" + (int) second;
            second -= Time.deltaTime;
            yield return null;
        }
        CheckQuestion();
    }

    private List<Animal> ShuffleQuestion(List<Animal> animalList, int amountQuestion)
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

    private void ShuffleAnswerAndDisplayQuestion(List<AnswerZone> answerList, Animal question)
    {
        List<AnswerZone> tempList = new List<AnswerZone>(answerList);

        animalSprite.sprite = question.animalSprite; //Set sprite
        animalSprite.rectTransform.DOPunchScale(Vector2.up, animSpeed, 1, 1f); //Animate sprite bounce

        bool correctWordPlaced = false; //Check if correct word has been placed. If so, just put wrong words everywhere else
        while (tempList.Count > 0)
        {
            for (int i = Random.Range(0, tempList.Count); i < tempList.Count; i++) //Random where to put the words
            {
                if (!correctWordPlaced)
                {
                    DisplayAnswerText(tempList[i].answerText, question.correctWord);
                    correctWord = question.correctWord; //Keep correct word to check if player gets the answer right
                    correctWordPlaced = true; //There can only be one correct word
                }
                else
                {
                    DisplayAnswerText(tempList[i].answerText, question.wrongWord);
                }
                tempList.RemoveAt(i); //Remove from temp list so doesn't loop over again
                i = 0; //So the list starts at the beginning
            }
        }
    }

    private void DisplayAnswerText(TextMeshProUGUI displayText, string textToDisplay)
    {
        displayText.DOText(textToDisplay, animSpeed);
    }

    private void NextQuestion()
    {
        OnQuestionChange?.Invoke();
        currentQuestion++;
        if (currentQuestion >= questionAmount)
        {
            OnGameEnd?.Invoke();
            print("FINISHED");
        }
        else if (currentQuestion < questionAmount)
        {
            ShuffleAnswerAndDisplayQuestion(answerZones, questionList[currentQuestion]);
            StartCoroutine(StartCountDown(timeQuestion));
        }
    }

    private void CheckQuestion()
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
        NextQuestion();
    }

    public static void OnPlayerEnterZone(string word)
    {
        OnPlayerAnswer?.Invoke();
        AnsweredWord = word;
        //print("ANSWER : " + word);
    }

    public static void OnPlayerExitZone()
    {
        OnPlayerUnAnswer?.Invoke();
        AnsweredWord = "";
        //print("UNANSWERED");
    }
    
}