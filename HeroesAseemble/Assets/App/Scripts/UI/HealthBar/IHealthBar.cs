using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public interface IHealthBar
    {
        public void Init(int maxHealth);

        public void AddHealth(int addAmount);

        public void MinusHealth(int minusAmount);

        public void UpdateHealth(int healthAmount);

        public void ResetHealth();

        public void SetShowBar(bool isShow);
    }
}