using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class AssemblyController : MonoBehaviour
    {
        private const int characterMax = 6;
        [SerializeField] private List<int> characterIds = new List<int>();

        private void Awake()
        {
            for(int i = 0;i < characterMax; i++)
            {
                characterIds.Add(-1);
            }
        }

        private void OnEnable()
        {
            EventController.Instance.UpdateAssemplyChannel.AddListener(UpdateAssemply);
            EventController.Instance.AddCharacterChannel.AddListener(AddCharacterIntoAssemply);
            EventController.Instance.RemoveCharacterChannel.AddListener(RemoveCharacterFromAssemply);
        }

        private void OnDisable()
        {
            if(EventController.Instance == null)
            {
                return;
            }

            EventController.Instance.UpdateAssemplyChannel.RemoveListener(UpdateAssemply);
            EventController.Instance.AddCharacterChannel.RemoveListener(AddCharacterIntoAssemply);
            EventController.Instance.RemoveCharacterChannel.RemoveListener(RemoveCharacterFromAssemply);
        }

        private void AddCharacterIntoAssemply(int characterId)
        {
            int changeIndex = -1;
            for(int i = 0; i < characterMax; i++)
            {
                if(characterIds[i]<0)
                {
                    changeIndex = i;
                    break;
                }
            }

            if(changeIndex >= 0)
            {
                characterIds[changeIndex] = characterId;
            }
        }

        private void RemoveCharacterFromAssemply(int characterIndex)
        {
            if(characterIndex >= 0 && characterIndex < characterIds.Count)
            {
                characterIds[characterIndex] = -1;
            }
        }

        private void UpdateAssemply()
        {
            CharacterInfor characterInfor = null;
            CharacterController currentCharacterController = null;
            GameObject currentPrefab = null;
            int currentCharacterId = 0;
            GameObject currentCharacterObject = null;
            Transform spawnTransform = null;

            for (int i = 0, max = characterIds.Count; i < max; i++)
            {
                currentCharacterId = characterIds[i];

                if(currentCharacterId < 0)
                {
                    continue;
                }

                characterInfor = EventController.Instance.GetCharacterInforChannel.RunChannel(currentCharacterId);

                if (characterInfor != null)
                {
                    currentPrefab = characterInfor.characterPrefab;
                }

                if (currentPrefab != null)
                {
                    if (CharacterList.Instance.CharacterSlotStatuses[i].IsFullSlot.Equals(false))
                    {
                        spawnTransform = CharacterList.Instance.pokemonListArray[i].transform;
                    }

                    if (spawnTransform == null)
                    {
                        spawnTransform = transform;
                    }

                    currentCharacterObject = Instantiate(currentPrefab, spawnTransform.position, Quaternion.identity);
                    currentCharacterController = currentCharacterObject.GetComponent<CharacterController>();
                    currentCharacterController.Init(currentCharacterId);
                    EventController.Instance.AddCharacterControllerChannel.RunCharacterControllerChannel(currentCharacterController);
                }
            }
        }
    }
}