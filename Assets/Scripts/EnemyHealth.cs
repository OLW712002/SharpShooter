using UnityEngine;
using System.Collections;
using NaughtyAttributes;

public enum DestroyType
{
    BulgeOut,
    ShakeUnstable,
    Instant
}

public class ExplosionParameters
{
    public DestroyType DestroyType { get; }
    public GameObject EnemyExplosion { get; }
    public float SelfDestructDelay { get; }
    public Vector3 BaseLocalScale { get; }
    public float BulgeOutScale { get; }
    public float ShakeDuration { get; }
    public float MaxShakeMagnitude { get; }
    public AnimationCurve MagnitudeOverTime { get; }

    public ExplosionParameters(DestroyType destroyType, GameObject enemyExplosion, float selfDestructDelay, Vector3 baseLocalScale, float bulgeOutScale)
    {
        DestroyType = destroyType;
        EnemyExplosion = enemyExplosion;
        //Bulge out parameters
        SelfDestructDelay = selfDestructDelay;
        BaseLocalScale = baseLocalScale;
        BulgeOutScale = bulgeOutScale;
        //Default values for shake unstable parameters
        ShakeDuration = 1f;
        MaxShakeMagnitude = 0f;
        MagnitudeOverTime = AnimationCurve.Constant(0f, 1f, 0f);
    }

    public ExplosionParameters(DestroyType destroyType, GameObject enemyExplosion, float shakeDuration, float maxShakeMagnitude, AnimationCurve magnitudeOverTime)
    {
        DestroyType = destroyType;
        EnemyExplosion = enemyExplosion;
        //Default values for bulge out parameters
        SelfDestructDelay = 0f;
        BaseLocalScale = Vector3.one;
        BulgeOutScale = 1f;
        //Shake unstable parameters
        ShakeDuration = shakeDuration;
        MaxShakeMagnitude = maxShakeMagnitude;
        MagnitudeOverTime = magnitudeOverTime;
    }

    public ExplosionParameters(DestroyType destroyType, GameObject enemyExplosion)
    {
        DestroyType = destroyType;
        EnemyExplosion = enemyExplosion;
        //Default values for bulge out parameters
        SelfDestructDelay = 0f;
        BaseLocalScale = Vector3.one;
        BulgeOutScale = 1f;
        //Default values for shake unstable parameters
        ShakeDuration = 1f;
        MaxShakeMagnitude = 0f;
        MagnitudeOverTime = AnimationCurve.Constant(0f, 1f, 0f);
    }
}

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 5;

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
                Destroy(this.gameObject);
                return;
            }

            if (destroyType == DestroyType.ShakeUnstable)
            {
                ExplosionParameters shakeExplosion = new ExplosionParameters(destroyType, enemyExplosion, enemyShakeDuration, enemyMaxShakeMagnitude, enemyMagnitudeOverTime);
                StartCoroutine(SelfDestruct(shakeExplosion));
                return;
            }

            //Use enemy.cs parameters
            //var enemyExplosionParameters = enemyClass.GetParameterForExplosion(0);
            //StartCoroutine(SelfDestruct(enemyExplosionParameters));

            //Use this class's parameters
            ExplosionParameters instantExplosion = new ExplosionParameters(DestroyType.Instant, enemyExplosion);
            StartCoroutine(SelfDestruct(instantExplosion));
        }
    }

    public IEnumerator SelfDestruct(ExplosionParameters explosionParameters)
    {
        switch(explosionParameters.DestroyType)
        {
            case DestroyType.BulgeOut:
                Vector3 startValue = explosionParameters.BaseLocalScale;
                Vector3 targetValue = explosionParameters.BaseLocalScale * explosionParameters.BulgeOutScale;
                float elapsedTimeBulge = 0f;
                while (elapsedTimeBulge < explosionParameters.SelfDestructDelay)
                {
                    elapsedTimeBulge += Time.deltaTime;
                    transform.localScale = Vector3.Lerp(startValue, targetValue, elapsedTimeBulge / explosionParameters.SelfDestructDelay);
                    yield return null;
                }
                transform.localScale = targetValue;
                break;
            case DestroyType.ShakeUnstable:
                Vector3 originalLocalRotate = transform.localEulerAngles;
                float elapsedTimeShake = 0f;
                float internalFreq = 20f;
                while (elapsedTimeShake < explosionParameters.ShakeDuration)
                {
                    elapsedTimeShake += Time.deltaTime;
                    float normalizedTime = elapsedTimeShake / explosionParameters.ShakeDuration;
                    float currentMagnitude = explosionParameters.MaxShakeMagnitude * explosionParameters.MagnitudeOverTime.Evaluate(normalizedTime);
                    float offsetZ = Mathf.Sin(elapsedTimeShake * internalFreq * Mathf.PI * 2f) * currentMagnitude;
                    transform.localEulerAngles = originalLocalRotate + new Vector3(0f, 0f, offsetZ);
                    yield return null;
                }
                transform.localPosition = originalLocalRotate;
                break;
            case DestroyType.Instant:
                //Do nothing, just explode immediately
                break;
        }

        //Bulge out before explosion
        //if (data.enemySelfDestructDelay > 0)
        //{
        //    Vector3 startValue = data.enemyBaseLocalScale;
        //    Vector3 targetValue = data.enemyBaseLocalScale * data.enemyBulgeOutScale;
        //    float elapsedTime = 0f;
        //    while (elapsedTime < data.enemySelfDestructDelay)
        //    {
        //        elapsedTime += Time.deltaTime;
        //        transform.localScale = Vector3.Lerp(startValue, targetValue, elapsedTime / data.enemySelfDestructDelay);
        //        yield return null;
        //    }
        //    transform.localScale = targetValue;
        //}

        //Base explosion
        Instantiate(explosionParameters.EnemyExplosion, transform.position + enemyExplosionOffset, Quaternion.identity);
        Destroy(gameObject);
    }

    public ExplosionParameters GetParameterForExplosion()
    {
        switch (destroyType)
        {
            case DestroyType.BulgeOut:
                return new ExplosionParameters(destroyType, enemyExplosion, enemySelfDestructDelay, transform.localScale, enemyBulgeOutScale);
            case DestroyType.ShakeUnstable:
                return new ExplosionParameters(destroyType, enemyExplosion, enemyShakeDuration, enemyMaxShakeMagnitude, enemyMagnitudeOverTime);
            case DestroyType.Instant:
                return new ExplosionParameters(destroyType, enemyExplosion);
            default: 
                return new ExplosionParameters(DestroyType.Instant, enemyExplosion);
        }
    }
}
