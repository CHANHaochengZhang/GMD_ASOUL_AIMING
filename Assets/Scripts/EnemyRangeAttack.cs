using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    public float lifeTime;

    public int damage;

    public float speed;

    private void Start()
    {
        damage = 2;
    }

    public void CheckHit()
    {
        
    }
    public void GiveDamage(GameObject gameObject)
    {
        Debug.Log("Damage given to: "+gameObject.tag);
        if (gameObject.tag=="Player")
        {
            /*PlayerAttackSound();*/
            /*animator.SetInteger("Attack",1);*/
            gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
        }
       
    }
    
    private void OnTriggerEnter(Collider c)
    
    {  Debug.Log("range hit!!");
        Debug.Log("source is --------"+c.gameObject.tag);
        if (c.gameObject.tag=="Player")
        {
            Debug.Log("range hit successfully!!!!!!!!!!!!!!");
            GiveDamage(c.gameObject);
        }
      
    }
    
    //
    //
    // public void Go ()
    // {
    //     GetComponent<Rigidbody>().AddForce(transform.forward * 1, ForceMode.VelocityChange);
    // }
    //
    // void FixedUpdate()
    // {
    //     GetComponent<Rigidbody>().AddForce(transform.forward * beamVelocity, ForceMode.Acceleration);
    // }
    //
    //
    // void Start()
    // {
    //     Destroy(gameObject, LifeTime);
    // }
    //
    // void Update()
    // {
    //     //transform.position += transform.forward * Speed * Time.deltaTime;       
    // }
    //
    // void OnCollisionEnter(Collision collision)
    // {        
    //     Destroy(gameObject);
    // }
}