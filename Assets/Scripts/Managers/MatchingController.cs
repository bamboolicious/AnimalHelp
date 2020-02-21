using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Managers
{
    public class MatchingController : GameController
    {
        private static MatchingController _instance;
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
            StartCoroutine(StartCountDown(timeStartGame,ShuffleAnswerAndDisplayQuestion));
        }

        protected override void ShuffleAnswerAndDisplayQuestion()
        {
            base.ShuffleAnswerAndDisplayQuestion();
        
            List<AnswerZone> tempAnswerList = new List<AnswerZone>(answerZones);
            List<Animal> tempQuestionList = new List<Animal>(questionList);
        
            bool correctWordPlaced = false; //Check if correct word has been placed. If so, just put wrong words everywhere else
            while (tempAnswerList.Count > 0)
            {
                for (int i = Random.Range(0,tempAnswerList.Count); i < tempAnswerList.Count; i++)  //Random where to put the words
                {
                    if (!correctWordPlaced)
                    {
                        DisplayAnswerText(tempAnswerList[i].answerText, tempQuestionList[currentQuestion].correctWord);
                        correctWord = tempQuestionList[currentQuestion].correctWord; //Keep correct word to check if player gets the answer right
                        tempQuestionList.RemoveAt(currentQuestion);
                        correctWordPlaced = true; //There can only be one correct word
                    }
                    else
                    {
                        int newRandom = Random.Range(0, tempQuestionList.Count); //Random between animals
                        DisplayAnswerText(tempAnswerList[i].answerText,tempQuestionList[newRandom].correctWord);
                        tempQuestionList.RemoveAt(newRandom);
                    }
                    tempAnswerList.RemoveAt(i); //Remove from temp list so doesn't loop over again
                    i = 0; //So the list starts at the beginning
                }
            }
            StartCoroutine(StartCountDown(timeQuestion, CheckQuestion)); //START THE COUNTDOWN
        }
    
        protected override void StartGameEnd()
        {
            base.StartGameEnd();
            LevelController.MatchingWon = true;
        }
    }
}