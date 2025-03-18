using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class EnemyController : CharacterBase
    {
        [SerializeField] private int characterId;

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
    }
}
