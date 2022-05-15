using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class PlayerHealth : MonoBehaviour
{
    public Image healthSlider;

    public TextMeshProUGUI healthNumber;

    private float currentHealth;

    private float maxHealth;

    private HealthManager healthManager;
    private void Start()
    {
        currentHealth = 80;
        maxHealth = 100;
        healthManager = new HealthManager();
    }

    private void Update()
    {
        healthNumber.text = currentHealth + "%";
        effects();
        healthManager.HealthBarFiller(healthSlider, currentHealth,  maxHealth);
        if (currentHealth<=0)
        {
            SceneManager.LoadScene ("SceneDeath");
        }
    }
    
    //calculate percentage to adjust the health slider
    /*void HealthBarFiller()
    {
        healthSlider.fillAmount = currentHealth / maxHealth;
    }*/


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


    public void takeDamage(float damage)
    {
        Debug.Log("player takes damage");
        currentHealth = currentHealth - damage;
        Debug.Log("health is "+currentHealth / maxHealth);
    }
    
    


}
