using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = System.Random;

public class YangtuoGenerator : MonoBehaviour
{
    public GameObject yangtuo;
    public float spwanTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("generator starts");

        //启动辅助线程 start a thread
        StartCoroutine(enemyWave());
    }

    private void generateEnemy()
    {
        Debug.Log("generateEnemy called");
        GameObject a = Instantiate(yangtuo) as GameObject;
        Random rd = new Random();
        int x = rd.Next(-50, 40);
        int y = rd.Next(0, 25);
        int z = rd.Next(-50, 50);
        a.transform.position = new Vector3(x, y, z);
        Debug.Log("yangtuo position is: ("+x+","+y+","+z+")");
    }

    //generate enemies wave
    IEnumerator enemyWave()
    {
        for (int i = 0; i < 300; i++)
      
        {
            Debug.Log("wair for 1s");
            yield return new WaitForSeconds(spwanTime);
      
            generateEnemy();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
