using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class EnemyTrigger : MonoBehaviour
    {
        private EnemyController enemyController;

        private void Awake()
        {
            enemyController = GetComponent<EnemyController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(enemyController==null)
            {
                return;
            }

            if(enemyController.IsDead())
            {
                return;
            }

            if(enemyController.CharacterController!=null)
            {
                return;
            }

            if(other.CompareTag(GlobalInfor.CharacterTag) && !IsDead())
            {
                enemyController.SetEnemy(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.Equals(enemyController.CharacterController) && !enemyController.CharacterController.IsAttack())
            {
                enemyController.SetEnemy(null);
            }
        }

        public bool IsDead()
        {
            if(enemyController==null)
            {
                return false;
            }

            return enemyController.IsDead();
        }

    }
}