using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YangtuoController : MonoBehaviour
{
  private static event Action<YangtuoController> onEnemyKilled;
  private float health, maxHealth = 10f;
  private float moveSpeed = 5f;
  private Transform playerDirection;
  private PlayerMovements player;
  private Vector2 moveDirection;

   void Start()
   {
       /*GameObject a = Instantiate(player) as GameObject;*/
       player = GameObject.FindObjectOfType<PlayerMovements> ();
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
   }


   private void OnTriggerEnter(Collider c)
   {
       if (c.tag=="ak47Bullet")
       {
           Debug.Log("hit yangtuo!!");
           takeDamage(1);
       }
       Debug.Log("hit yangtuo but not succeed!");
   }

   public void takeDamage(float damageAmount)
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
}