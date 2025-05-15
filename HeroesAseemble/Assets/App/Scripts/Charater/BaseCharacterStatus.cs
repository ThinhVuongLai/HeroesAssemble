using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public abstract class BaseCharacterStatus
    {
        [SerializeField] private CharacterBase characterController;

        public CharacterBase CurrentCharacterController
        {
            get
            {
                return characterController;
            }
        }

        public void SetCharacterController(CharacterBase characterController)
        {
            this.characterController = characterController;
        }

        public virtual void BeginStatus() { }

        public virtual void UpdateStatus() { }

        public virtual void EndStatus() { }
    }
}