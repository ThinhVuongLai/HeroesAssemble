using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private NavMeshPoissonSpawner navMeshPoissonSpawner;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private List<SpawnItem> spawnItems = new List<SpawnItem>();
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyNumber = 50;

    private void Start()
    {
        SpawnEnemy(parentTransform);
    }

    public void SpawnEnemy(Transform parent = null)
    {
        int spawnNumber = 0;
        List<int> spawnNumbers = GetListSpawnNumber();
        int leftSpawn = 0;
        int currentSpawnNumber = 0;

        int index = 0;
        foreach (var item in spawnItems)
        {
            spawnNumber = spawnNumbers[index];
            if(leftSpawn>0)
            {
                spawnNumber += leftSpawn;
            }

            currentSpawnNumber = SpawnEnemyAtLocal(spawnNumber,item.transform,item.AreaSpawn, parent: parent);

            leftSpawn = spawnNumber - currentSpawnNumber;
            index++;
        }
    }

    private int SpawnEnemyAtLocal(int spawnNumber,Transform transform, Vector2 areaSpawn, Transform parent = null)
    {
        Vector3 centerSpawn = transform.position;
        centerSpawn.y = 0;

        var currentEnemy = navMeshPoissonSpawner.SpawnObjectsOnNavMeshWithPoisson(spawnNumber, areaSpawn, centerSpawn, parent);

        return currentEnemy;
    }

    private List<int> GetListSpawnNumber()
    {
        List<int> returnList = new List<int>();
        int spawnItemsLength = spawnItems.Count;

        if (spawnItemsLength>=enemyNumber)
        {
            for (int i = 0, max= spawnItemsLength; i < max; i++)
            {
                returnList.Add(1);
            }
        }
        else
        {
            int everySpawnNumber = enemyNumber / spawnItemsLength;
            int leftNumber = enemyNumber - (everySpawnNumber * spawnItemsLength);

            for (int i = 0, max = spawnItemsLength; i < max; i++)
            {
                returnList.Add(everySpawnNumber);
            }

            if(leftNumber>0)
            {
                int random = UnityEngine.Random.Range(0, spawnItemsLength);
                returnList[random] += leftNumber;
            }
        }

        return returnList;
    }
}
