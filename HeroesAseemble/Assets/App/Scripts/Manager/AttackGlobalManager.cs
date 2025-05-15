using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class AttackGlobalManager : SceneDependentSingleton<AttackGlobalManager>
    {
        private Dictionary<int, CharacterBase> attackList = new Dictionary<int, CharacterBase>();

        public void AddAttackList(GameObject gameObject, CharacterBase characterBase)
        {
            int hashTash = gameObject.GetInstanceID();

            if(!attackList.ContainsKey(hashTash))
            {
                attackList.Add(hashTash, characterBase);
            }
        }

        public void RemoteAttackList(GameObject gameObject)
        {
            int hashTash = gameObject.GetInstanceID();

            if(attackList.ContainsKey(hashTash))
            {
                attackList.Remove(hashTash);
            }
        }


    }
}