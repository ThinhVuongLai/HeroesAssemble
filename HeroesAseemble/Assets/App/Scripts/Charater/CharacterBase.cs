using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CharacterBase : MonoBehaviour
    {
        protected CharacterInfor characterInfor;
        protected CharacterStatus currentCharacterStatus = CharacterStatus.None;
        protected FriendlyAgent friendlyAgent;
        protected Animator characterAnimator;

        protected BaseCharacterStatus currentStatus;
        protected BaseCharacterStatus idleStatus;
        protected BaseCharacterStatus runStatus;
        protected BaseCharacterStatus normalAttackStatus;
        protected BaseCharacterStatus deadStatus;

        [SerializeField] private IAttack attackInterface;
        [SerializeField] private Collider characterCollider;

        private bool hasTargetEnemy;
        private int currentHeath;

        public CharacterInfor CurrentCharacterInfor
        {
            get
            {
                return characterInfor;
            }
        }

        public CharacterStatus CurrentCharacterStatus
        {
            get
            {
                return currentCharacterStatus;
            }
        }

        public FriendlyAgent FriendlyAgent
        {
            get
            {
                return friendlyAgent;
            }
        }

        public int Heath
        {
            get
            {
                return characterInfor.heath;
            }
        }

        public IAttack AttackInterface
        {
            get
            {
                return attackInterface;
            }
        }

        public bool HasTargetEnemy
        {
            get => hasTargetEnemy;
            set
            {
                hasTargetEnemy = value;
            }
        }

        protected virtual void Awake()
        {
            friendlyAgent = GetComponent<FriendlyAgent>();
            characterAnimator = GetComponent<Animator>();
            attackInterface = GetComponent<IAttack>();

            characterCollider = GetComponent<Collider>();
        }

        public virtual void Init(int characterId)
        {
            characterInfor = EventController.Instance.GetCharacterInforChannel.RunChannel(characterId);

            currentHeath = characterInfor.heath;
        }

        public void PlayAnimation(string animationName, bool isForcePlay = false)
        {
            if (characterAnimator && !string.IsNullOrEmpty(animationName))
            {
                if (isForcePlay)
                {
                    characterAnimator.Play(animationName, 0, 0);
                }
                else
                {
                    characterAnimator.CrossFade(animationName, 0.1f, 0, 0);
                }
            }
        }

        public bool IsDead()
        {
            return currentCharacterStatus.Equals(CharacterStatus.Dead);
        }

        public void ChangeStatus(CharacterStatus characterStatus)
        {
            if (currentCharacterStatus.Equals(characterStatus))
            {
                return;
            }

            if (currentStatus != null)
            {
                currentStatus.EndStatus();
            }

            currentCharacterStatus = characterStatus;

            switch (characterStatus)
            {
                case CharacterStatus.Idle:
                    {
                        currentStatus = idleStatus;
                    }
                    break;
                case CharacterStatus.Run:
                    {
                        currentStatus = runStatus;
                    }
                    break;
                case CharacterStatus.NormalAttack:
                    {
                        currentStatus = normalAttackStatus;
                    }
                    break;
                case CharacterStatus.Dead:
                    {
                        currentStatus = deadStatus;
                    }
                    break;
                default:
                    break;
            }

            if (currentStatus != null)
            {
                currentStatus.BeginStatus();
            }
        }

        private void Update()
        {
            if (currentStatus != null)
            {
                currentStatus.UpdateStatus();
            }
        }

        public virtual void GetDamage(int damageAmount)
        {
            if(damageAmount<=0)
            {
                return;
            }

            currentHeath-=damageAmount;

            if(currentHeath <= 0)
            {
                ChangeStatus(CharacterStatus.Dead);
            }
        }

        public virtual void AddHeath(int addAmount)
        {
            if(addAmount>0)
            {
                currentHeath+=addAmount;
            }
        }

        public virtual void RunInitAttack()
        {
            AttackInterface?.InitAttack();
        }

        public virtual void UpdateAttack()
        {
            AttackInterface?.UpdateAttack();
        }

        public virtual void FinishAttack()
        {
            AttackInterface?.FinishAttack();
        }

        public void SetTargetToEnemy(GameObject enemyObject)
        {
            friendlyAgent.Target = enemyObject;
            hasTargetEnemy = true;
        }

        public void RemoveTarget()
        {
            friendlyAgent.Target = null;
            hasTargetEnemy = false;
        }

        public void SetTargetToMovePoint()
        {
            friendlyAgent.SetTargetToTargetMove();
            hasTargetEnemy = false;
        }

        public virtual void EnemyDeadAction()
        {

        }

        public void CheckEnemy()
        {
            characterCollider.enabled = false;

            StartCoroutine(CRCheckEnemy());
        }

        private IEnumerator CRCheckEnemy()
        {
            yield return null;

            characterCollider.enabled = true;
        }
    }

    public enum CharacterStatus
    {
        None = -1,
        Idle,
        Run,
        NormalAttack,
        Dead,
    }
}