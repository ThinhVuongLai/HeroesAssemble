using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace HeroesAssemble
{
    public class GameManager : SceneDependentSingleton<GameManager>
    {
        public int maxPokemon;
        public int getPokemon;
        public int maxGreenGoblin;
        public int getGreenGoblin;
        public int maxBlueGoblin;
        public int getBlueGoblin;
        public int maxLevel3Goblin;
        public int getLevel3Goblin;
        public int itemDownLucky;
        public int itemDownMaxLucky;
        public float maxSpeed;
        public GameObject Player;
        public GameObject playerHealthParticle;

        public GameObject healthItem;
        public GameObject pokemonItem;
        public bool startGame;
        public bool dead;
        public int deadGoblinCounter;
        public int catchPokemonCounter;

        void Start()
        {
            //  EndGame();
        }

        public void EndGame()
        {

        }

        public void GoGame()
        {
            Debug.Log("girdi go game");

            if (!startGame)
            {
                startGame = true;
            }
        }
    }
}