using System.Collections;
using UnityEngine;

[System.Serializable]
public class InstantExplosion : ExplosionBehavior
{
    public override IEnumerator BehaviorBeforeExploding(Transform ownerTransform)
    {
        yield return null;
    }

    public override float GetSelfDestructDelay()
    {
        return 0f;
    }
}
