using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class PlayerManager : SceneDependentSingleton<PlayerManager>
    {
        public bool isClicking;
        public GameObject playerDeath;

        [Header("Channel")]
        [SerializeField] private VoidChannel updateAssemplyChannel;
        [SerializeField] private IntegerChannel addCharacterChannel;
        [SerializeField] private IntegerChannel removeCharacterChannel;

        public override void Awake()
        {
            base.Awake();

            if(playerDeath)
            {
                playerDeath.SetActive(false);
            }
        }

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

            if(Input.GetKeyDown(KeyCode.M))
            {
                //addCharacterChannel.RunIntegerChannel(0);
                //addCharacterChannel.RunIntegerChannel(1);
                //addCharacterChannel.RunIntegerChannel(2);
                //addCharacterChannel.RunIntegerChannel(3);
                addCharacterChannel.RunIntegerChannel(4);
                addCharacterChannel.RunIntegerChannel(5);

                updateAssemplyChannel.RunVoidChannel();
            }
        }
    }
}