using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CharacterController : MonoBehaviour
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
        [SerializeField] private ParticleSystem evelotionParticle;
        [SerializeField] private ParticleSystem evelotionStartParticle;

        private bool stopExp;

        private CharacterInfor characterInfor;
        private FriendlyAgent friendlyAgent;
        private Animator characterAnimator;
        private CharacterTrigger characterTrigger;

        private void Awake()
        {
            friendlyAgent = GetComponent<FriendlyAgent>();
            characterAnimator = GetComponent<Animator>();
            characterTrigger = GetComponent<CharacterTrigger>();

            SetShowEvolutionParticle(false);
            SetShowEvolutionStartParticle(false);
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
                    if (friendlyAgent.Target.tag == "FollowCube")
                    {
                        friendlyAgent.Target.GetComponent<State>().isFill = false;
                    }

                    GetComponent<DemoEnemy>().hideHealtbar();
                    transform.tag = "Respawn";

                    characterTrigger.enabled = false;

                    friendlyAgent.IsStartGame = false;

                    characterAnimator.SetBool("Attack", false);
                    characterAnimator.SetBool("Run", false);
                    characterAnimator.SetBool("Idle", true);

                    if (evelotionStartParticle != null)
                    {
                        evelotionStartParticle.Play();
                    }
                    else
                    {
                        Debug.LogWarning(name + ": isimli pokemonda evolotion particle yok");
                    }

                    if (friendlyAgent.Target != null)
                    {
                        if (friendlyAgent.Target.GetComponent<Agent>().target.tag == "Pokemon")
                        {
                            friendlyAgent.Target.GetComponent<Agent>().target = null;
                        }
                    }

                    //  GetComponent<FriendlyAgent>().enabled = false;

                    Invoke("LevelUp", 1f);
                    stopExp = true;
                }
            }
        }

        public void SetShowEvolutionParticle(bool show)
        {
            if (evelotionParticle != null)
            {
                evelotionParticle.gameObject.SetActive(show);

                if (show)
                {
                    evelotionParticle.Play();
                }
                else
                {
                    evelotionParticle.Stop();
                }
            }
        }

        public void SetShowEvolutionStartParticle(bool show)
        {
            if (evelotionStartParticle != null)
            {
                evelotionStartParticle.gameObject.SetActive(show);

                if (show)
                {
                    evelotionStartParticle.Play();
                }
                else
                {
                    evelotionStartParticle.Stop();
                }
            }
        }
    }
}