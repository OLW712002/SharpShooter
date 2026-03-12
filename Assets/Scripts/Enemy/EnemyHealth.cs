using UnityEngine;
using System.Collections;
using NaughtyAttributes;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 5;

    [Header("Explosion Upon Death")]
    [SerializeField] DestroyType destroyType;
    [ShowIf("IsBulgeOut")][SerializeField] BulgeOutExplosion bulgeOutExplosion;
    [ShowIf("IsShakeUnstable")][SerializeField] ShakeUnstableExplosion shakeUnstableExplosion;
    [ShowIf("IsInstant")][SerializeField] InstantExplosion instantExplosion;
    bool IsBulgeOut => destroyType == DestroyType.BulgeOut;
    bool IsShakeUnstable => destroyType == DestroyType.ShakeUnstable;
    bool IsInstant => destroyType == DestroyType.Instant;

    Enemy enemyClass;

    void Start()
    {
        enemyClass = GetComponent<Enemy>();
    }

    public void TakeDamage(int dmg)
    {
        enemyHealth -= dmg;
        if (enemyHealth <= 0)
        {
            if (enemyClass == null)
            {
                Debug.Log("No enemy class");
                Destroy(this.gameObject);
                return;
            }

            ExplosionBehavior explosionBehavior = enemyClass.GetExplosionBehavior(destroyType, bulgeOutExplosion, shakeUnstableExplosion, instantExplosion);
            enemyClass.StartCoroutine(enemyClass.ExplodeSequence(explosionBehavior));
        }
    }
}
