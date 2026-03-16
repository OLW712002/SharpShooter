using UnityEngine;
using System.Collections;
using NaughtyAttributes;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 5;

    [SerializeField] ExplosionBehaviorInspector explosionBehaviorInspector;

    [Header("Explosion Upon Death")]
    [SerializeField] DestroyType destroyType;
    [ShowIf("IsBulgeOut")][SerializeField] BulgeOutExplosion bulgeOutExplosion;
    [ShowIf("IsShakeUnstable")][SerializeField] ShakeUnstableExplosion shakeUnstableExplosion;
    [ShowIf("IsInstant")][SerializeField] InstantExplosion instantExplosion;
    public bool IsBulgeOut => destroyType == DestroyType.BulgeOut;
    public bool IsShakeUnstable => destroyType == DestroyType.ShakeUnstable;
    public bool IsInstant => destroyType == DestroyType.Instant;

    Enemy enemyClass;
    ExplosionBehavior enemyExplosionBehavior;

    void Start()
    {
        enemyClass = GetComponent<Enemy>();
        enemyExplosionBehavior = ExplosionBehavior.GetExplosionBehavior(destroyType, bulgeOutExplosion, shakeUnstableExplosion, instantExplosion);
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
            enemyClass.StartCoroutine(enemyClass.ExplodeSequence(enemyExplosionBehavior));
        }
    }
}
