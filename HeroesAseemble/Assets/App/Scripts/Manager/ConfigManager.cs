using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class ConfigManager : SceneDependentSingleton<ConfigManager>
    {
        [Header("Character Config")]
        [SerializeField] private CharacterConfig characterConfig;

        private void Awake()
        {
            EventController.Instance.GetCharacterInforChannel.AddListener(GetCharacterInfor);
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