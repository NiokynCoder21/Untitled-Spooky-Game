using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerEnegy : MonoBehaviour
{
    public float maxEnergy = 200; //max health in the game
    public float currentEnergy; //current health in the game
    public HealthBars healthBar; //reference to the healthBar game object
    public AudioClip bloodDrinkSound; //this is the audio clip for when the player eats

    void Start()
    {
        currentEnergy = maxEnergy; //set current health to max health
        healthBar.SetMaxHealth(maxEnergy); //set health bar to max health  

    }

    public void LossEnergy(float energy) //function for when the player loses energy
    {
        currentEnergy -= energy; //this reduces energy from current energy and assigns the current energy
        healthBar.SetHealth(currentEnergy); //set healthbar to current energy

        if (currentEnergy <= 0) //if current energy is less than or equal to zero
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single); //load the game over screen
        }
    }

    public void GainEnergy(float energy) //function for when the player gets energy
    {
        AudioSource audio = GetComponent<AudioSource>(); //get component audio source and store as audio
        audio.clip = bloodDrinkSound;
        audio.Play(); //play the auido clip
        currentEnergy += energy; //this increases energy from current energy and assigns the current energy
        healthBar.SetHealth(currentEnergy); //set healthbar to current energy
    }

    //Brakeys.(2020, Febuary 9). How to make a Health bar in Unity![Video] https://www.youtube.com/watch?v=BLfNP4Sc_iA&list=PLt1E2jJc5nDj6KQi6BVJElz3vqFmg-B8I&index=4 
}
