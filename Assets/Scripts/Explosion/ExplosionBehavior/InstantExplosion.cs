using System.Collections;
using UnityEngine;

public class InstantExplosion
{
    DestroyType destroyType = DestroyType.Instant;
    float enemySelfDestructDelay = 0f;

    [SerializeField] GameObject enemyExplosion;
    [SerializeField] Vector3 explosionOffset = Vector3.zero;

    IEnumerator ExplodeBehavior(Transform ownerTransform)
    {
        ExplosionParameters explosionParameters = new ExplosionParameters(destroyType, enemyExplosion);
        //No pre action before explosion for instant explosion
        yield return null;
    }

    public GameObject GetEnemyExplosion()
    {
        return enemyExplosion;
    }

    public Vector3 GetExplosionOffset()
    {
        return explosionOffset;
    }

    public float GetSelfDestructDelay()
    {
        return enemySelfDestructDelay;
    }
}
