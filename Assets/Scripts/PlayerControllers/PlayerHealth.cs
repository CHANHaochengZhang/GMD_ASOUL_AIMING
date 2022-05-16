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

    private static float _currentHealth = 100;

    private float maxHealth;

    private HealthManager healthManager;
    private void Start()
    {
      
        maxHealth = 100;
        healthManager = new HealthManager();
    }

    private void Update()
    {
        healthNumber.text = _currentHealth + "%";
        effects();
        healthManager.HealthBarFiller(healthSlider, _currentHealth,  maxHealth);
   
        if (_currentHealth<=0)
        {
            SceneManager.LoadScene ("SceneDeath");
            _currentHealth = 100;
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
        if (_currentHealth<71 && _currentHealth>30)
        {
            healthNumber.color = Color.yellow;
            healthSlider.color = Color.yellow;
            
        }
        if (_currentHealth<31)
        {
            healthNumber.color = Color.red;
            healthSlider.color = Color.red;
        }
    }


    public void takeDamage(float damage)
    {
        Debug.Log("player takes damage");
        _currentHealth = _currentHealth - damage;
        Debug.Log("health is "+_currentHealth / maxHealth);
    }
    
    


}
