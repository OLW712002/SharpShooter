using UnityEngine;
using System.Collections;

public abstract class ExplosionBehavior
{
    [SerializeField] protected GameObject enemyExplosion;
    [SerializeField] protected Vector3 explosionOffset = Vector3.zero;

    public abstract IEnumerator BehaviorBeforeExploding(Transform ownerTransform);

    public virtual GameObject GetEnemyExplosion()
    {
        return enemyExplosion;
    }

    public virtual Vector3 GetExplosionOffset()
    {
        return explosionOffset;
    }
    public abstract float GetSelfDestructDelay();

    public static ExplosionBehavior GetExplosionBehavior(DestroyType destroyType, BulgeOutExplosion bulgeOutExplosion, ShakeUnstableExplosion shakeUnstableExplosion, InstantExplosion instantExplosion)
    {
        switch (destroyType)
        {
            case DestroyType.BulgeOut:
                return bulgeOutExplosion;
            case DestroyType.ShakeUnstable:
                return shakeUnstableExplosion;
            case DestroyType.Instant:
                return instantExplosion;
            default:
                return null;
        }
    }
}
