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

            if(enemyController.CharacterController!=null)
            {
                return;
            }

            if(other.CompareTag(GlobalInfor.CharacterTag) && !IsDead())
            {
                enemyController.SetTargetToEnemy(other.gameObject);
                enemyController.CharacterController = other.GetComponent<CharacterController>();
                enemyController.ChangeStatus(CharacterStatus.NormalAttack);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            enemyController.SetTargetToEnemy(null);
            enemyController.ChangeStatus(CharacterStatus.Idle);
        }

        public bool IsDead()
        {
            if(enemyController==null)
            {
                return false;
            }

            return enemyController.CurrentCharacterStatus.Equals(CharacterStatus.Dead);
        }

    }
}