using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class YangtuoController : MonoBehaviour
{
  private static event Action<YangtuoController> onEnemyKilled;
  private float health, maxHealth = 10f;
  private float moveSpeed = 0.003f;
  private Transform playerDirection;
  private GameObject targetPlayer;
  private PlayerMovements player;
  private Vector2 moveDirection;
  private float attack = 2;
  private float distance;
  private float awakeDistance;
  private float timer;
  public float timeBetweenAttacks = 3f;
  public float jumpForce = 3f;
  public Vector3 velocity; //y axis
  private Animator animator = null;
  private Rigidbody body;
  private RaycastHit hit;
  private float detectDistance;
  public GameObject enemyRangeAttack;


  [Header("Sound")] 
  [SerializeField]private AudioSource audioSource;
  public AudioClip attackSound;
  public AudioClip runningSound;
  
   void Start()
   {
       /*GameObject a = Instantiate(player) as GameObject;*/
       player = GameObject.FindObjectOfType<PlayerMovements> ();
       distance=100;
       awakeDistance = 3;
       timer = timeBetweenAttacks;
       animator = GetComponent<Animator>();
       targetPlayer = GameObject.FindWithTag("Player");
       targetPlayer = GameObject.Find("Player");
      
       audioSource = GetComponent<AudioSource>();
       health = maxHealth;
       body = transform.GetComponent<Rigidbody>();
       
       Debug.Log("health is "+health);
       health = 3;
       Debug.Log("health is "+health);
       if (health<=0)
       {
           Destroy(gameObject);
           onEnemyKilled?.Invoke(this);
       }
       
   }

   private void Update()
   {
       
       playerDirection = player.transform;
       // Not giving the desired results
       Vector3 lookAt = playerDirection.position;
       lookAt.y = transform.position.y;
       transform.LookAt(lookAt);
       distance = (playerDirection.position - transform.position).magnitude;
     Debug.Log("yangtuo distance is "+distance);
       FindTarget(distance,awakeDistance,targetPlayer);
       MoveToPlayer();
       /*StartCoroutine(RangeAttack());*/

   }


   private void OnTriggerEnter(Collider c)
   {
       if (c.tag=="ak47Bullet")
       {
           Debug.Log("hit yangtuo!!");
           TakeDamage(1);
       }
      
   }


   public void FindTarget(float currentDistance,float awakeDistance,GameObject gameObject)
   {
  
       /*Debug.Log("distance is "+ distance );*/
       if (currentDistance<awakeDistance)
            {
                timer += Time.deltaTime;
               if (timer>timeBetweenAttacks)
               {
                   GiveDamage(gameObject);
                   timer = 0;
               }
            }
   
   }


   public void MoveToPlayer()
   {

       /*
       if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out hit, 10f))
       {
           //往该角色提高半米的位置的前方发射一条10米的射线，如果有射中障碍物层级的物体
           float rotateDir = Vector3.Dot(transform.right, hit.normal); //获取角色右方向与击中位置的法线的点乘结果，主要用于判断障碍物位置，是在角色左边还是右边
           Debug.DrawRay(transform.position + Vector3.up * 0.5f, transform.forward * 7, Color.red);
           Debug.DrawRay(hit.point, hit.normal, Color.blue);
           if (rotateDir >= 0)
           {
               //如果大于等于0
               transform.Rotate(transform.up * 90 * Time.deltaTime); //则往顺时针方向以90度每秒的方向转动
               /*lookReTime = 0; //避免在躲避障碍物的时候，还会朝向玩家,易发生相反转向#1#
           }
           else
           {
               transform.Rotate(transform.up * -90 * Time.deltaTime); //则往逆时针方向以90度每秒的方向转动
               /*lookReTime = 0;#1#
           }
           */

           if (distance >= 2)
           {
               /*animator.Play("Move");*/
               animator.SetInteger("Move", 1);
               transform.Translate(Vector3.forward * moveSpeed);
               /*Debug.Log(" moving ");*/
           }
           else
           {
               animator.SetInteger("Move", 0);
               Jump();
           }

       }
   

   public void Jump()
   {
       RaycastHit hit;
       if(Physics.Raycast(transform.position,Vector3.down,out hit))
       {
            
           /*
           body.AddForce(Vector3.up * 4);*/
           body.AddForce(0,5,0);
       }
   }



   IEnumerator RangeAttack()
   {

       for (int i = 0; i < 300; i++)
       {
           yield return new WaitForSeconds(5f);
           GameObject instantBullet = Instantiate(enemyRangeAttack, transform.position, transform.rotation) as GameObject;;

           Rigidbody rigidbody = instantBullet.GetComponent<Rigidbody>();

           rigidbody.velocity = transform.forward * 2f;
       }
       

   }
   
   
   
   
   
   public void GiveDamage(GameObject gameObject)
   {
       Debug.Log("Damage given to: "+gameObject.tag);
       if (gameObject.tag=="Player")
       {
           PlayerAttackSound();
           /*animator.SetInteger("Attack",1);*/
           gameObject.GetComponent<PlayerHealth>().takeDamage(attack);
       }
       
   }

   public void TakeDamage(float damageAmount)
   {
       Debug.Log("Damage taken from player: "+damageAmount);
       health = health-damageAmount;
       Debug.Log("Current health: "+health);
       if (health<=0)
       {
           Destroy(gameObject);
           onEnemyKilled?.Invoke(this);
       }
   }
   
   
   private void PlayerAttackSound()
   {
       audioSource.clip = attackSound;
       audioSource.Play();
       
   }
   
   
   
}
