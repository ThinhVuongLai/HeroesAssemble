using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace HeroesAssemble
{
    public class FriendlyAgent : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private bool startGame;

        private CharacterController characterController;
        private Animator characterAnimator;
        private NavMeshAgent navMeshAgent;

        public GameObject Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }

        public bool IsStartGame
        {
            get
            {
                return startGame;
            }
            set
            {
                startGame = value;
            }
        }

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            characterAnimator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            CheckPosition();
            //characterController.SetShowEvolutionParticle(true);

            Invoke("GoGame", 1.1f);
        }

        private void GoGame()
        {
            startGame = true;
        }

        public void CheckPosition()
        {
            for (int i = 0; i < CharacterList.Instance.pokemonListArray.Count; i++)
            {
                if (CharacterList.Instance.pokemonListArray[i].GetComponent<State>().isFill == false)
                {
                    CharacterList.Instance.pokemonListArray[i].GetComponent<State>().isFill = true;
                    target = CharacterList.Instance.pokemonListArray[i];

                    break;
                }
            }
        }

        public void DamageCheck()
        {
            Debug.Log("Event attack check edildi pokemon");
            if (target != null && transform.tag == "Pokemon" && !characterController.dead)
            {

                if (target.tag == "Goblin" && !target.GetComponent<Agent>().isdead)
                {
                    if (target.GetComponent<Agent>().target.tag != "Pokemon")
                    {
                        target.GetComponent<Agent>().target = gameObject;
                    }
                }

                bool bDistance = navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance + 1f;
                if (!bDistance && target.CompareTag("Goblin") && !target.GetComponent<Agent>().isdead)
                {
                    Debug.Log("goblin cani indirioz");
                    target.GetComponent<DemoEnemy>().currentHp -= (characterController.power * characterController.level);
                    if (target.GetComponent<DemoEnemy>().currentHp <= 0 && !target.GetComponent<Agent>().isdead)
                    {
                        target.GetComponent<DemoEnemy>().currentHp = 0;
                        target.GetComponent<DemoEnemy>().hideHealtbar();
                        if (!target.GetComponent<Agent>().isdead)
                        {
                            characterController.LevelCheck(target.GetComponent<Agent>().exp);
                            GameManager.Instance.deadGoblinCounter++;
                        }
                        characterController.attack = false;
                        target.GetComponent<Agent>().Dead();
                    }
                    else if (target.GetComponent<DemoEnemy>().currentHp <= 0 && target.GetComponent<Agent>().isdead)
                    {
                        characterController.attack = false;
                        CheckPosition();
                    }
                }

            }

            if (characterController.dead)
            {
                characterController.attack = false;
            }

            if (target != null && transform.tag == "Pokemon" && target.tag == "Goblin")
            {
                if (target.GetComponent<Agent>().isdead)
                {
                    characterController.attack = false;
                    CheckPosition();
                }
            }
        }

        public void Dead()
        {
            characterAnimator.SetBool("Dead", true);
            if (target.tag != "Goblin")
            {
                target.GetComponent<State>().isFill = false;
            }
            GetComponent<DemoEnemy>().hideHealtbar();
            characterController.dead = true;
            Invoke("InActiveObj", 1f);
            GameManager.Instance.getPokemon--;

        }

        private void InActiveObj()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {

            if (target != null && startGame)
            {
                navMeshAgent.SetDestination(target.transform.position);

                bool bDistance = navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance + 1;
                if (!bDistance && !characterController.attack)
                {
                    navMeshAgent.isStopped = true;

                    characterAnimator.SetBool("Run", false);
                    characterAnimator.SetBool("Idle", true);

                }
                else if (bDistance && !characterController.attack)
                {
                    navMeshAgent.isStopped = false;

                    characterAnimator.SetBool("Run", true);
                    characterAnimator.SetBool("Attack", false);
                    characterAnimator.SetBool("Idle", false);
                }

                if (!bDistance && characterController.attack)
                {
                    navMeshAgent.isStopped = true;

                    characterAnimator.SetBool("Run", false);
                    characterAnimator.SetBool("Idle", true);
                    characterAnimator.SetBool("Attack", true);
                }

                if (bDistance && characterController.attack)
                {
                    navMeshAgent.isStopped = false;

                    characterAnimator.SetBool("Run", true);
                    characterAnimator.SetBool("Attack", false);
                    characterAnimator.SetBool("Idle", false);
                }

            }
        }
    }
}