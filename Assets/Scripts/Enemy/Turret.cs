using System.Collections;
using UnityEngine;

public class Turret : Enemy
{
    [SerializeField] Transform turretHead;
    [SerializeField] Transform target;
    [SerializeField] float fireRate = 1f;
    [SerializeField] Transform projectileSpawnPos;
    [SerializeField] GameObject projectilePrefab;

    void Start()
    {
        StartCoroutine(FireRoutine());
    }

    void Update()
    {
        turretHead.LookAt(target);
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Instantiate(projectilePrefab, projectileSpawnPos.position, turretHead.rotation);
        }
    }
}
