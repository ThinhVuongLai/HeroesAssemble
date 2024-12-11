using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class EnemyTriggerChecked : MonoBehaviour
    {
        public bool triggerPlayer;
        private void OnEnable()
        {
            triggerPlayer = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "SubPlayer" || other.tag == "Pokemon")
            {
                triggerPlayer = true;
            }

        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "SubPlayer" || other.tag == "Pokemon")
            {
                triggerPlayer = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "SubPlayer" || other.tag == "Pokemon")
            {
                triggerPlayer = false;
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}