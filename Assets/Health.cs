using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public int maxHealth = 3;
    public int currentHealth;

    private void Awake()
    {
        if(slider == null)
        {
            throw new Exception("Slider is null in Health");
        }
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
    }
    public bool DecreaseHealth()
    {
        currentHealth--;
        UpdateSlider(currentHealth);
        if(currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void UpdateSlider(int value)
    {
        slider.value = value;
    }
}
