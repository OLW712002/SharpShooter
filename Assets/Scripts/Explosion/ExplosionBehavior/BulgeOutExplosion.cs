using UnityEngine;
using System.Collections;

[System.Serializable]
public class BulgeOutExplosion
{
    DestroyType destroyType = DestroyType.BulgeOut;
    [SerializeField] GameObject enemyExplosion;
    [SerializeField] Vector3 explosionOffset = Vector3.zero;
    [SerializeField] float enemySelfDestructDelay = 0f;
    [SerializeField] float enemyBulgeOutScale = 1f;

    public IEnumerator ExplodeBehavior(Transform ownerTransform)
    {
        ExplosionParameters explosionParameters = new ExplosionParameters(destroyType, enemyExplosion, enemySelfDestructDelay, ownerTransform.localScale, enemyBulgeOutScale);

        Vector3 startValue = explosionParameters.BaseLocalScale;
        Vector3 targetValue = explosionParameters.BaseLocalScale * explosionParameters.BulgeOutScale;
        float elapsedTimeBulge = 0f;
        while (elapsedTimeBulge < explosionParameters.SelfDestructDelay)
        {
            elapsedTimeBulge += Time.deltaTime;
            ownerTransform.localScale = Vector3.Lerp(startValue, targetValue, elapsedTimeBulge / explosionParameters.SelfDestructDelay);
            yield return null;
        }
        ownerTransform.localScale = targetValue;
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
