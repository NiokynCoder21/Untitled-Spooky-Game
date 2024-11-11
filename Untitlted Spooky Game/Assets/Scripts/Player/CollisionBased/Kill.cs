using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kill : MonoBehaviour
{
    public PlayerController controller;
    public GameObject killText;
    public GameObject player;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBack"))
        {
            killText.gameObject.SetActive(true);

            if (controller != null)
            {
                controller.SetEnemyTurned(true);

                if (controller.hasKilled == true)
                {
                    controller.hasKilled = false;
                    killText.gameObject.SetActive(false);

                    Transform enemyParent = other.transform;

                    // Traverse up the hierarchy until we reach an object tagged as "Enemy"
                    while (enemyParent != null && !enemyParent.CompareTag("Enemy"))
                    {
                        enemyParent = enemyParent.parent;
                    }

                    // If we found an enemy parent, destroy it
                    if (enemyParent != null)
                    {
                        Destroy(enemyParent.gameObject); // Destroy the specific enemy object, e.g., Enemy 3
                    }
                }
            }
        }

        if (other.CompareTag("Rat"))
        {
            Rat rat = new Rat();
            rat.ApplyEffect(player);

            Transform ratParent = other.transform;

            // Traverse up the hierarchy until we reach an object tagged as "Enemy"
            while (ratParent != null && !ratParent.CompareTag("RatDad"))
            {
                ratParent = ratParent.parent;
            }

            // If we found an enemy parent, destroy it
            if (ratParent != null)
            {
                Destroy(ratParent.gameObject); // Destroy the specific enemy object, e.g., Enemy 3
            }
        }

        if (other.CompareTag("Rabbit"))
        {
            Rabbit rabbit = new Rabbit();
            rabbit.ApplyEffect(player);

            Transform rabbitParent = other.transform;

            // Traverse up the hierarchy until we reach an object tagged as "Enemy"
            while (rabbitParent != null && !rabbitParent.CompareTag("RabbitDad"))
            {
                rabbitParent = rabbitParent.parent;
            }

            // If we found an enemy parent, destroy it
            if (rabbitParent != null)
            {
                Destroy(rabbitParent.gameObject); // Destroy the specific enemy object, e.g., Enemy 3
            }
        }

        if (other.CompareTag("Winner"))
        {
            SceneManager.LoadScene("Win", LoadSceneMode.Single);
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
                    controller.hasKilled = false;
                    killText.gameObject.SetActive(false);

                    Transform enemyParent = other.transform;

                    // Traverse up the hierarchy until we reach an object tagged as "Enemy"
                    while (enemyParent != null && !enemyParent.CompareTag("Enemy"))
                    {
                        enemyParent = enemyParent.parent;
                    }

                    // If we found an enemy parent, destroy it
                    if (enemyParent != null)
                    {
                        Destroy(enemyParent.gameObject); // Destroy the specific enemy object, e.g., Enemy 3
                    }
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyBack"))
        {
            killText.gameObject.SetActive(false);

            if (controller != null)
            {
                controller.SetEnemyTurned(false);
            }
        }
    }
}
