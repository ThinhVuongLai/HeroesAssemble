using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class AssemblyController : MonoBehaviour
    {
        [SerializeField] private List<int> characterIds = new List<int>();

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
            if (!characterIds.Contains(characterId))
            {
                characterIds.Add(characterId);
            }
        }

        private void RemoveCharacterFromAssemply(int characterId)
        {
            if (characterIds.Contains(characterId))
            {
                characterIds.Remove(characterId);
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