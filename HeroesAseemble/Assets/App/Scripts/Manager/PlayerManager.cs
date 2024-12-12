using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class PlayerManager : SceneDependentSingleton<PlayerManager>
    {
        public bool isClicking;
        public GameObject playerDeath;

        public void SideMovement()
        {
            if (Input.GetMouseButton(0))
            {
                isClicking = true;
            }
            else
            {
                isClicking = false;
            }
        }

        public void DeadCheckPlayer()
        {
            if (GameManager.Instance.Player.GetComponent<DemoEnemy>().currentHp <= 0 && !GameManager.Instance.dead)
            {
                GameManager.Instance.dead = true;
                GameManager.Instance.startGame = false;
                GameManager.Instance.Player.GetComponent<Animator>().SetBool("Dead", true);
                PlayerManager.Instance.playerDeath.GetComponent<ParticleSystem>().Play();
                GameManager.Instance.EndGame();
                CancelInvoke();
            }
        }

        public void Update()
        {
            if (GameManager.Instance.startGame)
            {
                SideMovement();
            }
        }
    }
}