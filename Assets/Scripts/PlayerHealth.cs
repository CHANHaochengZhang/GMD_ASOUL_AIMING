using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthSlider;

    public TextMeshProUGUI healthNumber;

    private float currentHealth;

    private float maxHealth;
    private void Start()
    {
        currentHealth = 50;
        maxHealth = 100;
    }

    private void Update()
    {
        Debug.Log("----------health");
        healthNumber.text = currentHealth + "%";
        effects();
        HealthBarFiller();
    }
    
    //calculate percentage to adjust the health slider
    void HealthBarFiller()
    {
        healthSlider.fillAmount = currentHealth / maxHealth;
        Debug.Log("health is "+currentHealth / maxHealth);
    }


    //add effects to health bar by health 
    void effects()
    {
        if (currentHealth<71 && currentHealth>30)
        {
            healthNumber.color = Color.yellow;
            healthSlider.color = Color.yellow;
            
        }
        if (currentHealth<31)
        {
            healthNumber.color = Color.red;
            healthSlider.color = Color.red;
        }

      
    }
    
    
    public void UpdateHealthBar(int damage,int maxDamage)
    {
        if (currentHealth>0)
        {
            currentHealth  = currentHealth-damage;
        }
        
        
    }


}
