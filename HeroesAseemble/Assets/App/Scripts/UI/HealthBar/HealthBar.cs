using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeroesAssemble
{
    public class HealthBar : MonoBehaviour, IHealthBar
    {
        [SerializeField] private Image healthImage;

        private RectTransform rectTransform;
        private Transform characterTransform;
        private int maxHealth;
        private int currentHealth;
        private bool hasShow;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void AddHealth(int addAmount)
        {
            currentHealth += addAmount;
            if(currentHealth > maxHealth)
            {
                maxHealth = currentHealth;
            }

            UpdateHealth(currentHealth);
        }

        public void Init(int maxHealth)
        {
            hasShow = true;

            this.maxHealth = maxHealth;
            currentHealth = maxHealth;

            UpdateHealth(maxHealth);
        }

        public void SetCharacterTransform(Transform characterTransform)
        {
            this.characterTransform = characterTransform;
        }

        public void MinusHealth(int minusAmount)
        {
            currentHealth -= minusAmount;
            if(currentHealth <= 0)
            {
                currentHealth = 0;
            }

            UpdateHealth(currentHealth);
        }

        public void UpdateHealth(int healthAmount)
        {
            currentHealth = Mathf.Max(0, healthAmount);

            if (healthImage)
            {
                healthImage.fillAmount = Mathf.Clamp01((float)currentHealth / maxHealth);
            }

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        public void ResetHealth()
        {
            currentHealth = maxHealth;
            if(healthImage)
            {
                healthImage.fillAmount = 1;
            }
        }

        private void Update()
        {
            if(!hasShow)
            {
                return;
            }

            rectTransform.position = Camera.main.WorldToScreenPoint(characterTransform.position) + new Vector3(0, 100f);
        }
    }
}
