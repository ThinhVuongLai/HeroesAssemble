using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class AssemblyController : MonoBehaviour
    {
        [SerializeField] private List<int> characterIds = new List<int>();

        [Header("Channel")]
        [SerializeField] private GetCharacterInforChannel getCharacterInforChannel;
        [SerializeField] private VoidChannel updateAssemplyChannel;
        [SerializeField] private IntegerChannel addCharacterChannel;
        [SerializeField] private IntegerChannel removeCharacterChannel;

        private void OnEnable()
        {
            updateAssemplyChannel.AddListener(UpdateAssemply);
            addCharacterChannel.AddListener(AddCharacterIntoAssemply);
            removeCharacterChannel.AddListener(RemoveCharacterFromAssemply);
        }

        private void OnDisable()
        {
            updateAssemplyChannel.RemoveListener(UpdateAssemply);
            addCharacterChannel.RemoveListener(AddCharacterIntoAssemply);
            removeCharacterChannel.RemoveListener(RemoveCharacterFromAssemply);
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
            GameObject currentPrefab = null;
            int currentCharacterId = 0;
            GameObject currentCharacterObject = null;
            Transform spawnTransform = null;

            for (int i = 0, max = characterIds.Count; i < max; i++)
            {
                currentCharacterId = characterIds[i];
                characterInfor = getCharacterInforChannel.RunChannel(currentCharacterId);

                if (characterInfor != null)
                {
                    currentPrefab = characterInfor.characterPrefab;
                }

                if(currentPrefab!=null)
                {
                    currentCharacterObject = Instantiate(currentPrefab, transform.position, Quaternion.identity);

                    if (CharacterList.Instance.pokemonListArray[i].GetComponent<State>().isFill == false)
                    {
                        spawnTransform = CharacterList.Instance.pokemonListArray[i].transform;
                    }

                    if(spawnTransform==null)
                    {
                        spawnTransform = transform;
                    }

                    currentCharacterObject.transform.position = spawnTransform.position;
                }
            }
        }
    }
}