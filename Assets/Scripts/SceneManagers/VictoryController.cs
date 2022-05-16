using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryController : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Quit",5);
        StartCoroutine(ChangeNumber());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeNumber()
    {
        for (int i = 5; i >0; i--)
        {
           text.text = i.ToString();
           yield return new WaitForSeconds(1);
        }
    }
    
    public void Quit()
    {
        SceneManager.LoadScene("StartMenu");
    }
}


