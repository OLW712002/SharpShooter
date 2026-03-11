using System.Collections;
using UnityEngine;

[System.Serializable]
public class InstantExplosion : ExplosionBehavior
{
    DestroyType destroyType = DestroyType.Instant;
    float enemySelfDestructDelay = 0f;

    public override IEnumerator BehaviorBeforeExploding(Transform ownerTransform)
    {
        ExplosionParameters explosionParameters = new ExplosionParameters(destroyType, enemyExplosion);
        //No pre action before explosion for instant explosion
        yield return null;
    }

    public override float GetSelfDestructDelay()
    {
        return enemySelfDestructDelay;
    }
}
