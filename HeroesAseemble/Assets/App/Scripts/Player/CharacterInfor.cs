using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CharacterInfor : MonoBehaviour
    {
        [Header("Info Settings")]
        public string Name;
        public int power;
        public int maxHealth;
        public int level;
        public int maxLevel;
        public GameObject evelotionParent;
        [Header("Live Settings")]
        public int health = 0;
        public int exp = 0;
        public int levelExpLimit = 10;
        public bool dead;
        public bool target;
        public bool attack;
        [Header("Another Settings")]
        public GameObject evelotionParticle;
        public GameObject evelotionStartParticle;

        private bool stopExp;

        private CharacterInfor characterInfor;
        private FriendlyAgent friendlyAgent;
        private Animator characterAnimator;
        private CharacterTrigger characterTrigger;

        private void Awake()
        {
            characterInfor = GetComponent<CharacterInfor>();
            friendlyAgent = GetComponent<FriendlyAgent>();
            characterAnimator = GetComponent<Animator>();
            characterTrigger = GetComponent<CharacterTrigger>();
        }

        private void LevelUp()
        {
            Instantiate(evelotionParent, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }

        public void LevelCheck(int addExp)
        {
            Debug.Log("Exp Eklendi++");
            exp += addExp;
            if (exp >= levelExpLimit && !stopExp)
            {
                Debug.Log(Name + " adl� pokemon level atladi");

                if (level < maxLevel)
                {
                    characterInfor.level++;
                    if (friendlyAgent.target.tag == "FollowCube")
                    {
                        friendlyAgent.target.GetComponent<State>().isFill = false;
                    }

                    GetComponent<DemoEnemy>().hideHealtbar();
                    transform.tag = "Respawn";

                    characterTrigger.enabled = false;

                    friendlyAgent.startGame = false;

                    characterAnimator.SetBool("Attack", false);
                    characterAnimator.SetBool("Run", false);
                    characterAnimator.SetBool("Idle", true);

                    if (evelotionStartParticle != null)
                    {
                        evelotionStartParticle.GetComponent<ParticleSystem>().Play();
                    }
                    else
                    {
                        Debug.LogWarning(name + ": isimli pokemonda evolotion particle yok");
                    }

                    if (friendlyAgent.target != null)
                    {
                        if (friendlyAgent.target.GetComponent<Agent>().target.tag == "Pokemon")
                        {
                            friendlyAgent.target.GetComponent<Agent>().target = null;
                        }
                    }

                    //  GetComponent<FriendlyAgent>().enabled = false;

                    Invoke("LevelUp", 1f);
                    stopExp = true;
                }
            }
        }
    }
}