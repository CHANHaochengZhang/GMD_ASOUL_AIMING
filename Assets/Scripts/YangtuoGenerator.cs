using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class YangtuoGenerator : MonoBehaviour
{
    
    public GameObject yangtuo;
    public float spwanTime = 1.0f;

    private int killedNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
       
        Debug.Log("generator starts");
        // start a thread
        StartCoroutine(enemyWave());
    }
    
    void Update()
    {
        if (killedNumber==50)
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }

    private void generateEnemy(GameObject yangtuo)
    {
        Debug.Log("generateEnemy called");
        GameObject a = Instantiate(yangtuo) as GameObject;
        Random rd = new Random();
        int x = rd.Next(-35, 39);
        int y = 1;
        int z = rd.Next(-35, 35);
        
        a.transform.position = new Vector3(x, y, z);
        Debug.Log("yangtuo position is: ("+x+","+y+","+z+")");
       
    }

    //generate enemies wave
    IEnumerator enemyWave()
    {
        for (int i = 0; i < 50; i++)
        {
            Debug.Log("wait for 1s");
            yield return new WaitForSeconds(spwanTime);
            generateEnemy(yangtuo);
           
        }
        
    }
    
    // Update is called once per frame

    public void UpdateCount()
    {
        killedNumber++;
        Debug.Log(killedNumber);
    }
    
}
