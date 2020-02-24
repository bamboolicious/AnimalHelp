using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LevelController : MonoBehaviour
{
    public static bool MatchingWon = false;
    public static bool SpellingWon = false;
    [SerializeField] private List<GameObject> decorations;
    [SerializeField] private AnimalManager animalManager;
    [SerializeField] private GameObject spellingZone, matchingZone;
    [SerializeField] private SpriteRenderer spellingSprite, matchingSprite;
    [SerializeField] private Sprite fireSprite;
    [SerializeField] private GameObject winPanel;

    private int timesClicked = 0;
    [SerializeField] private GameObject hiddenCredits;

    void Start()
    {
        hiddenCredits.SetActive(false);
        spellingSprite.sprite = fireSprite;
        matchingSprite.sprite = fireSprite;
        if (MatchingWon && SpellingWon)
        {
            CompleteBoth();
        }
        else if (MatchingWon)
        {
            CompleteMatching();
        }
        else if (SpellingWon)
        {
            CompleteSpelling();
        }
    }

    private void CompleteBoth()
    {
        foreach (var deco in decorations)
        {
            deco.SetActive(true);
        }
        spellingZone.SetActive(false);
        matchingZone.SetActive(false);
        spellingSprite.sprite = animalManager.animalList[Random.Range(0, animalManager.animalList.Count)].animalSprite;
        matchingSprite.sprite = animalManager.animalList[Random.Range(0, animalManager.animalList.Count)].animalSprite;
        StartCoroutine(StartGameEnd());
    }

    IEnumerator StartGameEnd()
    {
        winPanel.SetActive(true);
        yield return new WaitForSeconds(5);
        TransitionManager.Instance.StartLoadScene("Cutscene_End");
    }
    private void CompleteSpelling()
    {
        spellingZone.SetActive(false);
        spellingSprite.sprite = animalManager.animalList[Random.Range(0, animalManager.animalList.Count)].animalSprite;
    }
    private void CompleteMatching()
    {
        matchingZone.SetActive(false);
        matchingSprite.sprite = animalManager.animalList[Random.Range(0, animalManager.animalList.Count)].animalSprite;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.F5))
        {
            if (timesClicked < 5)
            {
                timesClicked++;
            }
            else
            {
                hiddenCredits.SetActive(!hiddenCredits.activeInHierarchy);
            }
        }
    }
}