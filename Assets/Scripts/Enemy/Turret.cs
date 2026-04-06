using System.Collections;
using UnityEngine;

public class Turret : Enemy
{
    [SerializeField] float fireRate = 1f;

    [Header("TurretProperty")]
    [SerializeField] Transform turretHead;
    [SerializeField] Transform target;
    [SerializeField] Transform projectileSpawnPos;

    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] int projectileDamage = 1;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject projectileHitVFX;

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
        while (target)
        {
            if (!target) break;
            yield return new WaitForSeconds(fireRate);
            TurretProjectileDamage turretProjectileDamage = Instantiate(projectilePrefab, projectileSpawnPos.position, turretHead.rotation).GetComponent<TurretProjectileDamage>();
            turretProjectileDamage.Init(projectileDamage, projectileSpeed, projectileHitVFX);
        }
    }
}
