using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class MeeleAttack : MonoBehaviour, IAttack
    {
        [SerializeField] private int damageAmount = 1;
        [SerializeField] private float attackRadius = 1f; // Radius of the fan-shaped area
        [SerializeField] private float attackAngle = 30f; // Angle of the fan-shaped area
        [SerializeField] private bool isPlayer;

        private CharacterBase enemyBase;

        private float multiDamage = 1f;

        public void InitAttack()
        {
            if(enemyBase)
            {
                int realDamage = (int)(multiDamage * damageAmount);
                enemyBase.GetDamage(realDamage);
            }
        }

        public void UpdateAttack()
        {
            
        }

        public void FinishAttack()
        {
            
        }

        public void SetEnemyController(CharacterBase characterBase)
        {
            enemyBase = characterBase;   
        }

        public GameObject DetectClosestEnemyInFanArea()
        {
            GameObject closestEnemy = null;
            float minDistance = -1;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius, GlobalInfor.Instance.CharacterLayerMask);

            bool hasCheckEnemy = false;
            foreach (var hitCollider in hitColliders)
            {
                if((isPlayer && hitCollider.CompareTag(GlobalInfor.EnemyTag)) || (!isPlayer && (hitCollider.CompareTag(GlobalInfor.CharacterTag))))
                {
                    hasCheckEnemy = true;
                }

                if(!hasCheckEnemy)
                {
                    continue;
                }

                Vector3 directionToTarget = (hitCollider.transform.position - transform.position).normalized;
                float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

                if (angleToTarget <= attackAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, hitCollider.transform.position);
                    if (distanceToTarget < minDistance)
                    {
                        minDistance = distanceToTarget;
                        closestEnemy = hitCollider.gameObject;
                    }
                }
            }

            return closestEnemy;
        }
    }
}

