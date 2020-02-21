using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    private bool skipPressed = false;
    [SerializeField] private string sceneToChange;
    [SerializeField] private GameObject skipText;
    // Start is called before the first frame update
    void Start()
    {
        skipPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.anyKeyDown)
        {
            if (!skipPressed)
            {
                skipText.SetActive(true);
                skipPressed = true;
            }
            else if (skipPressed)
            {
                skipText.SetActive(false);
                TransitionManager.Instance.StartLoadScene(sceneToChange);
            }
        }

    }
}
