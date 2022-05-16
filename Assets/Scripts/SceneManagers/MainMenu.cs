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
    
            Application.Quit();
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("SceneOne");
    }
    
    public void QuitButton()
    {
        Debug.Log("------------------------------------------");
        Application.Quit();
    }
}
