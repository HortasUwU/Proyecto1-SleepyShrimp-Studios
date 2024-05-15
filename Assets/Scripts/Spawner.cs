using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            if (spawnPoints.Length > 0)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation, transform);
            }
            else
            {
                Debug.LogError("No se han asignado puntos de aparición.");
            }
        }
        else
        {
            Debug.LogError("No se ha asignado el enemigo.");
        }
    }
}
