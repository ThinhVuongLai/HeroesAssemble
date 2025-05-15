using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f; // Speed of the bullet
        [SerializeField] private GameObject impactEffectPrefab;
        [SerializeField] private int damageAmount = 1;

        private Vector3 moveDirect = Vector3.one;

        private BulletSpawnFrom bulletSpawnFrom;
        private float multiDamage = 1;

        public void SetMoveDirect(Vector3 setDirect)
        {
            moveDirect = setDirect;
        }

        public void Init(BulletSpawnFrom bulletSpawnFrom)
        {
            this.bulletSpawnFrom = bulletSpawnFrom;
        }

        private void Update()
        {
            MoveBullet();
        }

        private void MoveBullet()
        {
            if(GameManager.Instance.IsPauseGame)
            {
                return;
            }

            transform.Translate(moveDirect * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            bool hasFinish = true;

            if(other.gameObject.CompareTag(GlobalInfor.EnemyTag))
            {
                if(bulletSpawnFrom.Equals(BulletSpawnFrom.Enemy))
                {
                    hasFinish = false;
                }
                else
                {
                    EnemyController enemyController=other.gameObject.GetComponent<EnemyController>();

                    if(enemyController!=null)
                    {
                        int realDamage = (int)(multiDamage * damageAmount);
                        enemyController.GetDamage(realDamage);
                    }
                }
            }
            else if(other.gameObject.CompareTag(GlobalInfor.CharacterTag))
            {
                if(bulletSpawnFrom.Equals(BulletSpawnFrom.Player))
                {
                    hasFinish = false;
                }
                else
                {
                    CharacterController characterController = other.gameObject.GetComponent<CharacterController>();

                    if(characterController != null)
                    {
                        int realDamage = (int)(multiDamage * damageAmount);
                        characterController.GetDamage(realDamage);
                    }
                }
            }

            if(hasFinish)
            {
                Vector3 hitPoint = GetHitPoint(other);
                GameObject impactObject = Instantiate(impactEffectPrefab, hitPoint, Quaternion.identity);

                Destroy(gameObject, 0.1f);
            }
        }

        private Vector3 GetHitPoint(Collider other)
        {
            return other.ClosestPoint(transform.position);
        }
    }

    public enum BulletSpawnFrom
    {
        Player,
        Enemy,
    }
}