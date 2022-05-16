using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("___________________QUITRQWRQWRQWR");
            Application.Quit();
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("SceneOne");
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("Settings");
    }
    
    public void QuitButton()
    {
        Debug.Log("------------------------------------------");
        Application.Quit();
    }
}
