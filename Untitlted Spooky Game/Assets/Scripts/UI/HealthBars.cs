using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    public Slider slider; //references the slider gameObject 

    public void SetMaxHealth(float health) //a void that sets the max health 
    {
        slider.maxValue = health; //ensure the slider max is equal to max health
        slider.value = health; //ensure the matches the health amount
    }

    public void SetHealth(float health)//a void to set the health 
    {
        slider.value = health; //ensure the matches the health amount
    }

    //Brakeys.(2020, Febuary 9). How to make a Health bar in Unity![Video] https://www.youtube.com/watch?v=BLfNP4Sc_iA&list=PLt1E2jJc5nDj6KQi6BVJElz3vqFmg-B8I&index=4 
}
