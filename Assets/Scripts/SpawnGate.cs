using UnityEngine;
using System.Collections;
using StarterAssets;

public class SpawnGate : Enemy
{
    [SerializeField] GameObject spawnedEnemy;
    [SerializeField] float spawnSpeedPerSec = 0.5f;
    [SerializeField] Transform spawnPoint;

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
            Instantiate(enemy, spawnPoint);
            Debug.Log("Spawned");
            yield return new WaitForSeconds(1/spawnSpeedPerSec);
        }
    }
}
