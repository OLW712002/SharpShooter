using UnityEngine;
using System.Collections;
using NaughtyAttributes;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 5;

    [Header("ExplosionTest")]
    [SerializeField] DestroyType testDestroyType;
    [ShowIf("testBulgeOut")][SerializeField] BulgeOutExplosion testBulgeOutExplosion;
    [ShowIf("testShakeUnstable")][SerializeField] ShakeUnstableExplosion testShakeUnstableExplosion;
    [ShowIf("testInstant")][SerializeField] InstantExplosion testInstantExplosion;
    bool testBulgeOut => testDestroyType == DestroyType.BulgeOut;
    bool testShakeUnstable => testDestroyType == DestroyType.ShakeUnstable;
    bool testInstant => testDestroyType == DestroyType.Instant;

    [Header("Explosion Upon Death")]
    [SerializeField] DestroyType destroyType;
    [SerializeField] GameObject enemyExplosion;
    [SerializeField] Vector3 enemyExplosionOffset = Vector3.zero;

    [Header("BulgeOut")]
    [ShowIf("IsBulgeOut")][SerializeField] float enemySelfDestructDelay = 0f;
    [ShowIf("IsBulgeOut")][SerializeField] float enemyBulgeOutScale = 1f;
    bool IsBulgeOut => destroyType == DestroyType.BulgeOut;

    [Header("ShakeUnstable")]
    [ShowIf("IsShakeUnstable")][SerializeField] float enemyShakeDuration = 1f;
    [ShowIf("IsShakeUnstable")][SerializeField] float enemyMaxShakeMagnitude = 0f;
    [ShowIf("IsShakeUnstable")][SerializeField] AnimationCurve enemyMagnitudeOverTime = AnimationCurve.Constant(0f, 1f, 0f);
    bool IsShakeUnstable => destroyType == DestroyType.ShakeUnstable;

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
            //StartCoroutine(Exploding(GetParameterForExplosion()));

            ExplosionBehavior explosionBehavior = enemyClass.GetExplosionBehavior(testDestroyType, testBulgeOutExplosion, testShakeUnstableExplosion, testInstantExplosion);
            enemyClass.StartCoroutine(enemyClass.ExplodeSequence(explosionBehavior));
        }
    }

    //public IEnumerator Exploding(ExplosionParameters explosionParameters)
    //{
    //    //Pre action before explosion based on destroy type
    //    switch (explosionParameters.DestroyType)
    //    {
    //        case DestroyType.BulgeOut:
    //            Vector3 startValue = explosionParameters.BaseLocalScale;
    //            Vector3 targetValue = explosionParameters.BaseLocalScale * explosionParameters.BulgeOutScale;
    //            float elapsedTimeBulge = 0f;
    //            while (elapsedTimeBulge < explosionParameters.SelfDestructDelay)
    //            {
    //                elapsedTimeBulge += Time.deltaTime;
    //                transform.localScale = Vector3.Lerp(startValue, targetValue, elapsedTimeBulge / explosionParameters.SelfDestructDelay);
    //                yield return null;
    //            }
    //            transform.localScale = targetValue;
    //            break;
    //        case DestroyType.ShakeUnstable:
    //            Vector3 originalLocalRotate = transform.localEulerAngles;
    //            float elapsedTimeShake = 0f;
    //            float internalFreq = 20f;
    //            while (elapsedTimeShake < explosionParameters.ShakeDuration)
    //            {
    //                elapsedTimeShake += Time.deltaTime;
    //                float normalizedTime = elapsedTimeShake / explosionParameters.ShakeDuration;
    //                float currentMagnitude = explosionParameters.MaxShakeMagnitude * explosionParameters.MagnitudeOverTime.Evaluate(normalizedTime);
    //                float offsetZ = Mathf.Sin(elapsedTimeShake * internalFreq * Mathf.PI * 2f) * currentMagnitude;
    //                transform.localEulerAngles = originalLocalRotate + new Vector3(0f, 0f, offsetZ);
    //                yield return null;
    //            }
    //            transform.localPosition = originalLocalRotate;
    //            break;
    //        case DestroyType.Instant:
    //            //Do nothing, just explode immediately
    //            break;
    //    }

    //    //Explosion
    //    Instantiate(explosionParameters.EnemyExplosion, transform.position + enemyExplosionOffset, Quaternion.identity);
    //    Destroy(gameObject);
    //}

    //public ExplosionParameters GetParameterForExplosion()
    //{
    //    switch (destroyType)
    //    {
    //        case DestroyType.BulgeOut:
    //            return new ExplosionParameters(destroyType, enemyExplosion, enemySelfDestructDelay, transform.localScale, enemyBulgeOutScale);
    //        case DestroyType.ShakeUnstable:
    //            return new ExplosionParameters(destroyType, enemyExplosion, enemyShakeDuration, enemyMaxShakeMagnitude, enemyMagnitudeOverTime);
    //        case DestroyType.Instant:
    //            return new ExplosionParameters(destroyType, enemyExplosion);
    //        default: 
    //            return new ExplosionParameters(DestroyType.Instant, enemyExplosion);
    //    }
    //}
}
