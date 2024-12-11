using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CharacterTrigger : MonoBehaviour
    {
        private CharacterInfor characterInfor;
        private FriendlyAgent friendlyAgent;

        private void Awake()
        {
            characterInfor = GetComponent<CharacterInfor>();
            friendlyAgent = GetComponent<FriendlyAgent>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Goblin" && characterInfor.attack == false && GetComponent<FriendlyAgent>().target != null && other.GetComponent<Agent>().targetCount <= 2)
            {
                characterInfor.attack = true;

                if (friendlyAgent.target.tag == "FollowCube")
                {
                    friendlyAgent.target.GetComponent<State>().isFill = false;
                }

                friendlyAgent.target = other.gameObject;
                other.GetComponent<Agent>().target = gameObject;
                other.GetComponent<Agent>().targetCount++;
            }
        }
    }
}