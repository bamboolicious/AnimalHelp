using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Animal",menuName = "Animal")]
public class Animal : ScriptableObject
{
    public Sprite animalSprite;
    public string correctWord;
    public string wrongWord;
    public string description;

}
