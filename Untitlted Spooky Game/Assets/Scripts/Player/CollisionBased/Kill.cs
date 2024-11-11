using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kill : MonoBehaviour
{
    public PlayerController controller; //reference to player contrller script
    public GameObject killText; //text that says what to press to kill
    public GameObject player; //this is the object with the player energy script


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBack")) //when collides with game object with enemy back tag 
        {
            killText.gameObject.SetActive(true); //show kill text

            if (controller != null)
            {
                controller.SetEnemyTurned(true); //the player is behind the enemy

                if (controller.hasKilled == true)
                {
                    controller.hasKilled = false; //the enemy is now dead
                    killText.gameObject.SetActive(false); //hide kill text

                    Transform enemyParent = other.transform; 
                   
                    while (enemyParent != null && !enemyParent.CompareTag("Enemy"))  //Go up the hiercrachy until it reaches a gameobject enemy
                    {
                        enemyParent = enemyParent.parent; //this game object found is the enemy parent
                    }
  
                    if (enemyParent != null) 
                    {
                        Destroy(enemyParent.gameObject); // Destroy the game object
                    }
                }
            }
        }

        if (other.CompareTag("Rat"))
        {
            Rat rat = new Rat();
            rat.ApplyEffect(player); //this increases player energy

            Transform ratParent = other.transform;

            while (ratParent != null && !ratParent.CompareTag("RatDad")) //Go up the hiercrachy until it reaches a gameobject rat
            {
                ratParent = ratParent.parent;
            }

            if (ratParent != null)
            {
                Destroy(ratParent.gameObject); // Destroy the game object
            }
        }

        if (other.CompareTag("Rabbit"))
        {
            Rabbit rabbit = new Rabbit();
            rabbit.ApplyEffect(player); //this increases player energy

            Transform rabbitParent = other.transform;

            while (rabbitParent != null && !rabbitParent.CompareTag("RabbitDad")) //Go up the hiercrachy until it reaches a gameobject rabbit
            {
                rabbitParent = rabbitParent.parent;
            }

            if (rabbitParent != null)
            {
                Destroy(rabbitParent.gameObject); // Destroy the game object
            }
        }

        if (other.CompareTag("Winner"))
        {
            SceneManager.LoadScene("Win", LoadSceneMode.Single); //this will transition the game to win scence
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyBack"))
        {
            if (controller != null)
            {
                if (controller.hasKilled == true)
                {
                    controller.hasKilled = false; //the enemy is now dead
                    killText.gameObject.SetActive(false); //hide kill text

                    Transform enemyParent = other.transform;

                    while (enemyParent != null && !enemyParent.CompareTag("Enemy"))  //Go up the hiercrachy until it reaches a gameobject enemy
                    {
                        enemyParent = enemyParent.parent;
                    }

                    if (enemyParent != null)
                    {
                        Destroy(enemyParent.gameObject);
                    }
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyBack"))
        {
            killText.gameObject.SetActive(false); //disable kill text

            if (controller != null)
            {
                controller.SetEnemyTurned(false); //the player is not behind the enemy
            }
        }
    }
}
