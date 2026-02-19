using UnityEngine;
using System.Collections;
using StarterAssets;

public class SpawnGate : Enemy
{
    [SerializeField] GameObject spawnedEnemy;
    [SerializeField] float spawnSpeedPerSec = 0.5f;
    [SerializeField] Transform spawnPoint;
    [Tooltip("The radius of the area where enemies will spawn if there is a player within that radius.")]
    [SerializeField] float spawnRadius = 10f;

    FirstPersonController playerController;

    void Awake()
    {
        playerController = FindFirstObjectByType<FirstPersonController>();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy(spawnedEnemy));
    }

    IEnumerator SpawnEnemy(GameObject enemy)
    {
        while (playerController)
        {
            if (Vector3.Distance(transform.position, playerController.transform.position) < spawnRadius)
            {
                Instantiate(enemy, spawnPoint);
                Debug.Log("Spawned");
            }
            yield return new WaitForSeconds(1/spawnSpeedPerSec);
        }
    }
}
