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
  private Transform target;
  private Vector2 moveDirection;

   void Start()
   {
       health = maxHealth;
       Debug.Log("health is "+health);
       health = 0;
       Debug.Log("health is "+health);
       if (health<=0)
       {
           Destroy(gameObject);
           onEnemyKilled?.Invoke(this);
       }
       
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
