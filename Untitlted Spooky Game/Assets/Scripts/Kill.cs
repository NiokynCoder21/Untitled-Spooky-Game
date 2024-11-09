using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public PlayerController controller;
    public GameObject killText;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBack"))
        {
            killText.gameObject.SetActive(true);

            if (controller != null)
            {
               // controller.
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyBack"))
        {
            killText.gameObject.SetActive(true);
        }
    }
}
