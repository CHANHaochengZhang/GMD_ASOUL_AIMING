using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngineInternal;
using Utils;

public class CanvasController : MonoBehaviour
{
    private DistanceCalculator distanceCalculator;

    private GameObject gameObjectNPC;
    
    private GameObject gameObjectEnemy;

    private float distanceFromNPC;
    
    private float distanceFromEnemy;

    public Canvas dialogueCanvas;

    public Canvas guideCanvas;

    public Canvas interactButtonCanvas;
    
    public Canvas enemyHealthCanvas;

    private bool isFight = false;
    
    private bool isTalk = false;

    /*public GameObject player;*/

    private Transform playerDirection;

    public GameObject player;

    

    /*private PlayerMovements player;*/
    private string[] mData =
    {
        "Hi I'm xiangwan",
        "This was my home",
        "Until the white monster came",
        "its name is Yangtuo, it pretended to be friendly to us",
        "but gradually grabbed our resources and spirits",
        "I'm the only one left",
        "Could you kill it for us"
    };

//当前对话索引
    private int index = 0;

//用于显示对话的GUI Text
    public TextMeshProUGUI mText;


    // Start is called before the first frame update
    void Start()
    {
        distanceCalculator = new DistanceCalculator();
        gameObjectNPC = GameObject.FindGameObjectWithTag("NPC");
        gameObjectEnemy = GameObject.FindGameObjectWithTag("Yangtuo");
        enemyHealthCanvas.enabled = false;
        guideCanvas.enabled = false;
        interactButtonCanvas.enabled = false;
        playerDirection = player.transform;
        dialogueCanvas.enabled = false;
    
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromNPC = distanceCalculator.getDistance(transform.position, gameObjectNPC.transform.position);
        distanceFromEnemy = distanceCalculator.getDistance(transform.position, gameObjectEnemy.transform.position);
        CheckInteractDistance();
        CheckGuideButton();
        CheckDialogue();
        InitTheFight();
    }

    public void CheckGuideButton()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            guideCanvas.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            guideCanvas.enabled = false;
        }
    }

    public void CheckInteractDistance()
    {
        if (distanceFromEnemy <= 4 || distanceFromNPC<=4 && isTalk == false && isFight == false)
        {
            interactButtonCanvas.enabled = true;
        }
        else
        {
            interactButtonCanvas.enabled = false;
        }
       
 
    }

    public void CheckDialogue()
    {
        if (distanceFromNPC<=4)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactButtonCanvas.enabled = false;
                isTalk = true;
                Talk();
            }
        }
    }


    // Start is called before the first frame update

    public void Talk()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (index < mData.Length)
                {
                    dialogueCanvas.enabled = true;
                    mText.text = "Ava:  " + mData[index];
                    index = index + 1;
                }
                else
                {
                    index = 0;
                    dialogueCanvas.enabled = false;
                    mText.text = "Ava:  " + mData[index];
                }
            }
        }


    public void InitTheFight()
    {
        if (distanceFromEnemy<=4)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("fight fight fight");
                enemyHealthCanvas.enabled = true;
                isFight = true;
                Fight();
            }
        }
    }


    public void Fight()
    {
        
    }
    
    
    }
