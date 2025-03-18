using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class SlotStatus : MonoBehaviour
    {
        private bool isFullSlot;
        public bool IsFullSlot
        {
            get
            {
                return isFullSlot;
            }
            set
            {
                isFullSlot = value;
            }
        }
    }
}