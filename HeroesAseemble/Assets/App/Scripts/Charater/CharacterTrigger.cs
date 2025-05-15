
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CharacterTrigger : MonoBehaviour
    {
        private CharacterController characterController;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void OnTriggerEnter(Collider other)
        {            
            if(characterController == null)
            {
                return;
            }

            if(!characterController.CurrentCharacterStatus.Equals(CharacterStatus.NormalAttack))
            {
                return;
            }

            if (other.CompareTag(GlobalInfor.CharacterTag) && !IsDead()
                && characterController.CurrentEnemy == null)
            {

            }
            else if (other.CompareTag(GlobalInfor.EnemyTag) && !IsDead())
            {
                characterController.SetTargetToEnemy(other.gameObject);
                characterController.CurrentEnemy = other.GetComponent<EnemyController>();
            }
        }

        public bool IsDead()
        {
            return characterController.CurrentCharacterStatus.Equals(CharacterStatus.Dead);
        }
    }
}