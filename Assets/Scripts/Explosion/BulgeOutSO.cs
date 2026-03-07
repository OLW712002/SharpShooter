using System.Collections;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

[CreateAssetMenu(fileName = "BulgeOutSO", menuName = "Scriptable Objects/BulgeOutSO")]
public class BulgeOutSO : ExplosionBehaviorSO
{
    DestroyType destroyType = DestroyType.BulgeOut;
    public GameObject enemyExplosion;
    public Vector3 explosionOffset = Vector3.zero;
    public float enemySelfDestructDelay = 0f;
    public float enemyBulgeOutScale = 1f;

    public override IEnumerator Explode(ExplosionParameters explosionParameters, Transform ownerTransform)
    {
        explosionParameters = new ExplosionParameters(destroyType, enemyExplosion, enemySelfDestructDelay, ownerTransform.localScale, enemyBulgeOutScale);

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

        Instantiate(explosionParameters.EnemyExplosion, ownerTransform.position + explosionOffset, Quaternion.identity);
        Destroy(ownerTransform.gameObject);
    }
}
