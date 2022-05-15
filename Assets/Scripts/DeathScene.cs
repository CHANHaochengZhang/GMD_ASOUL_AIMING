using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    private float fadeSpeed;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;

    public Canvas canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        
        text3.enabled = false;
        Invoke("FadeIn",3);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (text3.enabled)
        {
            if (Input.anyKeyDown)
            {
                Debug.Log("----------pressed---");
                SceneManager.LoadScene("StartMenu");
            }

        }

    }

    public void FadeIn()
    {
        text3.enabled = true;
    }
    
    
    private void FadeToClear()
    {
        text1.color = Color.Lerp(text1.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    // 溅现
    private void FadeToBlack()
    {
        text1.color = Color.Lerp(text1.color, Color.red, fadeSpeed * Time.deltaTime);
       
    }

    // 开始溅现，结束渐隐
    private void StartScene()
    {
        FadeToClear();
        // alpha 通道小于等于0.5
        if (text1.color.a <= 0.05f)
        {
            text1.color = Color.clear;
            text1.enabled = false;
            /*sceneStarting = false;*/
        }
    }
  
    
}
