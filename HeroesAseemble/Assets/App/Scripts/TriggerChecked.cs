using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class TriggerChecked : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Grass"))
            {
                int luckyPet = Random.Range(0, 100 - GameManager.Instance.itemDownLucky);
                if (luckyPet == 7)
                {
                    //AssemblyController.Instance.PokeWin(other.GetComponent<GrassInfo>().grassMapNumber, transform.position);
                }
            }
        }
    }
}