using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class EnemyController : CharacterBase
    {
        [SerializeField] private int characterId;

        private CharacterController characterController;

        public CharacterController CharacterController
        {
            get => characterController;
            set
            {
                characterController = value;
            }
        }

        private void Start()
        {
            StartCoroutine(CRInitCharacter());
        }

        private IEnumerator CRInitCharacter()
        {
            yield return null;

            if (characterInfor == null)
            {
                Init(characterId);
            }
        }

        public override void Init(int characterId)
        {
            base.Init(characterId);

            idleStatus = new EnemyIdleStatus();
            runStatus = new EnemyRunStatus();
            normalAttackStatus = new EnemyNormalAttackStatus();
            deadStatus = new EnemyDeadStatus();

            idleStatus.SetCharacterController(this);
            runStatus.SetCharacterController(this);
            normalAttackStatus.SetCharacterController(this);
            deadStatus.SetCharacterController(this);

            ChangeStatus(CharacterStatus.Idle);
        }

        public override void RunInitAttack()
        {
            if (characterController == null || AttackInterface == null)
            {
                return;
            }
            else
            {
                AttackInterface.SetEnemyController(characterController);
                AttackInterface.SetCharacterController(this);

                base.RunInitAttack();
            }
        }

        public override void SetEnemy(GameObject enemyObject)
        {
            base.SetEnemy(enemyObject);

            if (enemyObject == null)
            {
                RemoveTarget();
                CharacterController = null;
                ChangeStatus(CharacterStatus.Idle);
            }
            else
            {
                SetTargetToEnemy(enemyObject);
                CharacterController = enemyObject.GetComponent<CharacterController>();
                ChangeStatus(CharacterStatus.NormalAttack);
            }
        }
    }
}
