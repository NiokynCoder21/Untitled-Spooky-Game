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

    public void onMainMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GoMainMenu();
        }
    }

    public void onStart(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartGame();
        }
    }

    public void onQuit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            QuitGame();
        }
    }
    
    public void onContinue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ContinueGame();
        }
    }

    public void RespawnPlayer()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tut", LoadSceneMode.Single);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
