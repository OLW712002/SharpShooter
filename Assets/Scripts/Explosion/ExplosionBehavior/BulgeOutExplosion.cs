using UnityEngine;
using System.Collections;

[System.Serializable]
public class BulgeOutExplosion : ExplosionBehavior
{
    //DestroyType destroyType = DestroyType.BulgeOut;

    [SerializeField] float enemySelfDestructDelay = 1f;
    [SerializeField] float enemyBulgeOutScale = 2f;

    public override IEnumerator BehaviorBeforeExploding(Transform ownerTransform)
    {
        //ExplosionParameters explosionParameters = new ExplosionParameters(destroyType, enemyExplosion, enemySelfDestructDelay, ownerTransform.localScale, enemyBulgeOutScale);

        //Vector3 startValue = explosionParameters.BaseLocalScale;
        //Vector3 targetValue = explosionParameters.BaseLocalScale * explosionParameters.BulgeOutScale;
        //float elapsedTimeBulge = 0f;
        //while (elapsedTimeBulge < explosionParameters.SelfDestructDelay)
        //{
        //    elapsedTimeBulge += Time.deltaTime;
        //    ownerTransform.localScale = Vector3.Lerp(startValue, targetValue, elapsedTimeBulge / explosionParameters.SelfDestructDelay);
        //    yield return null;
        //}
        //ownerTransform.localScale = targetValue;

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
