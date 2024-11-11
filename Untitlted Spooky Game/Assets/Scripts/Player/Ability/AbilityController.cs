using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityController : MonoBehaviour
{
    public bool isGhost = false; //is the ability active
    public bool isInvisble = false; //is the ability active
    public GameObject detectionObject; //in my game detection works using trigger enter game object so i disbale it to make the player invisible
    public GameObject normalMode; //this is for post processing
    public GameObject ghostMode; //this is for post processing
    public GameObject invisibleMode; //this is for post processing

    public void onGhostAbility(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGhost == true)
            {
                isGhost = false;
            }

            else
            {
                isGhost = true;
            }
        }
    }

    public void onInvisible(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isInvisble == true)
            {
                isInvisble = false;
            }

            else
            {
                isInvisble = true;
            }
        }
    }

    private void Update()
    {
        if (isGhost == true)
        {
            GhostAbilityUse();
        }

        else
        {
            GhostAbilityNotUse();
        }

        if (isInvisble == true)
        {
            InvisibleAbilityUse();
        }

        else
        {
            InvisibleAbilityNotUse();
        }
    }

    public void InvisibleAbilityUse() //ability activate
    {
        InvisibleAbility invisibility = new InvisibleAbility();
        invisibility.Activate(gameObject);
        detectionObject.gameObject.SetActive(false);
        normalMode.gameObject.SetActive(false);
        invisibleMode.gameObject.SetActive(true);
    }
    public void InvisibleAbilityNotUse() //ability deactive
    {
        InvisibleAbility invisibility = new InvisibleAbility();
        invisibility.Disable(gameObject);
        detectionObject.gameObject.SetActive(true);
        normalMode.gameObject.SetActive(true);
        invisibleMode.gameObject.SetActive(false);
    }

    public void GhostAbilityUse() //ability activate
    {
        GhostAbility ghost = new GhostAbility();
        ghost.Activate(gameObject);
        normalMode.gameObject.SetActive(false);
        ghostMode.gameObject.SetActive(true);
    }

    public void GhostAbilityNotUse() //ability deactive
    {
        GhostAbility ghost = new GhostAbility();
        ghost.Disable(gameObject);
        normalMode.gameObject.SetActive(true);
        ghostMode.gameObject.SetActive(false);
    }

}
