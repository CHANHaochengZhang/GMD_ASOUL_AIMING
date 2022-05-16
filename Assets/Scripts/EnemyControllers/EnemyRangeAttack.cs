using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//the bullet object of enemy range attack
public class EnemyRangeAttack : MonoBehaviour
{
    public int damage;
    private void Start()
    {
        damage = 2;
    }
    
    public void GiveDamage(GameObject gameObject)
    {
        Debug.Log("Damage given to: "+gameObject.tag);
        if (gameObject.tag=="Player")
        {
            gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
        }
       
    }
    
    //when the bullet is hit by other objects
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag=="Player")
        {
            GiveDamage(c.gameObject);
        }
    }


    
}