using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class RangeAttack : MonoBehaviour, IAttack
    {
        [SerializeField] private float damageAmount;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform spawnTransform;

        private CharacterBase characterBase;
        private CharacterBase enemyBase;

        private Vector3 moveDirect = Vector3.zero;

        public void InitAttack()
        {
            GameObject currentBullet = Instantiate(bulletPrefab);
            currentBullet.transform.position = spawnTransform.position;
            
            BulletController bulletController = currentBullet.GetComponent<BulletController>();
            if(bulletController != null)
            {
                bool isCharacter = enemyBase is CharacterController;
                if(isCharacter)
                {
                    bulletController.Init(BulletSpawnFrom.Player);
                }
                else
                {
                    bulletController.Init(BulletSpawnFrom.Enemy);
                }

                SetMoveDirect(enemyBase.transform.position);
                bulletController.SetMoveDirect(moveDirect);
            }
        }

        public void SetMoveDirect(Vector3 targetPosition)
        {
            targetPosition.y = spawnTransform.position.y;

            Vector3 direct = (targetPosition - spawnTransform.position).normalized;

            moveDirect = direct;
        }

        public void UpdateAttack()
        {
            
        }

        public void FinishAttack()
        {
            if (enemyBase == null)
            {
                return;
            }

            if (enemyBase.IsDead())
            {
                EventController.Instance.SetEnemyForCharacter.RunVoidChannel();
                characterBase.SetEnemy(null);
                characterBase.HasTargetEnemy = false;
            }
        }

        public void SetEnemyController(CharacterBase characterBase)
        {
            enemyBase = characterBase;
        }

        public void SetCharacterController(CharacterBase characterBase)
        {
            this.characterBase = characterBase;
        }
    }
}

