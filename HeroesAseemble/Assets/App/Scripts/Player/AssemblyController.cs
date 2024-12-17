using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class AssemblyController : SceneDependentSingleton<AssemblyController>
    {
        [SerializeField] private List<int> characterIds = new List<int>();

        [Header("Channel")]
        [SerializeField] private GetCharacterInforChannel getCharacterInforChannel;

        public void AddCharacterIntoAssemply(int characterId)
        {
            if (!characterIds.Contains(characterId))
            {
                characterIds.Add(characterId);
            }
        }

        public void RemoveCharacterFromAssemply(int characterId)
        {
            if (characterIds.Contains(characterId))
            {
                characterIds.Remove(characterId);
            }
        }

        public void UpdateAssemply()
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