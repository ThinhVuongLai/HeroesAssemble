
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

            if(characterController.IsDead() || characterController.CurrentEnemy != null)
            {
                return;
            }

            if (other.CompareTag(GlobalInfor.EnemyTag))
            {
                characterController.SetEnemy(other.gameObject);
            }
        }
    }
}