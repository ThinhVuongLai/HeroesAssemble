using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HeroesAssemble
{
    public class Agent : MonoBehaviour
    {
        public GameObject target;
        public int agentLevel = 1;
        public bool isdead;
        public int exp;
        public int power;
        public GameObject deathParticle;
        public int targetCount;
        public Vector3 refPos;

        private GameObject _player;
        private NavMeshAgent agent;
        private Vector3 Deadposition;
        private int counter;
        private bool bDistance;
        private bool startAttack;
        private Animator animator;
        private DemoEnemy demoEnemy;

        private void OnEnable()
        {
            isdead = false;
            targetCount = 0;

            animator = GetComponent<Animator>();
            demoEnemy = GetComponent<DemoEnemy>();

            tag = "Goblin";
            agent = GetComponent<NavMeshAgent>();
            target = GameObject.FindGameObjectWithTag("Player");
            _player = target;

            animator.Rebind();
            animator.Update(0f);

            Invoke("StartAttackGo", 1.25f);

            if (counter > 1)
            {
                demoEnemy.openHealtbar();

            }

            counter++;
            if (agentLevel == 1)
            {

                if (GameManager.Instance.maxGreenGoblin > GameManager.Instance.getGreenGoblin)
                {
                    GameManager.Instance.getGreenGoblin++;

                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else if (agentLevel == 2)
            {
                if (GameManager.Instance.maxBlueGoblin > GameManager.Instance.getBlueGoblin)
                {
                    GameManager.Instance.getBlueGoblin++;
                }
                else
                {
                    gameObject.SetActive(false);
                }


            }
            else if (agentLevel == 3)
            {
                if (GameManager.Instance.maxLevel3Goblin > GameManager.Instance.getLevel3Goblin)
                {
                    GameManager.Instance.getLevel3Goblin++;

                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }

        private void StartAttackGo()
        {
            startAttack = true;
        }

        private void DownItem(int luckyCount, bool direckDown)
        {
            int luckyHpOrPokeball = Random.Range(0, 20);
            int lucky = Random.Range(0, luckyCount - GameManager.Instance.itemDownLucky);

            if (luckyHpOrPokeball == 3)
            {

                GameObject obj = Instantiate(GameManager.Instance.healthItem, Deadposition, Quaternion.identity);
                obj.transform.position = new Vector3(obj.transform.position.x, 2.34f, obj.transform.position.z);
            }
            else
            {
                if (lucky == 3 || direckDown)
                {
                    GameObject obj = Instantiate(GameManager.Instance.pokemonItem, Deadposition, Quaternion.identity);
                    obj.transform.position = new Vector3(obj.transform.position.x, 0.37f, obj.transform.position.z);
                }
            }
        }

        private void InActiveObj()
        {
            demoEnemy.hideHealtbar();

            if (agentLevel == 1)
            {
                DownItem(40, false);
            }
            else if (agentLevel == 2)
            {
                DownItem(30, false);
            }
            else if (agentLevel == 3)
            {
                DownItem(40, true);
            }

            Invoke("NotActive", .25f);
        }

        private void NotActive()
        {
            transform.position = refPos;
            gameObject.SetActive(false);

        }

        private void Update()
        {
            if (target != null && !isdead)
            {
                agent.SetDestination(target.transform.position);

                if (agent.remainingDistance > 0.25f && startAttack)
                {
                    bDistance = agent.remainingDistance > agent.stoppingDistance;

                    if (!bDistance)
                    {
                        animator.SetBool("Attack", true);
                    }
                    else
                    {
                        animator.SetBool("Attack", false);
                    }
                }
            }
        }
    }
}