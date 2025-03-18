using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class GlobalInfor : SceneDependentSingleton<GlobalInfor>
    {
        public static string CharacterTag = "CurrentPlayer";
        public static string EnemyTag = "Enemy";

        [SerializeField] private LayerMask characterLayerMask;

        public LayerMask CharacterLayerMask
        {
            get
            {
                return characterLayerMask;
            }
        }
    }
}