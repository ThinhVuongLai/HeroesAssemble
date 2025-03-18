using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public interface IAttack
    {
        public void InitAttack();

        public void UpdateAttack();

        public void FinishAttack();

        public void SetEnemyController(CharacterBase characterBase);
    }
}