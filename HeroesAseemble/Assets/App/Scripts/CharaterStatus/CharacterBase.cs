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

        private int currentHeath;

        protected virtual void Awake()
        {
            friendlyAgent = GetComponent<FriendlyAgent>();
            characterAnimator = GetComponent<Animator>();
            attackInterface = GetComponent<IAttack>();
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
            if(damageAmount<0)
            {
                return;
            }

            currentHeath-=damageAmount;

            if(currentHeath <= 0)
            {

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