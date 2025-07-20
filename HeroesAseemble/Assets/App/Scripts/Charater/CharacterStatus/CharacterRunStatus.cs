using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CharacterRunStatus : BaseCharacterStatus
    {
        public override void BeginStatus()
        {
            base.BeginStatus();

            CurrentCharacterController.PlayAnimation(CurrentCharacterController.CurrentCharacterInfor.walkAnimationName);

            CurrentCharacterController.FriendlyAgent.ContinueNavMeshAgent();
        }

        public override void UpdateStatus()
        {
            base.UpdateStatus();

            if (CurrentCharacterController.FriendlyAgent.IsEnoughDistanceToTarget(0.05f))
            {
                CurrentCharacterController.ChangeStatus(CharacterStatus.Idle);
            }
            else
            {
                CurrentCharacterController.FriendlyAgent.RunToTarget();
            }
        }
    }
}
