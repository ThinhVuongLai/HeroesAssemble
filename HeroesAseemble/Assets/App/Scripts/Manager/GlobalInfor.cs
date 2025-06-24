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
        [SerializeField] private Camera uiCamera;

        [Header("Prefabs")]
        [SerializeField] private GameObject healthBarPrefab;

        public LayerMask CharacterLayerMask
        {
            get
            {
                return characterLayerMask;
            }
        }

        public GameObject HeathBarPrefab => healthBarPrefab;
        public Camera UICamera => uiCamera;
    }
}