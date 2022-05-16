using TMPro;
using UnityEngine.UI;

namespace Utils
{
    public class HealthManager
    {
      
        public Image healthSlider;

        public TextMeshProUGUI healthNumber;

        private float currentHealth;

        private float maxHealth;
        
            public void HealthBarFiller(Image healthSlider, float currentHealth, float maxHealth)
            {
                healthSlider.fillAmount = currentHealth / maxHealth;
            }
            public void takeDamage(float damage,float currentHealth)
            {
                currentHealth = currentHealth - damage;
            }
    }
}