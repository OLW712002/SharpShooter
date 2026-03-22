using UnityEngine;
using System.Collections;

[System.Serializable]
public class BulgeOutExplosion : ExplosionBehavior
{
    [SerializeField] float enemySelfDestructDelay = 1f;
    [SerializeField] float enemyBulgeOutScale = 2f;

    public override IEnumerator BehaviorBeforeExploding(Transform ownerTransform)
    {
        Vector3 startValue = ownerTransform.localScale;
        Vector3 targetValue = ownerTransform.localScale * enemyBulgeOutScale;
        float elapsedTimeBulge = 0f;
        while (elapsedTimeBulge < enemySelfDestructDelay)
        {
            elapsedTimeBulge += Time.deltaTime;
            ownerTransform.localScale = Vector3.Lerp(startValue, targetValue, elapsedTimeBulge / enemySelfDestructDelay);
            yield return null;
        }
        ownerTransform.localScale = targetValue;
    }

    public override float GetSelfDestructDelay()
    {
        return enemySelfDestructDelay;
    }
}
