using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityController : MonoBehaviour
{
    public bool isGhost = false;
    public bool isInvisble = false;
    public GameObject detectionObject;
    public GameObject invisibleText;
    public GameObject normalMode;
    public GameObject ghostMode;
    public GameObject invisibleMode;

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

    public void InvisibleAbilityUse()
    {
        InvisibleAbility invisibility = new InvisibleAbility();
        invisibility.Activate(gameObject);
        detectionObject.gameObject.SetActive(false);
        normalMode.gameObject.SetActive(false);
        invisibleMode.gameObject.SetActive(true);
    }
    public void InvisibleAbilityNotUse()
    {
        InvisibleAbility invisibility = new InvisibleAbility();
        invisibility.Disable(gameObject);
        detectionObject.gameObject.SetActive(true);
        normalMode.gameObject.SetActive(true);
        invisibleMode.gameObject.SetActive(false);
    }

    public void GhostAbilityUse()
    {
        GhostAbility ghost = new GhostAbility();
        ghost.Activate(gameObject);
        normalMode.gameObject.SetActive(false);
        ghostMode.gameObject.SetActive(true);
    }

    public void GhostAbilityNotUse()
    {
        GhostAbility ghost = new GhostAbility();
        ghost.Disable(gameObject);
        normalMode.gameObject.SetActive(true);
        ghostMode.gameObject.SetActive(false);
    }

}
