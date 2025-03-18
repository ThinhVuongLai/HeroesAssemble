using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CharacterNormalAttackStatus : BaseCharacterStatus
    {
        protected bool hasBeginAttackAnimation;

        public override void BeginStatus()
        {
            base.BeginStatus();

            hasBeginAttackAnimation = false;

            CheckToBeginAttack();
        }

        public override void UpdateStatus()
        {
            base.UpdateStatus();

            CheckToBeginAttack();
            
            CurrentCharacterController.UpdateAttack();

            if(hasBeginAttackAnimation)
            {
                CurrentCharacterController.FriendlyAgent.LookAtTarget();
            }
        }

        public void CheckToBeginAttack()
        {
            if(CurrentCharacterController.FriendlyAgent.IsEnoughDistanceToAttack())
            {
                // if(CurrentCharacterController.FriendlyAgent.IsTooNearTarget())
                // {
                //     MoveWhenTooNearTarget();
                //     return;
                // }

                if (!hasBeginAttackAnimation)
                {
                    hasBeginAttackAnimation = true;

                    CurrentCharacterController.FriendlyAgent.StopNavMeshAgent();

                    CurrentCharacterController.PlayAnimation(CurrentCharacterController.CurrentCharacterInfor.normalAttackAnimationName);
                }
            }
            else
            {
                CurrentCharacterController.FriendlyAgent.ContinueNavMeshAgent();
                CurrentCharacterController.FriendlyAgent.RunToTarget();

                hasBeginAttackAnimation = false;
                CurrentCharacterController.PlayAnimation(CurrentCharacterController.CurrentCharacterInfor.walkAnimationName);
            }
        }

        private void MoveWhenTooNearTarget()
        {
            CurrentCharacterController.FriendlyAgent.ContinueNavMeshAgent();
            CurrentCharacterController.FriendlyAgent.MoveWhenTooNearTarget();

            hasBeginAttackAnimation = false;
            CurrentCharacterController.PlayAnimation(CurrentCharacterController.CurrentCharacterInfor.walkAnimationName);
        }

        
    }
}