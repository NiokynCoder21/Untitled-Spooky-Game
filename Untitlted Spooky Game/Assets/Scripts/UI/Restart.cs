using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void OnRetry(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
