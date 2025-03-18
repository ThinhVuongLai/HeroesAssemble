using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class PlayerTrigger : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag(GlobalInfor.EnemyTag))
            {
                EventController.Instance.AddEnemyTransform.RunTransformChannel(other.transform);

                PlayerStatus currentPlayerStatus = EventController.Instance.GetPlayerStatusChannel.RunGetPlayerStatusChannel();

                if(currentPlayerStatus.Equals(PlayerStatus.Idle))
                {
                    EventController.Instance.SetEnemyForCharacter.RunVoidChannel();
                }
            }
        }


        public void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag(GlobalInfor.EnemyTag))
            {
                EventController.Instance.RemoveEnemyTransform.RunTransformChannel(other.transform);
            }
        }
    }
}