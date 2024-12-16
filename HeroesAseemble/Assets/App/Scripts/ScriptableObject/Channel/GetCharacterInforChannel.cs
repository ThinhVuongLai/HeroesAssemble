using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    [CreateAssetMenu(fileName = "GetCharacterChannel", menuName = "ScriptableObject/Channel/GetCharacterInforChannel")]
    public class GetCharacterInforChannel : ScriptableObject
    {
        private Func<int, CharacterInfor> func;

        public void AddListener(Func<int, CharacterInfor> func)
        {
            this.func += func;
        }

        public void RemoveListener(Func<int, CharacterInfor> func)
        {
            this.func -= func;
        }

        public CharacterInfor RunChannel(int characterId)
        {
            CharacterInfor characterInfor = func == null ? null : func.Invoke(characterId);

            return characterInfor;
        }
    }
}