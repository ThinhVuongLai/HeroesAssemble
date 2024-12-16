using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CharacterTrigger : MonoBehaviour
    {
        private CharacterController characterController;
        private FriendlyAgent friendlyAgent;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            friendlyAgent = GetComponent<FriendlyAgent>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Goblin" && characterController.attack == false && GetComponent<FriendlyAgent>().Target != null && other.GetComponent<Agent>().targetCount <= 2)
            {
                characterController.attack = true;

                if (friendlyAgent.Target.tag == "FollowCube")
                {
                    friendlyAgent.Target.GetComponent<State>().isFill = false;
                }

                friendlyAgent.Target = other.gameObject;
                other.GetComponent<Agent>().target = gameObject;
                other.GetComponent<Agent>().targetCount++;
            }
        }
    }
}