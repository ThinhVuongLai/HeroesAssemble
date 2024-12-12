using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CharacterList : SceneDependentSingleton<CharacterList>
    {
        public List<GameObject> pokemonListArray = new List<GameObject>();

        private void Start()
        {
            foreach (Transform child in transform)
            {
                pokemonListArray.Add(child.gameObject);
            }
        }
    }
}