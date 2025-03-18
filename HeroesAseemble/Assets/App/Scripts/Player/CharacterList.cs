using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class CharacterList : SceneDependentSingleton<CharacterList>
    {
        public List<GameObject> pokemonListArray = new List<GameObject>();
        private List<SlotStatus> characterSlotStatuses = new List<SlotStatus>();
        public List<SlotStatus> CharacterSlotStatuses
        {
            get
            {
                return characterSlotStatuses;
            }
        }

        private void Start()
        {
            SlotStatus currentSlotStatus = null;
            foreach (Transform child in transform)
            {
                pokemonListArray.Add(child.gameObject);

                currentSlotStatus = child.GetComponent<SlotStatus>();
                characterSlotStatuses.Add(currentSlotStatus);
            }
        }
    }
}