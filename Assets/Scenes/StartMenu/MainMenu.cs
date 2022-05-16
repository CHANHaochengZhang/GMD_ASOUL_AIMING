using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButton : MonoBehaviour
{
   
    public void Play()
    {
        Console.WriteLine("rararn");
        Debug.Log("然然我的然然欸嘿嘿");
        SceneManager.LoadScene("SampleScene");
        Debug.Log("然然我的然然欸嘿嘿");
    }

    public void Quit()
    {
        Console.WriteLine("rararn");
        Application.Quit();
    }
}
