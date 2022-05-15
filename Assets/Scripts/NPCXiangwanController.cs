using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCXiangwanController : MonoBehaviour
{


    private Animator animator;

    public Canvas canvas;
    
    private bool isTalk = false;

    /*public GameObject player;*/
    
    private Transform playerDirection;

    public GameObject player;

    public float distance=10f;
   
    /*private PlayerMovements player;*/

    

    private string[] mData=
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
    private int index=0;
//用于显示对话的GUI Text
    public TextMeshProUGUI mText;
//对话标示贴图
    public Texture mTalkIcon;
    

    // Start is called before the first frame update
    void Start()
    {
        playerDirection = player.transform;
        canvas.enabled = false;
       
    // animator = GetComponent<Animator>();
        // animator.SetInteger("Talk",0);
        //
        // animator.SetInteger("Talk",1);
        /*mText.text="NPC:"+mData[index];*/
        /*index=index+1;*/

    }

    // Update is called once per frame
    void Update()
    {
        distance = (playerDirection.position - transform.position).magnitude;
    
        if (distance<=5f)
        {
           
            if (Input.GetKeyDown(KeyCode.E))
            {
                canvas.enabled = true;
                isTalk = true;
                Talk();
                /*StartCoroutine(TalkStart());*/
                /*TalkStart();*/
            }
        }
        else
        {
            canvas.enabled = false;
        }
       
    }

    private void OnGUI()
    {
        if(isTalk)
{
              /*//禁用系统鼠标指针
                Cursor.visible = true;
                Rect mRect=new Rect(Input.mousePosition.x-mTalkIcon.width,
                    Screen.height-Input.mousePosition.y-mTalkIcon.height,
                    mTalkIcon.width,mTalkIcon.height);
              //绘制自定义鼠标指针
                GUI.DrawTexture(mRect,mTalkIcon);*/
}
    }


    /*IEnumerator TalkStart()
    {
        while (index<mData.Length)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("talking---number---");
                yield return new WaitForSeconds(0.1f);
                mText.text = mData[index];
                /*Cursor.visible = true;#1#
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }*/
       
    /*}*/
  
    
    public void Talk()
    {
        if(Input.GetKeyDown(KeyCode.E) )
        {
             //绘制指定索引的对话文本
            if(index<mData.Length)
            {
                mText.text="Xiangwan:"+mData[index];
                index=index+1;
            }else
            {
                index=0;
                canvas.enabled = false;
                mText.text="NPC:"+mData[index];
            }
        }
    }
  
    
    
}
