using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class ConfigManager : SceneDependentSingleton<ConfigManager>
    {
        [Header("Character Config")]
        [SerializeField] private CharacterConfig characterConfig;
        [SerializeField] private GetCharacterInforChannel getCharacterInforChannel;

        private void OnEnable()
        {
            getCharacterInforChannel.AddListener(GetCharacterInfor);
        }

        private void OnDisable()
        {
            getCharacterInforChannel.RemoveListener(GetCharacterInfor);
        }

        private CharacterInfor GetCharacterInfor(int characterId)
        {
            if (characterConfig == null)
            {
                return null;
            }

            return characterConfig.GetCharacterInfor(characterId);
        }
    }
}