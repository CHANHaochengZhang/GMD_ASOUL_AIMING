using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class YangtuoGenerator : MonoBehaviour
{
    private List<GameObject> list;
    public GameObject yangtuo;
    public float spwanTime = 1.0f;

    private int killedNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        list = new List<GameObject>();
        Debug.Log("generator starts");

        //启动辅助线程 start a thread
        StartCoroutine(enemyWave());
    }
    
    void Update()
    {
        if (killedNumber==30)
        {
            SceneManager.LoadScene("SceneOne");
        }
    }

    private void generateEnemy(GameObject yangtuo)
    {
        Debug.Log("generateEnemy called");
        GameObject a = Instantiate(yangtuo) as GameObject;
        Random rd = new Random();
        int x = rd.Next(-50, 40);
        int y = rd.Next(0, 25);
        int z = rd.Next(-50, 50);
        y = 0;
        a.transform.position = new Vector3(x, y, z);
        Debug.Log("yangtuo position is: ("+x+","+y+","+z+")");
       
    }

    //generate enemies wave
    IEnumerator enemyWave()
    {
        for (int i = 0; i < 30; i++)
        {
            Debug.Log("wait for 1s");
            yield return new WaitForSeconds(spwanTime);
            generateEnemy(yangtuo);
            list.Add(yangtuo);
        }
        
    }
    
    // Update is called once per frame

    public void UpdateCount()
    {
        killedNumber++;
        Debug.Log(killedNumber);
    }
    
}
