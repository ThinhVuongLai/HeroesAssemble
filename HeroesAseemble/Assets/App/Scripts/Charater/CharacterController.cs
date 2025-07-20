using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CharacterTrigger))]
    [RequireComponent(typeof(FriendlyAgent))]
    [RequireComponent(typeof(AnimationEventController))]
    public class CharacterController : CharacterBase
    {
        protected CharacterTrigger characterTrigger;

        [SerializeField] private EnemyController currentEnemy;

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
                AttackInterface.SetCharacterController(this);

                base.RunInitAttack();
            }
        }

        protected override void DeadAction()
        {
            base.DeadAction();

            if(characterTrigger)
            {
                characterTrigger.enabled = false;
            }

            CurrentEnemy = null;
            RemoveTarget();
        }

        public override void Reborn()
        {
            base.Reborn();

            if (characterTrigger)
            {
                characterTrigger.enabled = true;
            }

            EventController.Instance.SetEnemyForCharacter.RunVoidChannel();
        }

        public override void SetEnemy(GameObject enemyObject)
        {
            base.SetEnemy(enemyObject);

            if (enemyObject == null)
            {
                CurrentEnemy = null;

                HasTargetEnemy = false;

                SetTargetToMovePoint();
                ChangeStatus(CharacterStatus.Run);
            }
            else
            {
                EnemyController enemyController = enemyObject.GetComponent<EnemyController>();
                CurrentEnemy = enemyController;

                HasTargetEnemy = true;

                SetTargetToEnemy(enemyObject);
                ChangeStatus(CharacterStatus.NormalAttack);
            }
        }
    }
}