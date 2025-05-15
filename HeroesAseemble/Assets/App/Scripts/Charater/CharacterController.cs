using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CharacterController : CharacterBase
    {
        protected CharacterTrigger characterTrigger;
        private EnemyController currentEnemy;

        private GameObject assembleTarget;

        public CharacterTrigger CharacterTrigger
        {
            get
            {
                return characterTrigger;
            }
        }

        public EnemyController CurrentEnemy
        {
            get
            {
                return currentEnemy;
            }
            set
            {
                currentEnemy = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            characterTrigger = GetComponent<CharacterTrigger>();
        }

        private void SetAssembleTarget()
        {
            var characterSlotStatuses = PlayerCharacterList.Instance.CharacterSlotStatuses;

            for (int i = 0, length = characterSlotStatuses.Count; i < length; i++)
            {
                if (characterSlotStatuses[i].IsFullSlot.Equals(false))
                {
                    characterSlotStatuses[i].IsFullSlot = true;
                    assembleTarget = PlayerCharacterList.Instance.pokemonListArray[i];

                    break;
                }
            }
        }

        public override void Init(int characterId)
        {
            base.Init(characterId);

            idleStatus = new CharacterIdleStatus();
            runStatus = new CharacterRunStatus();
            normalAttackStatus = new CharacterNormalAttackStatus();
            deadStatus = new CharacterDeadStatus();

            idleStatus.SetCharacterController(this);
            runStatus.SetCharacterController(this);
            normalAttackStatus.SetCharacterController(this);
            deadStatus.SetCharacterController(this);

            SetAssembleTarget();
            friendlyAgent.SetTargetToMove(assembleTarget);
            SetTargetToMovePoint();

            ChangeStatus(CharacterStatus.Idle);
        }

        public override void RunInitAttack()
        {
            if(currentEnemy == null || AttackInterface == null)
            {
                return;
            }
            else
            {
                AttackInterface.SetEnemyController(currentEnemy);

                base.RunInitAttack();
            }
        }
    }
}