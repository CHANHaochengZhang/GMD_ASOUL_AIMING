using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class YangtuoController : MonoBehaviour
{

  private float health, maxHealth = 1000;
  private float moveSpeed = 0.03f;
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
  private Scene scene;
  public GameObject enemyRangeAttack;
  private DistanceCalculator distanceCalculator;

  public Image healthSlider;

  public TextMeshProUGUI healthNumber;

  private bool destroyed;

  
  private YangtuoGenerator yangtuoGenerator;


  [Header("Sound")] 
  [SerializeField]private AudioSource audioSource;
  public AudioClip attackSound;
  public AudioClip runningSound;
  
   void Start()
   {
       yangtuoGenerator = GameObject.FindWithTag("EnemyCounter").GetComponent<YangtuoGenerator>();
       health = 800;
       maxHealth = 1000;
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
       distanceCalculator = new DistanceCalculator();
     
       health = 3;
    
       if (health<=0)
       {
           Destroy(gameObject);
  
       }
       
   }

   private void Update()
   {
       body.AddForce(Vector3.down*9);
       scene = SceneManager.GetActiveScene();
       playerDirection = player.transform;
       // Not giving the desired results
       Vector3 lookAt = playerDirection.position;
       lookAt.y = transform.position.y;
       transform.LookAt(lookAt);
       
       distance = distanceCalculator.getDistance(playerDirection.position,transform.position);
      
       if(scene.name=="SceneTwo"){
       FindTarget(distance,awakeDistance,targetPlayer);
       MoveToPlayer();
       
       /*StartCoroutine(RangeAttack());*/
       }

       if (scene.name=="SceneOne")
       {
           /*StartCoroutine(RangeAttack());*/
       }
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
           animator.SetInteger("Attack",0);
           Debug.Log("----range attacking---");
           yield return new WaitForSeconds(1f);
           GameObject instantBullet = Instantiate(enemyRangeAttack, transform.position, transform.rotation) as GameObject;;

           Rigidbody rigidbody = instantBullet.GetComponent<Rigidbody>();

           var x = transform.position.x;
           var y = transform.position.y;
           var z = transform.position.z;
           
           instantBullet.transform.position = new Vector3(x,y+2,z);
           animator.SetInteger("Attack",1);
           rigidbody.velocity = transform.forward * 40f;
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
           yangtuoGenerator.UpdateCount();
           Destroy(gameObject);
       }
   }
   
   
   private void PlayerAttackSound()
   {
       audioSource.clip = attackSound;
       audioSource.Play();
       
   }

   public float GetCurrentHealth()
   {
       return health;
   }

   public bool isDestroyed()
   {
       return destroyed;
   }


}
