using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHide : MonoBehaviour
{
    private bool isNearHidingSpot = false;  // Tracks if the player is near a hiding spot
    private bool isHidden = false;          // Tracks if the player is currently hiding
    public GameObject hideText; //the hide text
    public GameObject player; //the player game object

    public void OnHide(InputAction.CallbackContext context)
    {
        if (context.performed && isNearHidingSpot)
        {
            ToggleHide();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            isNearHidingSpot = true;
            hideText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            isNearHidingSpot = false;
            hideText.gameObject.SetActive(false);
        }
    }


    private void ToggleHide()
    {
        if (isHidden)
        {
            // Exit hiding
            isHidden = false;
            player.gameObject.SetActive(true);
        }

        else
        {
            // Enter hiding
            isHidden = true;
            player.gameObject.SetActive(false);
        }
    }
}
