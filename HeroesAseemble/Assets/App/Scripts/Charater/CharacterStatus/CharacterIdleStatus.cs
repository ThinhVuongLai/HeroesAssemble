using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CharacterIdleStatus : BaseCharacterStatus
    {
        public override void BeginStatus()
        {
            base.BeginStatus();

            CurrentCharacterController.PlayAnimation(CurrentCharacterController.CurrentCharacterInfor.idleAnimationName);

            CurrentCharacterController.FriendlyAgent.StopNavMeshAgent();
        }

        public override void UpdateStatus()
        {
            base.UpdateStatus();

            if (!CurrentCharacterController.FriendlyAgent.IsEnoughDistanceToTarget())
            {
                CurrentCharacterController.ChangeStatus(CharacterStatus.Run);
            }
        }
    }
}
