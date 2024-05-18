using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private bool[] spawnPointUsed;

    private void Start()
    {
        spawnPointUsed = new bool[spawnPoints.Length];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            SpawnEnemy(i); 
        }
    }

    private void SpawnEnemy(int index)
    {
        if (enemyPrefab != null && spawnPoints.Length > 0)
        {
            if (!spawnPointUsed[index])
            {
                spawnPointUsed[index] = true;

                Transform spawnPoint = spawnPoints[index];

                GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

                enemyInstance.transform.parent = transform;
            }
            else
            {
                Debug.LogWarning("Spawn point at index " + index + " has already been used.");
            }
        }
        else
        {
            Debug.LogError("Enemy prefab is not assigned or there are no spawn points available.");
        }
    }
}
