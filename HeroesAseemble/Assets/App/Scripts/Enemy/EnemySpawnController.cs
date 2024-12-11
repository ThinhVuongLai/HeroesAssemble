using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroesAssemble
{
    public class EnemySpawnController : MonoBehaviour
    {
        public GameObject objectToSpawn;
        public GameObject objectToSpawn2;
        public GameObject objectToSpawn3;
        public Vector3 origin = Vector3.zero;
        public float radius = 15;
        public bool deneme;
        public float startTime;

        public List<GameObject> level1Goblin = new List<GameObject>();
        public List<GameObject> level2Goblin = new List<GameObject>();
        public List<GameObject> level3Goblin = new List<GameObject>();

        public int startGoblin1Count;
        public int startGoblin2Count;
        public int startGoblin3Count;

        private void Start()
        {
            CreateGoblins();
            Invoke("GoGame", startTime);
        }

        private void CreateGoblins()
        {
            origin = transform.position;

            if (radius < 8)
            {
                radius = 8;
            }
            for (int i = 0; i < startGoblin1Count; i++)
            {
                Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
                randomPosition = new Vector3(randomPosition.x, 0.06f, randomPosition.z);
                GameObject goblin = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
                goblin.SetActive(false);
                goblin.GetComponent<Agent>().refPos = transform.position;
                level1Goblin.Add(goblin);
            }

            for (int i = 0; i < startGoblin2Count; i++)
            {
                Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
                randomPosition.y = 1;
                GameObject goblin2 = Instantiate(objectToSpawn2, randomPosition, Quaternion.identity);
                goblin2.SetActive(false);
                level2Goblin.Add(goblin2);

            }
            for (int i = 0; i < startGoblin3Count; i++)
            {

                Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
                randomPosition.y = 1;
                GameObject goblin3 = Instantiate(objectToSpawn3, randomPosition, Quaternion.identity);
                goblin3.SetActive(false);
                level3Goblin.Add(goblin3);
            }
        }

        private void GoGame()
        {

            if (!deneme)
            {
                InvokeRepeating("goSpawn", Random.Range(7f, 10f), Random.Range(8f, 10f));
                InvokeRepeating("goSpawnLevel2", Random.Range(35f, 45f), Random.Range(10f, 25f));
                InvokeRepeating("goSpawnBoss", Random.Range(60f, 70f), Random.Range(30f, 45f));
            }
            else
            {
                GoSpawn();

            }
        }

        private void GoSpawn()
        {
            if (GameManager.Instance.maxGreenGoblin > GameManager.Instance.getGreenGoblin)
            {
                foreach (GameObject item in level1Goblin)
                {

                    if (!item.activeSelf)
                    {
                        Debug.Log("ilk dalga sal�nd�");
                        item.SetActive(true);
                        level1Goblin.Remove(item);
                        level1Goblin.Add(item);
                        break;
                    }
                }
            }
        }

        private void GoSpawnLevel2()
        {
            if (GameManager.Instance.maxBlueGoblin > GameManager.Instance.getBlueGoblin)
            {
                foreach (GameObject item in level2Goblin)
                {

                    if (!item.activeSelf)
                    {
                        item.SetActive(true);
                        level2Goblin.Remove(item);
                        level2Goblin.Add(item);
                        break;
                    }
                }
            }
            // Finds a position in a sphere with a radius of 10 units.
        }

        private void GoSpawnBoss()
        {
            if (GameManager.Instance.maxLevel3Goblin > GameManager.Instance.getLevel3Goblin)
            {
                foreach (GameObject item in level3Goblin)
                {

                    if (!item.activeSelf)
                    {
                        item.SetActive(true);
                        level3Goblin.Remove(item);
                        level3Goblin.Add(item);
                        break;
                    }
                }
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (GameManager.Instance.dead)
            {
                CancelInvoke();
            }
        }
    }
}