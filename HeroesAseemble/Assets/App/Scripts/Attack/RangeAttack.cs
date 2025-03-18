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

        private CharacterBase enemyBase;

        private Vector3 moveDirect = Vector3.zero;

        public void InitAttack()
        {
            GameObject currentBullet = Instantiate(bulletPrefab);
            currentBullet.transform.position = spawnTransform.position;
            
            BulletController bulletController = currentBullet.GetComponent<BulletController>();
            if(bulletController != null)
            {
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
            
        }

        public void SetEnemyController(CharacterBase characterBase)
        {
            enemyBase = characterBase;
        }
    }
}

