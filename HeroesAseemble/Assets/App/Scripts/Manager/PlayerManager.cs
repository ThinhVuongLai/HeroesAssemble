using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class PlayerManager : SceneDependentSingleton<PlayerManager>
    {
        public bool isClicking;
        public GameObject playerDeath;

        private List<int> characterIds = new List<int>() { 2, 3, 4, 5, 6 };

        public override void Awake()
        {
            base.Awake();

            if (playerDeath)
            {
                playerDeath.SetActive(false);
            }
        }

        private void Start()
        {
            StartCoroutine(CRSpawnCharacter());
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

        private IEnumerator CRSpawnCharacter()
        {
            yield return null;

            SpawnCharacter();
        }

        private void SpawnCharacter()
        {
            for (int i = 0, length = characterIds.Count; i < length; i++)
            {
                EventController.Instance.AddCharacterChannel.RunIntegerChannel(characterIds[i]);
            }

            EventController.Instance.UpdateAssemplyChannel.RunVoidChannel();
        }

        private void AddCharacterId(int characterId)
        {
            if(!characterIds.Contains(characterId))
            {
                characterIds.Add(characterId);
            }
        }

        private void RemoveCharacterId(int characterId)
        {
            if(characterIds.Contains(characterId))
            {
                characterIds.Remove(characterId);
            }
        }

        public void Update()
        {
            if (GameManager.Instance.startGame)
            {
                SideMovement();
            }

            //if (Input.GetKeyDown(KeyCode.M))
            //{
            //    EventController.Instance.AddCharacterChannel.RunIntegerChannel(2);
            //    EventController.Instance.AddCharacterChannel.RunIntegerChannel(3);
            //    EventController.Instance.AddCharacterChannel.RunIntegerChannel(4);
            //    EventController.Instance.AddCharacterChannel.RunIntegerChannel(5);
            //    EventController.Instance.AddCharacterChannel.RunIntegerChannel(6);

            //    EventController.Instance.UpdateAssemplyChannel.RunVoidChannel();
            //}
        }
    }
}