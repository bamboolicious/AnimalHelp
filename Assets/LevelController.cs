using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static bool MatchingWon,SpellingWon;
    [SerializeField] private SpriteRenderer spellingSprite,matchingSprite;
    [SerializeField] private Sprite treeSprite, fireSprite;


    // Start is called before the first frame update
    void Start()
    {
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
        spellingSprite.sprite = treeSprite;
        matchingSprite.sprite = treeSprite;
    }
    private void CompleteSpelling()
    {
        spellingSprite.sprite = treeSprite;
    }
    private void CompleteMatching()
    {
        matchingSprite.sprite = treeSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
