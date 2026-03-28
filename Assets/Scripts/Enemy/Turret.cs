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
        if (!target) return;
        turretHead.LookAt(target.position - new Vector3(0,1,0));
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
