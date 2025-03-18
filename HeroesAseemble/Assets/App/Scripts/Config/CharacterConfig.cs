using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "GameConfig/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private List<CharacterInfor> characterInfors = new List<CharacterInfor>();

        public CharacterInfor GetCharacterInfor(int characterId)
        {
            CharacterInfor returnCharacterInfor = null;

            for (int i = 0, max = characterInfors.Count; i < max; i++)
            {
                if (characterInfors[i].characterId.Equals(characterId))
                {
                    returnCharacterInfor = characterInfors[i];
                    break;
                }
            }

            return returnCharacterInfor;
        }
    }

    [System.Serializable]
    public class CharacterInfor
    {
        public int characterId;
        public GameObject characterPrefab;
        public int heath;
        public string idleAnimationName = string.Empty;
        public string walkAnimationName = string.Empty;
        public string normalAttackAnimationName = string.Empty;
        public float beginAttackDistance = 0.5f;
    }
}