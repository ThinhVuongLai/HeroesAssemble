using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class AssemblyController : SceneDependentSingleton<AssemblyController>
    {
        public GameObject pokemon1;
        public GameObject pokemon2;
        public GameObject pokemon3;
        public GameObject pokemon4;
        public GameObject pokemon5;
        public GameObject pokemon6;
        public GameObject pokemon7;
        public GameObject pokemon8;
        public GameObject pokemon9;
        public GameObject pokemon10;
        public GameObject pokemon11;
        public GameObject pokemon12;

        public void PokeWin(int rarityLevel, Vector3 pokePosition)
        {
            if (GameManager.Instance.maxPokemon > GameManager.Instance.getPokemon)
            {
                GameManager.Instance.catchPokemonCounter++;
                if (rarityLevel == 1)
                {
                    int random = Random.Range(0, 4);

                    if (random == 0)
                    {
                        Instantiate(pokemon1, pokePosition, Quaternion.identity);
                    }
                    else if (random == 1)
                    {

                        Instantiate(pokemon9, pokePosition, Quaternion.identity);
                    }
                    else if (random == 2)
                    {

                        Instantiate(pokemon5, pokePosition, Quaternion.identity);
                    }
                    else if (random == 3)
                    {
                        Instantiate(pokemon4, pokePosition, Quaternion.identity);

                    }


                }
                else if (rarityLevel == 2)
                {
                    int random = Random.Range(0, 3);

                    if (random == 0)
                    {
                        Instantiate(pokemon2, pokePosition, Quaternion.identity);
                    }
                    else if (random == 1)
                    {
                        Instantiate(pokemon3, pokePosition, Quaternion.identity);
                    }
                    else if (random == 2)
                    {
                        Instantiate(pokemon10, pokePosition, Quaternion.identity);
                    }
                }
                else if (rarityLevel == 3)
                {
                    int random = Random.Range(0, 3);

                    if (random == 0)
                    {

                        Instantiate(pokemon6, pokePosition, Quaternion.identity);
                    }
                    else if (random == 1)
                    {

                        Instantiate(pokemon7, pokePosition, Quaternion.identity);
                    }
                    else if (random == 2)
                    {

                        Instantiate(pokemon8, pokePosition, Quaternion.identity);
                    }
                }

                GameManager.Instance.getPokemon++;
            }
            else
            {
                Debug.Log("Pokemon Win But Max Pokemon Limit");
            }
        }
    }
}