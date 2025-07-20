using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class ConfigManager : SceneDependentSingleton<ConfigManager>
    {
        [Header("Character Config")]
        [SerializeField] private CharacterConfig characterConfig;

        [Header("Enemy Config")]
        [SerializeField] private CharacterConfig enemyConfig;

        public override void Awake()
        {
            base.Awake();

            EventController.Instance.GetCharacterInforChannel.AddListener(GetCharacterInfor);
            EventController.Instance.GetEnemyInforChannel.AddListener(GetEnemyInfor);
        }

        private CharacterInfor GetCharacterInfor(int characterId)
        {
            if (characterConfig == null)
            {
                return null;
            }

            return characterConfig.GetCharacterInfor(characterId);
        }

        private CharacterInfor GetEnemyInfor(int characterId)
        {
            if (enemyConfig == null)
            {
                return null;
            }

            return enemyConfig.GetCharacterInfor(characterId);
        }
    }
}