using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SpellingController : GameController
{
    private static SpellingController _instance;
    public new static event PlayerEvent OnGameStart;
    

    // Start is called before the first frame update
    void Start()
    {
        if (questionAmount == 0) return;
        OnStart();
    }

    protected override void OnStart()
    {
        OnGameStart?.Invoke();
        questionList = ShuffleQuestion(animalManager.animalList, questionAmount);
        StartCoroutine(StartCountDown(timeStartGame, ShuffleAnswerAndDisplayQuestion));
    }


    protected override void ShuffleAnswerAndDisplayQuestion()
    {
        base.ShuffleAnswerAndDisplayQuestion();

        List<AnswerZone> tempList = new List<AnswerZone>(answerZones);

        bool
            correctWordPlaced =
                false; //Check if correct word has been placed. If so, just put wrong words everywhere else
        while (tempList.Count > 0)
        {
            for (int i = Random.Range(0, tempList.Count); i < tempList.Count; i++) //Random where to put the words
            {
                if (!correctWordPlaced)
                {
                    DisplayAnswerText(tempList[i].answerText, questionList[currentQuestion].correctWord);
                    correctWord =
                        questionList[currentQuestion]
                            .correctWord; //Keep correct word to check if player gets the answer right
                    correctWordPlaced = true; //There can only be one correct word
                }
                else
                {
                    DisplayAnswerText(tempList[i].answerText, questionList[currentQuestion].wrongWord);
                }

                tempList.RemoveAt(i); //Remove from temp list so doesn't loop over again
                i = 0; //So the list starts at the beginning
            }
        }

        StartCoroutine(StartCountDown(timeQuestion, CheckQuestion)); //START THE COUNTDOWN
    }

    protected override void StartGameEnd()
    {
        base.StartGameEnd();
        LevelController.SpellingWon = true;
    }
}