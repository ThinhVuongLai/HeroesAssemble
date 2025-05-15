using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CharacterDeadStatus : BaseCharacterStatus
    {
        public override void BeginStatus()
        {
            base.BeginStatus();

            CurrentCharacterController.PlayAnimation(CurrentCharacterController.CurrentCharacterInfor.deadAnimattionName);
            CurrentCharacterController.FriendlyAgent.StopNavMeshAgent();
        }
    }
}
