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
  private Animator animator = null;
  
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
     
       FindTarget(distance,awakeDistance,targetPlayer);
       
       if (distance>=2)
       {
           /*animator.Play("Move");*/
           animator.SetInteger("Move",1);
         
            transform.Translate(Vector3.forward * moveSpeed);
            /*Debug.Log(" moving ");*/
       }
       else
       {
           animator.SetInteger("Move",0);
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
       Debug.Log("----finding target");
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
